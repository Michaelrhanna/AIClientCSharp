using AIClient.Model.Interface;

namespace AIClient
{
    public partial class FormMain : Form
    {
        private readonly IChatService _chatService;
        public FormMain(IChatService chatService)
        {
            _chatService = chatService;
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserInput.Text.Trim()))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }
            _chatService.SendAsync(txtUserInput.Text.Trim()).ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    MessageBox.Show($"Error: {t.Exception.InnerException?.Message}");
                }
                else
                {
                    Invoke(() =>
                    {
                        rtbChat.AppendText($"User: {txtUserInput.Text.Trim()}\r\n");
                        rtbChat.AppendText($"AI: {t.Result}\r\n\r\n");
                        txtUserInput.Clear();
                    });
                }
            });
        }

        private void btnSetupSystem_Click(object sender, EventArgs e)
        {
            var result = new FormSetup(_chatService).ShowDialog();
            if (result == DialogResult.OK)
                rtbChat.Clear();
        }
    }
}
