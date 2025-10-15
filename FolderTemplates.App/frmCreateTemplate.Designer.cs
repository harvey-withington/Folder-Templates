using System.ComponentModel;

namespace FolderTemplates.App
{
    partial class frmCreateTemplate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private List<TemplateParameter> parameters = new List<TemplateParameter>();
        private TextBox txtTemplateName;
        private TextBox txtTemplateDescription;
        private DataGridView dgvParameters;
        private Button btnAddParameter;
        private Button btnRemoveParameter;
        private Button btnSave;
        private Button btnCancel;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(frmCreateTemplate));
            txtTemplateName = new TextBox();
            txtTemplateDescription = new TextBox();
            dgvParameters = new DataGridView();
            btnAddParameter = new Button();
            btnRemoveParameter = new Button();
            btnSave = new Button();
            btnCancel = new Button();
            lblName = new Label();
            lblDescription = new Label();
            lblParameters = new Label();
            lblTargetPath = new Label();
            txtDefaultTargetPath = new TextBox();
            ((ISupportInitialize)dgvParameters).BeginInit();
            SuspendLayout();
            // 
            // txtTemplateName
            // 
            txtTemplateName.Location = new Point(202, 16);
            txtTemplateName.Margin = new Padding(3, 4, 3, 4);
            txtTemplateName.Name = "txtTemplateName";
            txtTemplateName.Size = new Size(277, 27);
            txtTemplateName.TabIndex = 1;
            // 
            // txtTemplateDescription
            // 
            txtTemplateDescription.Location = new Point(202, 56);
            txtTemplateDescription.Margin = new Padding(3, 4, 3, 4);
            txtTemplateDescription.Multiline = true;
            txtTemplateDescription.Name = "txtTemplateDescription";
            txtTemplateDescription.Size = new Size(751, 63);
            txtTemplateDescription.TabIndex = 5;
            // 
            // dgvParameters
            // 
            dgvParameters.AllowUserToAddRows = false;
            dgvParameters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvParameters.BackgroundColor = SystemColors.Control;
            dgvParameters.ColumnHeadersHeight = 29;
            dgvParameters.Location = new Point(23, 151);
            dgvParameters.Margin = new Padding(3, 4, 3, 4);
            dgvParameters.Name = "dgvParameters";
            dgvParameters.RowHeadersWidth = 51;
            dgvParameters.Size = new Size(930, 280);
            dgvParameters.TabIndex = 7;
            dgvParameters.CellPainting += dgvParameters_CellPainting;
            dgvParameters.CellValueChanged += dgvParameters_CellValueChanged;
            // 
            // btnAddParameter
            // 
            btnAddParameter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddParameter.Location = new Point(24, 439);
            btnAddParameter.Margin = new Padding(3, 4, 3, 4);
            btnAddParameter.Name = "btnAddParameter";
            btnAddParameter.Size = new Size(142, 31);
            btnAddParameter.TabIndex = 8;
            btnAddParameter.Text = "Add Parameter";
            btnAddParameter.Click += btnAddParameter_Click;
            // 
            // btnRemoveParameter
            // 
            btnRemoveParameter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRemoveParameter.Location = new Point(173, 439);
            btnRemoveParameter.Margin = new Padding(3, 4, 3, 4);
            btnRemoveParameter.Name = "btnRemoveParameter";
            btnRemoveParameter.Size = new Size(147, 31);
            btnRemoveParameter.TabIndex = 9;
            btnRemoveParameter.Text = "Remove Parameter";
            btnRemoveParameter.Click += btnRemoveParameter_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(835, 477);
            btnSave.Margin = new Padding(3, 4, 3, 4);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(118, 31);
            btnSave.TabIndex = 10;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(711, 477);
            btnCancel.Margin = new Padding(3, 4, 3, 4);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(118, 31);
            btnCancel.TabIndex = 11;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(24, 19);
            lblName.Name = "lblName";
            lblName.Size = new Size(118, 20);
            lblName.TabIndex = 0;
            lblName.Text = "Template Name:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(24, 59);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(154, 20);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Template Description:";
            // 
            // lblParameters
            // 
            lblParameters.AutoSize = true;
            lblParameters.Location = new Point(23, 127);
            lblParameters.Name = "lblParameters";
            lblParameters.Size = new Size(151, 20);
            lblParameters.TabIndex = 6;
            lblParameters.Text = "Template Parameters:";
            // 
            // lblTargetPath
            // 
            lblTargetPath.AutoSize = true;
            lblTargetPath.Location = new Point(541, 19);
            lblTargetPath.Name = "lblTargetPath";
            lblTargetPath.Size = new Size(85, 20);
            lblTargetPath.TabIndex = 2;
            lblTargetPath.Text = "Target Path:";
            // 
            // txtDefaultTargetPath
            // 
            txtDefaultTargetPath.Location = new Point(673, 16);
            txtDefaultTargetPath.Margin = new Padding(3, 4, 3, 4);
            txtDefaultTargetPath.Name = "txtDefaultTargetPath";
            txtDefaultTargetPath.Size = new Size(277, 27);
            txtDefaultTargetPath.TabIndex = 3;
            // 
            // frmCreateTemplate
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(969, 524);
            Controls.Add(lblTargetPath);
            Controls.Add(txtDefaultTargetPath);
            Controls.Add(lblName);
            Controls.Add(txtTemplateName);
            Controls.Add(lblDescription);
            Controls.Add(txtTemplateDescription);
            Controls.Add(lblParameters);
            Controls.Add(dgvParameters);
            Controls.Add(btnAddParameter);
            Controls.Add(btnRemoveParameter);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MinimumSize = new Size(536, 376);
            Name = "frmCreateTemplate";
            Text = "Create Template";
            ((ISupportInitialize)dgvParameters).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblName;
        private Label lblDescription;
        private Label lblParameters;
        private Label lblTargetPath;
        private TextBox txtDefaultTargetPath;
    }
}