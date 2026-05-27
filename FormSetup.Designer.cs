namespace AIClient
{
    partial class FormSetup
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            rtbSystemSetup = new RichTextBox();
            btnSave = new Button();
            btnCancel = new Button();
            SuspendLayout();
            // 
            // rtbSystemSetup
            // 
            rtbSystemSetup.Location = new Point(12, 12);
            rtbSystemSetup.Name = "rtbSystemSetup";
            rtbSystemSetup.Size = new Size(533, 251);
            rtbSystemSetup.TabIndex = 0;
            rtbSystemSetup.Text = "";
            // 
            // btnSave
            // 
            btnSave.Location = new Point(192, 279);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(75, 23);
            btnSave.TabIndex = 1;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new Point(289, 279);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // FormSetup
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(560, 314);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(rtbSystemSetup);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSetup";
            ShowInTaskbar = false;
            Text = "FormSetup";
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox rtbSystemSetup;
        private Button btnSave;
        private Button btnCancel;
    }
}