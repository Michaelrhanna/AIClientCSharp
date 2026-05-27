using AIClient.Model.Interface;

namespace AIClient
{
    public partial class FormMain : Form
    {
        private readonly IChatService _chatService;
        private readonly Func<FormSetup> _formSetupFactory;
        public FormMain(IChatService chatService, Func<FormSetup> formSetupFactory)
        {
            _chatService = chatService;
            InitializeComponent();
            _formSetupFactory = formSetupFactory;
        }

        private async void btnSend_Click(object sender, EventArgs e)
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
                rtbChat.Clear();
        }
    }
}
