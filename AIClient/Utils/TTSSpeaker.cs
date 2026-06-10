using AIClient.Model;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;

namespace AIClient.Utils
{
    public class TTSSpeaker
    {
        public readonly AppSettings _appSettings;

        public TTSSpeaker(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public async Task SpeakAsync(string text)
        {
            if(!_appSettings.UseTTS)
                return;
            var synth = new SpeechSynthesizer();

            // pick a specific neural voice
            var voice = SpeechSynthesizer.AllVoices.First(v => v.DisplayName.Contains(_appSettings.TTSVoice));
            synth.Voice = voice;

            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);

            using var reader = new DataReader(stream);
            byte[] buffer = new byte[stream.Size];
            await reader.LoadAsync((uint)stream.Size);
            reader.ReadBytes(buffer);

            // Play with NAudio
            using var ms = new MemoryStream(buffer);
            using var audio = new WaveFileReader(ms);
            using var output = new WaveOutEvent();
            output.Init(audio);
            output.Play();

            while (output.PlaybackState == PlaybackState.Playing)
                await Task.Delay(50);
        }

    }
}
