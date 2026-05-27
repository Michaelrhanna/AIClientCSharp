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
            if(string.IsNullOrEmpty(txtUserInput.Text.Trim()))
            {
                MessageBox.Show("Please enter a message.");
                return;
            }

        }
    }
}
