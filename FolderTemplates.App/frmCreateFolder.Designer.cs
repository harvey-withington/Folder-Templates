namespace FolderTemplates.App
{
    partial class frmCreateFolder
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
            progressBar1 = new ProgressBar();
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            btnCancel = new Button();
            btnProcess = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            tbTemplateFolderPath = new TextBox();
            tbDestinationFolderPath = new TextBox();
            folderBrowserDialog2 = new FolderBrowserDialog();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            progressBar1.Location = new Point(12, 200);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(163, 23);
            progressBar1.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 3;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel1.Size = new Size(409, 24);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(12, 41);
            panel1.Name = "panel1";
            panel1.Size = new Size(415, 153);
            panel1.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(271, 200);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(75, 23);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnProcess
            // 
            btnProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnProcess.DialogResult = DialogResult.OK;
            btnProcess.Location = new Point(352, 200);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(75, 23);
            btnProcess.TabIndex = 8;
            btnProcess.Text = "Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // tbTemplateFolderPath
            // 
            tbTemplateFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTemplateFolderPath.Location = new Point(12, 12);
            tbTemplateFolderPath.Name = "tbTemplateFolderPath";
            tbTemplateFolderPath.PlaceholderText = "Click to select template folder";
            tbTemplateFolderPath.ReadOnly = true;
            tbTemplateFolderPath.Size = new Size(206, 23);
            tbTemplateFolderPath.TabIndex = 9;
            tbTemplateFolderPath.Click += tbTemplateFolderPath_Click;
            // 
            // tbDestinationFolderPath
            // 
            tbDestinationFolderPath.Location = new Point(224, 12);
            tbDestinationFolderPath.Name = "tbDestinationFolderPath";
            tbDestinationFolderPath.PlaceholderText = "Click to select destination folder";
            tbDestinationFolderPath.ReadOnly = true;
            tbDestinationFolderPath.Size = new Size(203, 23);
            tbDestinationFolderPath.TabIndex = 10;
            tbDestinationFolderPath.Click += tbDestinationFolderPath_Click;
            // 
            // frmCreateFolder
            // 
            AcceptButton = btnProcess;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            CausesValidation = false;
            ClientSize = new Size(439, 235);
            Controls.Add(tbDestinationFolderPath);
            Controls.Add(tbTemplateFolderPath);
            Controls.Add(btnProcess);
            Controls.Add(btnCancel);
            Controls.Add(panel1);
            Controls.Add(progressBar1);
            MinimumSize = new Size(378, 144);
            Name = "frmCreateFolder";
            Text = "Folder Templates: Create Folder";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ProgressBar progressBar1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button btnCancel;
        private Button btnProcess;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox tbTemplateFolderPath;
        private TextBox tbDestinationFolderPath;
        private FolderBrowserDialog folderBrowserDialog2;
    }
}