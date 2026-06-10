using NAudio.Wave;
using Whisper.net;

namespace AIClient.Utils
{
    public class WhisperListener
    {
        private WaveInEvent? _waveIn;
        private List<byte> _currentPhraseAudio = new List<byte>();
        private WhisperProcessor _whisperProcessor;
        private bool _isProcessing = false;
        private DateTime _lastAudioReceivedTime;
        private bool _silenceDetected = false;

        // UI Callback to update your text box
        public Action<string, bool>? OnTextUpdated { get; set; }
        public Action? OnSilenceDetected { get; set; }

        public WhisperListener(WhisperFactory factory)
        {
            _whisperProcessor = factory.CreateBuilder()
            .WithLanguage("en") // Optional: Lock to English to speed up initial detection
            .Build();
        }

        public void StartListening()
        {
            _silenceDetected = false; // reset on each listen session
            _currentPhraseAudio.Clear();

            // Whisper strictly requires 16,000Hz, 16-bit, Mono PCM audio
            _waveIn = new WaveInEvent();
            _waveIn.WaveFormat = new WaveFormat(16000, 16, 1);

            _waveIn.DataAvailable += async (sender, e) =>
            {
                // Append incoming live mic data
                _currentPhraseAudio.AddRange(e.Buffer.Take(e.BytesRecorded));
                if(!IsAudioSilent() ) 
                    _lastAudioReceivedTime = DateTime.Now;

                // Trigger a transcription update every 1000ms
                if (!_isProcessing && !_silenceDetected && _currentPhraseAudio.Count > 32000) // At least 1 second of audio
                {
                    await ProcessCurrentAudioBuffer();
                }
            };

            _waveIn.StartRecording();

            // Start a background loop to check for sentence finalization (silence/pauses)
            Task.Run(CheckForSilenceLoop);
        }

        private async Task ProcessCurrentAudioBuffer(bool isFinal = false)
        {
            _isProcessing = true;
            try
            {
                // 1. Take ONLY the actual recorded audio bytes (No 30-second zero-padding!)
                byte[] audioSnapshot = _currentPhraseAudio.ToArray();
                if (isFinal)
                    _currentPhraseAudio.Clear();

                // 2. Wrap the real bytes into a valid WAV stream directly
                using var rawStream = new MemoryStream(audioSnapshot);
                var waveFormat = new WaveFormat(16000, 16, 1);
                using var rawSource = new NAudio.Wave.RawSourceWaveStream(rawStream, waveFormat);

                using var wavMemoryStream = new MemoryStream();
                NAudio.Wave.WaveFileWriter.WriteWavFileToStream(wavMemoryStream, rawSource);
                wavMemoryStream.Seek(0, SeekOrigin.Begin);

                string interimText = "";

                // 3. Process the short WAV stream
                await foreach (var segment in _whisperProcessor.ProcessAsync(wavMemoryStream))
                {
                    interimText += segment.Text;
                }

                if (!string.IsNullOrWhiteSpace(interimText))
                {
                    // Clean up the text just in case Whisper adds trailing spaces
                    OnTextUpdated?.Invoke(interimText.Trim(), false);
                }
            }
            catch (Exception ex)
            {
                // Actually print the error so we aren't flying blind!
                System.Diagnostics.Debug.WriteLine($"WHISPER ERROR: {ex.Message}");
            }
            finally
            {
                _isProcessing = false;
            }
        }

        private async Task CheckForSilenceLoop()
        {
            while (true)
            {
                await Task.Delay(300);

                // Simple silence check: If no audio or a natural pause happens for > half a second
                if (_currentPhraseAudio.Count > 0 && (DateTime.Now - _lastAudioReceivedTime).TotalMilliseconds > 500)
                {
                    if (IsAudioSilent())
                    {
                        _silenceDetected = true;

                        if (_isProcessing)
                        {
                            // Wait for any in-flight processing to finish
                            while (_isProcessing)
                                await Task.Delay(50);
                        }
                        // Final pass to get the absolute final transcription text
                        await ProcessCurrentAudioBuffer(isFinal: true);

                        // Tell the UI to commit this text permanently (isFinal = true)
                        OnTextUpdated?.Invoke("", true);
                        OnSilenceDetected?.Invoke();
                        break; 
                    }
                }
            }
        }

        private bool IsAudioSilent(double threshold = 200)
        {
            // 16000 samples/sec * 2 bytes per sample * 3 seconds = 96000 bytes
            int twoSecondBytes = 16000 * 2 * 3;

            byte[] recentAudio = _currentPhraseAudio.Count > twoSecondBytes
                ? _currentPhraseAudio.Skip(_currentPhraseAudio.Count - twoSecondBytes).ToArray()
                : _currentPhraseAudio.ToArray();

            double energy = 0;
            int sampleCount = recentAudio.Length / 2;

            for (int i = 0; i < recentAudio.Length - 1; i += 2)
            {
                short sample = BitConverter.ToInt16(recentAudio, i);
                energy += Math.Abs(sample);
            }

            return (energy / sampleCount) < threshold;
        }

        public void StopListening()
        {
            _silenceDetected = false;
            _waveIn?.StopRecording();
            _waveIn?.Dispose();
        }
    }
}
