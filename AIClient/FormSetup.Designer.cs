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
            label1 = new Label();
            label2 = new Label();
            cmbAvailableVoices = new ComboBox();
            chkUseTTS = new CheckBox();
            SuspendLayout();
            // 
            // rtbSystemSetup
            // 
            rtbSystemSetup.Location = new Point(12, 27);
            rtbSystemSetup.Name = "rtbSystemSetup";
            rtbSystemSetup.Size = new Size(536, 102);
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(94, 15);
            label1.TabIndex = 3;
            label1.Text = "System Message";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 164);
            label2.Name = "label2";
            label2.Size = new Size(108, 15);
            label2.TabIndex = 4;
            label2.Text = "Select voice for TTS";
            // 
            // cmbAvailableVoices
            // 
            cmbAvailableVoices.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbAvailableVoices.FormattingEnabled = true;
            cmbAvailableVoices.Location = new Point(12, 182);
            cmbAvailableVoices.Name = "cmbAvailableVoices";
            cmbAvailableVoices.Size = new Size(536, 23);
            cmbAvailableVoices.TabIndex = 5;
            // 
            // chkUseTTS
            // 
            chkUseTTS.AutoSize = true;
            chkUseTTS.Location = new Point(12, 142);
            chkUseTTS.Name = "chkUseTTS";
            chkUseTTS.Size = new Size(66, 19);
            chkUseTTS.TabIndex = 6;
            chkUseTTS.Text = "Use TTS";
            chkUseTTS.UseVisualStyleBackColor = true;
            // 
            // FormSetup
            // 
            AcceptButton = btnSave;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            ClientSize = new Size(560, 314);
            Controls.Add(chkUseTTS);
            Controls.Add(cmbAvailableVoices);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnCancel);
            Controls.Add(btnSave);
            Controls.Add(rtbSystemSetup);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormSetup";
            ShowInTaskbar = false;
            Text = "System Setup";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RichTextBox rtbSystemSetup;
        private Button btnSave;
        private Button btnCancel;
        private Label label1;
        private Label label2;
        private ComboBox cmbAvailableVoices;
        private CheckBox chkUseTTS;
    }
}