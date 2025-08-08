using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;
using FolderTemplates.CommandLine;

namespace FolderTemplates.App
{

    public partial class frmCreateTemplate : Form
    {
        string _sourceFolderPath = string.Empty;
        public frmCreateTemplate(string sourceFolderPath)
        {
            InitializeComponent();
            _sourceFolderPath = sourceFolderPath;
        }

        private void RefreshParametersGrid()
        {
            dgvParameters.DataSource = null;
            dgvParameters.DataSource = new BindingList<TemplateParameter>(parameters);
        }

        public List<TemplateParameter> GetTemplateParameters()
        {
            return new List<TemplateParameter>(parameters);
        }

        private static bool CreateTemplate(string templateName, string templateDescription,
                                     List<TemplateParameter> parameters, string sourceFolderPath)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(templateName))
                    throw new ArgumentException("Template name cannot be empty");

                if (!Directory.Exists(sourceFolderPath))
                    throw new DirectoryNotFoundException($"Source folder not found: {sourceFolderPath}");

                // Create the .ft folder in the source directory if it doesn't exist
                string ftFolderPath = Path.Combine(sourceFolderPath, ".ft");
                if (!Directory.Exists(ftFolderPath))
                {
                    DirectoryInfo ftDir = Directory.CreateDirectory(ftFolderPath);

                    // Set the folder as hidden
                    ftDir.Attributes |= FileAttributes.Hidden;
                }

                // Create template configuration
                var templateConfig = new
                {
                    Name = templateName,
                    Description = templateDescription,
                    Parameters = parameters
                };

                // Serialize the template configuration to JSON
                string templateJson = System.Text.Json.JsonSerializer.Serialize(templateConfig,
                    new System.Text.Json.JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                // Save the template configuration to the .ft folder
                string templateConfigPath = Path.Combine(ftFolderPath, "template.json");
                File.WriteAllText(templateConfigPath, templateJson);

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error creating template: {ex.Message}");
                return false;
            }
        }

        private void btnAddParameter_Click(object sender, EventArgs e)
        {
            TemplateParameter newParam = new TemplateParameter();
            parameters.Add(newParam);
            RefreshParametersGrid();
        }

        private void btnRemoveParameter_Click(object sender, EventArgs e)
        {
            if (dgvParameters.SelectedRows.Count > 0)
            {
                int index = dgvParameters.SelectedRows[0].Index;
                if (index >= 0 && index < parameters.Count)
                {
                    parameters.RemoveAt(index);
                    RefreshParametersGrid();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate form
            if (string.IsNullOrWhiteSpace(txtTemplateName.Text))
            {
                MessageBox.Show("Please enter a template name.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // TODO: Save template and parameters to storage
            string templateName = txtTemplateName.Text;
            string templateDescription = txtTemplateDescription.Text;
            List<TemplateParameter> parameters = GetTemplateParameters();
            CreateTemplate(templateName, templateDescription, parameters, _sourceFolderPath);

            MessageBox.Show("Template saved successfully!", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}