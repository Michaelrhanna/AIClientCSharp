using AIClient.Model.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AIClient
{
    public partial class FormSetup : Form
    {
        private readonly IChatService _chatService;
        public FormSetup(IChatService chatService)
        {
            InitializeComponent();
            _chatService = chatService;
            rtbSystemSetup.Text = _chatService.GetSystemPrompt();
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
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
