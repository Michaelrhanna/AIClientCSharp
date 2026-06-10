using AIClient.Model.Interface;
using System.Text;
using Windows.Media.SpeechSynthesis;
using Windows.Storage.Streams;
using AIClient.Model;
using NAudio.Wave;
using AIClient.Utils;




namespace AIClient
{
    public partial class FormMain : Form
    {
        private readonly IChatService _chatService;
        private readonly Func<FormSetup> _formSetupFactory;

        private AppSettings _appSettings;
        private readonly WhisperListener _whisperListener;
        private bool _isListening = false;

        private string _completedConversationHistory = string.Empty;

        TTSSpeaker _ttsSpeaker; 

        public FormMain(IChatService chatService, Func<FormSetup> formSetupFactory, AppSettings appSettings, WhisperListener whisperListener, TTSSpeaker ttsSpeaker)
        {
            _chatService = chatService;
            InitializeComponent();
            _formSetupFactory = formSetupFactory;
            _appSettings = appSettings;
            _whisperListener = whisperListener;
            _whisperListener.OnTextUpdated = OnWhisperTextUpdated;
            _whisperListener.OnSilenceDetected = OnSilenceDetected;
            _ttsSpeaker = ttsSpeaker;

            _chatService.AddSystemSetup(_appSettings.SystemMessage);
        }


        private async void btnSend_Click(object? sender, EventArgs e)
        {
            var userInput = txtUserInput.Text.Trim();
            if (string.IsNullOrEmpty(userInput))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }
            btnSend.Enabled = false;
            try
            {
                var reply = await _chatService.SendAsync(userInput);
                rtbChat.AppendText($"User: {userInput}\r\n");
                rtbChat.AppendText($"AI: {reply}\r\n\r\n");
                txtUserInput.Clear();
                await _ttsSpeaker.SpeakAsync(reply);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
            finally
            {
                btnSend.Enabled = true;
            }
        }

        private void btnSetupSystem_Click(object sender, EventArgs e)
        {
            var result = _formSetupFactory().ShowDialog();
            if (result == DialogResult.OK)
            {
                rtbChat.Clear();

            }
        }

        private void btnNewConversation_Click(object sender, EventArgs e)
        {
            _chatService.ClearHistory();
            rtbChat.Clear();
        }

        private void btnSpeak_Click(object sender, EventArgs e)
        {
            if(!_isListening)
            {
                txtUserInput.Clear();
                _completedConversationHistory = string.Empty;
                _isListening = true;
                btnSpeak.Text = "Listening...";
                _whisperListener.StartListening();
            }
            else
            {
                _isListening = false;
                btnSpeak.Text = "Speak";
                _whisperListener.StopListening();
            }
        }

        

        

        private void OnWhisperTextUpdated(string newText, bool isFinal)
        {
            // Ensure we switch back to the Main UI Thread
            Invoke(() =>
            {
                if (isFinal)
                {
                    // Phrase ended. Append the last stable text permanently to the absolute history
                    _completedConversationHistory = string.Empty;
                }
                else
                {
                    // User is still talking. Overwrite only the current "live" phrase block
                    txtUserInput.Text = _completedConversationHistory + newText;

                    // Auto-scroll to the bottom of the text box
                    txtUserInput.SelectionStart = txtUserInput.Text.Length;
                    txtUserInput.ScrollToCaret();
                }
            });
        }

        private void OnSilenceDetected()
        {
            Invoke(() =>
            {
                _isListening = false;
                btnSpeak.Text = "Speak";
                _whisperListener.StopListening();
            });
        }
    }
}
