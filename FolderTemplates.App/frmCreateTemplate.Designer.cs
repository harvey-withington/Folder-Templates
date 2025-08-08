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
            ((ISupportInitialize)dgvParameters).BeginInit();
            SuspendLayout();
            // 
            // txtTemplateName
            // 
            txtTemplateName.Location = new Point(177, 12);
            txtTemplateName.Name = "txtTemplateName";
            txtTemplateName.Size = new Size(273, 23);
            txtTemplateName.TabIndex = 1;
            // 
            // txtTemplateDescription
            // 
            txtTemplateDescription.Location = new Point(177, 42);
            txtTemplateDescription.Name = "txtTemplateDescription";
            txtTemplateDescription.Size = new Size(273, 23);
            txtTemplateDescription.TabIndex = 3;
            // 
            // dgvParameters
            // 
            dgvParameters.AllowUserToAddRows = false;
            dgvParameters.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvParameters.BackgroundColor = SystemColors.Control;
            dgvParameters.Location = new Point(20, 95);
            dgvParameters.Name = "dgvParameters";
            dgvParameters.Size = new Size(814, 228);
            dgvParameters.TabIndex = 5;
            // 
            // btnAddParameter
            // 
            btnAddParameter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnAddParameter.Location = new Point(21, 329);
            btnAddParameter.Name = "btnAddParameter";
            btnAddParameter.Size = new Size(124, 23);
            btnAddParameter.TabIndex = 6;
            btnAddParameter.Text = "Add Parameter";
            btnAddParameter.Click += btnAddParameter_Click;
            // 
            // btnRemoveParameter
            // 
            btnRemoveParameter.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnRemoveParameter.Location = new Point(151, 329);
            btnRemoveParameter.Name = "btnRemoveParameter";
            btnRemoveParameter.Size = new Size(129, 23);
            btnRemoveParameter.TabIndex = 7;
            btnRemoveParameter.Text = "Remove Parameter";
            btnRemoveParameter.Click += btnRemoveParameter_Click;
            // 
            // btnSave
            // 
            btnSave.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnSave.Location = new Point(731, 358);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(103, 23);
            btnSave.TabIndex = 8;
            btnSave.Text = "Save";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnCancel.Location = new Point(622, 358);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(103, 23);
            btnCancel.TabIndex = 9;
            btnCancel.Text = "Cancel";
            btnCancel.Click += btnCancel_Click;
            // 
            // lblName
            // 
            lblName.AutoSize = true;
            lblName.Location = new Point(20, 20);
            lblName.Name = "lblName";
            lblName.Size = new Size(94, 15);
            lblName.TabIndex = 0;
            lblName.Text = "Template Name:";
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(20, 50);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(122, 15);
            lblDescription.TabIndex = 2;
            lblDescription.Text = "Template Description:";
            // 
            // lblParameters
            // 
            lblParameters.AutoSize = true;
            lblParameters.Location = new Point(21, 77);
            lblParameters.Name = "lblParameters";
            lblParameters.Size = new Size(121, 15);
            lblParameters.TabIndex = 4;
            lblParameters.Text = "Template Parameters:";
            // 
            // frmCreateTemplate
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(848, 393);
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
            MinimumSize = new Size(471, 294);
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
    }
}