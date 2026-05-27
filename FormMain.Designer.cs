namespace AIClient
{
    partial class FormMain
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            txtUserInput = new TextBox();
            rtbChat = new RichTextBox();
            btnSend = new Button();
            btnSetupSystem = new Button();
            btnNewConversation = new Button();
            SuspendLayout();
            // 
            // txtUserInput
            // 
            txtUserInput.Location = new Point(12, 403);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(641, 23);
            txtUserInput.TabIndex = 0;
            // 
            // rtbChat
            // 
            rtbChat.Location = new Point(12, 12);
            rtbChat.Name = "rtbChat";
            rtbChat.Size = new Size(715, 385);
            rtbChat.TabIndex = 1;
            rtbChat.Text = "";
            // 
            // btnSend
            // 
            btnSend.Location = new Point(659, 403);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(56, 23);
            btnSend.TabIndex = 2;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += btnSend_Click;
            // 
            // btnSetupSystem
            // 
            btnSetupSystem.Location = new Point(736, 12);
            btnSetupSystem.Name = "btnSetupSystem";
            btnSetupSystem.Size = new Size(86, 23);
            btnSetupSystem.TabIndex = 3;
            btnSetupSystem.Text = "Setup";
            btnSetupSystem.UseVisualStyleBackColor = true;
            btnSetupSystem.Click += btnSetupSystem_Click;
            // 
            // btnNewConversation
            // 
            btnNewConversation.Location = new Point(736, 41);
            btnNewConversation.Name = "btnNewConversation";
            btnNewConversation.Size = new Size(88, 41);
            btnNewConversation.TabIndex = 4;
            btnNewConversation.Text = "New Conversation";
            btnNewConversation.UseVisualStyleBackColor = true;
            btnNewConversation.Click += btnNewConversation_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(834, 450);
            Controls.Add(btnNewConversation);
            Controls.Add(btnSetupSystem);
            Controls.Add(btnSend);
            Controls.Add(rtbChat);
            Controls.Add(txtUserInput);
            Name = "FormMain";
            Text = "AI Client";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtUserInput;
        private RichTextBox rtbChat;
        private Button btnSend;
        private Button btnSetupSystem;
        private Button btnNewConversation;
    }
}
