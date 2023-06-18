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
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            btnCancel = new Button();
            btnProcess = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            tbTemplateFolderPath = new TextBox();
            tbDestinationFolderPath = new TextBox();
            splitContainer1 = new SplitContainer();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
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
            tableLayoutPanel1.Size = new Size(414, 24);
            tableLayoutPanel1.TabIndex = 4;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.AutoScroll = true;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Location = new Point(12, 48);
            panel1.Name = "panel1";
            panel1.Size = new Size(420, 122);
            panel1.TabIndex = 5;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(276, 176);
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
            btnProcess.Location = new Point(357, 176);
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
            tbTemplateFolderPath.Location = new Point(3, 4);
            tbTemplateFolderPath.Name = "tbTemplateFolderPath";
            tbTemplateFolderPath.PlaceholderText = "Click to select template folder";
            tbTemplateFolderPath.ReadOnly = true;
            tbTemplateFolderPath.Size = new Size(203, 23);
            tbTemplateFolderPath.TabIndex = 9;
            tbTemplateFolderPath.Click += tbTemplateFolderPath_Click;
            // 
            // tbDestinationFolderPath
            // 
            tbDestinationFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbDestinationFolderPath.Location = new Point(3, 4);
            tbDestinationFolderPath.Name = "tbDestinationFolderPath";
            tbDestinationFolderPath.PlaceholderText = "Click to select destination folder";
            tbDestinationFolderPath.ReadOnly = true;
            tbDestinationFolderPath.Size = new Size(201, 23);
            tbDestinationFolderPath.TabIndex = 10;
            tbDestinationFolderPath.Click += tbDestinationFolderPath_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(12, 12);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tbTemplateFolderPath);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tbDestinationFolderPath);
            splitContainer1.Size = new Size(420, 30);
            splitContainer1.SplitterDistance = 209;
            splitContainer1.SplitterIncrement = 8;
            splitContainer1.TabIndex = 11;
            // 
            // frmCreateFolder
            // 
            AcceptButton = btnProcess;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            CausesValidation = false;
            ClientSize = new Size(444, 211);
            Controls.Add(splitContainer1);
            Controls.Add(btnProcess);
            Controls.Add(btnCancel);
            Controls.Add(panel1);
            MinimumSize = new Size(350, 200);
            Name = "frmCreateFolder";
            Text = "Folder Templates: Create Folder";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button btnCancel;
        private Button btnProcess;
        private FolderBrowserDialog folderBrowserDialog1;
        private TextBox tbTemplateFolderPath;
        private TextBox tbDestinationFolderPath;
        private SplitContainer splitContainer1;
    }
}