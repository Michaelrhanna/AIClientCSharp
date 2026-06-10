using AIClient.Model;
using AIClient.Model.Interface;
using Windows.Media.SpeechSynthesis;


namespace AIClient
{
    public partial class FormSetup : Form
    {
        private readonly IChatService _chatService;
        private readonly AppSettings _appSettings;
        private readonly SettingsService _settingsService;

        public FormSetup(IChatService chatService, AppSettings appSettings, SettingsService settingsService)
        {
            InitializeComponent();
            _chatService = chatService;
            _appSettings = appSettings;
            _settingsService = settingsService;

            rtbSystemSetup.Text = _appSettings.SystemMessage;

            foreach (var voice in SpeechSynthesizer.AllVoices)
            {
                cmbAvailableVoices.Items.Add(voice.DisplayName);
            }

            if (_appSettings.TTSVoice != null && cmbAvailableVoices.Items.Contains(_appSettings.TTSVoice))
            {
                cmbAvailableVoices.SelectedItem = _appSettings.TTSVoice;
            }

            chkUseTTS.Checked = _appSettings.UseTTS;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(rtbSystemSetup.Text.Trim()))
            {
                MessageBox.Show("System setup cannot be empty.");
                return;
            }

            if (_chatService.HasChatHistory())
            {
                var result = MessageBox.Show("Are you sure you want to save the changes?\nIf you click save all the chat history will be cleared.", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }
            }
            _chatService.AddSystemSetup(rtbSystemSetup.Text.Trim());
            _appSettings.SystemMessage = rtbSystemSetup.Text.Trim();
            _appSettings.TTSVoice = cmbAvailableVoices.SelectedItem?.ToString() ?? _appSettings.TTSVoice;
            _appSettings.UseTTS = chkUseTTS.Checked;
            _settingsService.SaveSettings();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
