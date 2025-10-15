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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCreateFolder));
            tableLayoutPanel1 = new TableLayoutPanel();
            panel1 = new Panel();
            btnCancel = new Button();
            btnProcess = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            tbTemplateFolderPath = new TextBox();
            tbDestinationFolderPath = new TextBox();
            splitContainer1 = new SplitContainer();
            tbOutput = new TextBox();
            splitContainer2 = new SplitContainer();
            cbExitImmediately = new CheckBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            mnuOptions = new ToolStripMenuItem();
            mnuAddSendTo = new ToolStripMenuItem();
            mnuRemoveSendTo = new ToolStripMenuItem();
            mnuExit = new ToolStripMenuItem();
            btnEditTemplate = new Button();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            menuStrip1.SuspendLayout();
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
            tableLayoutPanel1.Location = new Point(3, 4);
            tableLayoutPanel1.Margin = new Padding(3, 4, 3, 4);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 1;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 32F));
            tableLayoutPanel1.Size = new Size(594, 32);
            tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.Controls.Add(tableLayoutPanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(601, 164);
            panel1.TabIndex = 7;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.DialogResult = DialogResult.Cancel;
            btnCancel.Location = new Point(305, 425);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(86, 31);
            btnCancel.TabIndex = 6;
            btnCancel.Text = "Close";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnProcess
            // 
            btnProcess.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnProcess.DialogResult = DialogResult.OK;
            btnProcess.Location = new Point(530, 425);
            btnProcess.Margin = new Padding(3, 4, 3, 4);
            btnProcess.Name = "btnProcess";
            btnProcess.Size = new Size(86, 31);
            btnProcess.TabIndex = 5;
            btnProcess.Text = "Process";
            btnProcess.UseVisualStyleBackColor = true;
            btnProcess.Click += btnProcess_Click;
            // 
            // tbTemplateFolderPath
            // 
            tbTemplateFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbTemplateFolderPath.Location = new Point(3, 5);
            tbTemplateFolderPath.Margin = new Padding(3, 4, 3, 4);
            tbTemplateFolderPath.Name = "tbTemplateFolderPath";
            tbTemplateFolderPath.PlaceholderText = "Click to select template folder";
            tbTemplateFolderPath.ReadOnly = true;
            tbTemplateFolderPath.Size = new Size(228, 27);
            tbTemplateFolderPath.TabIndex = 1;
            tbTemplateFolderPath.Click += tbTemplateFolderPath_Click;
            tbTemplateFolderPath.KeyPress += tbTemplateFolderPath_KeyPress;
            // 
            // tbDestinationFolderPath
            // 
            tbDestinationFolderPath.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbDestinationFolderPath.Location = new Point(3, 5);
            tbDestinationFolderPath.Margin = new Padding(3, 4, 3, 4);
            tbDestinationFolderPath.Name = "tbDestinationFolderPath";
            tbDestinationFolderPath.PlaceholderText = "Click to select destination folder";
            tbDestinationFolderPath.ReadOnly = true;
            tbDestinationFolderPath.Size = new Size(289, 27);
            tbDestinationFolderPath.TabIndex = 2;
            tbDestinationFolderPath.Click += tbDestinationFolderPath_Click;
            tbDestinationFolderPath.KeyPress += tbDestinationFolderPath_KeyPress;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(14, 36);
            splitContainer1.Margin = new Padding(3, 4, 3, 4);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(tbTemplateFolderPath);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(tbDestinationFolderPath);
            splitContainer1.Size = new Size(601, 40);
            splitContainer1.SplitterDistance = 267;
            splitContainer1.SplitterIncrement = 8;
            splitContainer1.SplitterWidth = 5;
            splitContainer1.TabIndex = 11;
            splitContainer1.TabStop = false;
            // 
            // tbOutput
            // 
            tbOutput.BorderStyle = BorderStyle.None;
            tbOutput.Dock = DockStyle.Fill;
            tbOutput.Location = new Point(0, 0);
            tbOutput.Margin = new Padding(3, 4, 3, 4);
            tbOutput.Multiline = true;
            tbOutput.Name = "tbOutput";
            tbOutput.ReadOnly = true;
            tbOutput.ScrollBars = ScrollBars.Vertical;
            tbOutput.Size = new Size(601, 164);
            tbOutput.TabIndex = 8;
            tbOutput.TabStop = false;
            // 
            // splitContainer2
            // 
            splitContainer2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer2.Location = new Point(14, 84);
            splitContainer2.Margin = new Padding(3, 4, 3, 4);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(panel1);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tbOutput);
            splitContainer2.Size = new Size(601, 333);
            splitContainer2.SplitterDistance = 164;
            splitContainer2.SplitterWidth = 5;
            splitContainer2.TabIndex = 13;
            splitContainer2.TabStop = false;
            // 
            // cbExitImmediately
            // 
            cbExitImmediately.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            cbExitImmediately.AutoSize = true;
            cbExitImmediately.Checked = true;
            cbExitImmediately.CheckState = CheckState.Checked;
            cbExitImmediately.Location = new Point(17, 429);
            cbExitImmediately.Margin = new Padding(3, 4, 3, 4);
            cbExitImmediately.Name = "cbExitImmediately";
            cbExitImmediately.Size = new Size(137, 24);
            cbExitImmediately.TabIndex = 4;
            cbExitImmediately.Text = "Exit When Done";
            cbExitImmediately.UseVisualStyleBackColor = true;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(7, 3, 0, 3);
            menuStrip1.Size = new Size(628, 30);
            menuStrip1.TabIndex = 14;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuOptions, mnuExit });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(46, 24);
            fileToolStripMenuItem.Text = "File";
            // 
            // mnuOptions
            // 
            mnuOptions.DropDownItems.AddRange(new ToolStripItem[] { mnuAddSendTo, mnuRemoveSendTo });
            mnuOptions.Name = "mnuOptions";
            mnuOptions.Size = new Size(144, 26);
            mnuOptions.Text = "Options";
            // 
            // mnuAddSendTo
            // 
            mnuAddSendTo.Name = "mnuAddSendTo";
            mnuAddSendTo.Size = new Size(239, 26);
            mnuAddSendTo.Text = "Add to Send To";
            mnuAddSendTo.Click += mnuAddSendTo_Click;
            // 
            // mnuRemoveSendTo
            // 
            mnuRemoveSendTo.Name = "mnuRemoveSendTo";
            mnuRemoveSendTo.Size = new Size(239, 26);
            mnuRemoveSendTo.Text = "Remove from Send To";
            mnuRemoveSendTo.Click += mnuRemoveSendTo_Click;
            // 
            // mnuExit
            // 
            mnuExit.Name = "mnuExit";
            mnuExit.Size = new Size(144, 26);
            mnuExit.Text = "Exit";
            mnuExit.Click += exitToolStripMenuItem_Click;
            // 
            // btnEditTemplate
            // 
            btnEditTemplate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnEditTemplate.DialogResult = DialogResult.OK;
            btnEditTemplate.Location = new Point(397, 425);
            btnEditTemplate.Margin = new Padding(3, 4, 3, 4);
            btnEditTemplate.Name = "btnEditTemplate";
            btnEditTemplate.Size = new Size(127, 31);
            btnEditTemplate.TabIndex = 15;
            btnEditTemplate.Text = "Edit Template";
            btnEditTemplate.UseVisualStyleBackColor = true;
            btnEditTemplate.Click += btnEditTemplate_Click;
            // 
            // frmCreateFolder
            // 
            AcceptButton = btnProcess;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = btnCancel;
            CausesValidation = false;
            ClientSize = new Size(628, 470);
            Controls.Add(btnEditTemplate);
            Controls.Add(cbExitImmediately);
            Controls.Add(splitContainer2);
            Controls.Add(splitContainer1);
            Controls.Add(btnProcess);
            Controls.Add(btnCancel);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(397, 251);
            Name = "frmCreateFolder";
            Text = "Folder Templates: Create Folder";
            Load += frmCreateFolder_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private TextBox tbOutput;
        private SplitContainer splitContainer2;
        private CheckBox cbExitImmediately;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem mnuOptions;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem mnuAddSendTo;
        private ToolStripMenuItem mnuRemoveSendTo;
        private Button btnEditTemplate;
    }
}