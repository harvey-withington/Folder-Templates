using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using FolderTemplates.CommandLine;
using FolderTemplates.API.Console;
using FolderTemplates.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;

namespace FolderTemplates.App
{
    public partial class frmCreateFolder : Form
    {
        private readonly NameValueCollection? appSettings;
        readonly ConsoleCommandLine cmd;
        private readonly string cmdPath;
        private readonly string shortcutName = "Process with Folder Templates";

        public frmCreateFolder(string[] args)
        {
            InitializeComponent();

            bool sendToExists = Shortcut.SendToShortcutExists(shortcutName);
            mnuRemoveSendTo.Visible = sendToExists;
            mnuAddSendTo.Visible = !sendToExists;

            string defaultCmdPath = "FolderTemplates.Console.exe";
            appSettings = ConfigurationManager.AppSettings;
            cmdPath = (appSettings != null && appSettings["cmdPath"] != null) ? appSettings["cmdPath"] ?? defaultCmdPath : defaultCmdPath;

            cmd = new();
            cmd.RegisterParameter(new CommandLineParameter("sourceFolder", false, "The path of the Template Folder to process"));
            //cmd.RegisterParameter(new CommandLineParameter("templateFile", false, "The path of the Template Folder Definition file to apply"));
            cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
            cmd.Parse(args ?? Array.Empty<string>(), true, "sourceFolder");

            if (cmd.ParsedSuccessfully)
            {
                // Get the source, target and template paths from supplied parameters
                string? initialSourcePath = cmd["sourceFolder"].Exists ? cmd["sourceFolder"].Value : "";
                string? initialTargetPath = cmd["targetFolder"].Exists ? cmd["targetFolder"].Value : "";
                tbTemplateFolderPath.Text = initialSourcePath;
                tbDestinationFolderPath.Text = initialTargetPath;

                CreateForm(cmdPath, initialSourcePath, initialTargetPath);
            }
        }

        private void CreateForm(string cmdPath, string? sourceFolder, string? targetFolder = null)
        {
            if (!string.IsNullOrWhiteSpace(sourceFolder))
            {
                TemplateInfo templateInfo = FolderTemplatesConsoleAPI.getTemplateInfo(cmdPath, sourceFolder);
                if (string.IsNullOrWhiteSpace(targetFolder))
                {
                    if (!string.IsNullOrWhiteSpace(templateInfo.DefaultTargetPath))
                    {
                        string defaultTargetFolder = Path.GetFullPath(templateInfo.DefaultTargetPath, Path.GetDirectoryName(sourceFolder) ?? "");
                        tbDestinationFolderPath.Text = defaultTargetFolder;
                    }
                }
                else
                {
                    tbDestinationFolderPath.Text = targetFolder;
                }

                List<ParameterInfo> parameters = FolderTemplatesConsoleAPI.getParameterInfo(cmdPath, sourceFolder);
                RenderForm(parameters);
            }
        }

        private void RenderForm(List<ParameterInfo> parameters)
        {
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Clear();

            int row = 0;
            foreach (ParameterInfo parameter in parameters)
            {
                Label label1 = new() { Text = parameter.Prompt, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Tag = parameter };
                tableLayoutPanel1.Controls.Add(label1, 0, row);

                TextBox textbox1 = new() { PlaceholderText = parameter.Placeholder, Dock = DockStyle.Fill, TabIndex = 0, Tag = parameter };
                tableLayoutPanel1.Controls.Add(textbox1, 1, row);

                row++;
            }
            tableLayoutPanel1.Focus();
        }

        private static void WriteDataToControl(string? Data, Control ctrl)
        {
            if (Data != null)
                ctrl.BeginInvoke(new Action(() => ctrl.Text += (Environment.NewLine + Data)));
        }

        private NameValueCollection GetParameters()
        {
            NameValueCollection parameters = new();

            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is TextBox tb)
                {
                    ParameterInfo? param = tb != null ? tb.Tag as ParameterInfo : null;
                    string? val = tb?.Text;
                    if (param != null && !string.IsNullOrWhiteSpace(val))
                        parameters.Add(param.Name, val);
                }
            }

            return parameters;
        }

        private void ProcessFolder(string cmdPath, string sourceFolder, NameValueCollection? parameters = null, string? targetFolder = null)
        {
            string? command = Path.GetFullPath(cmdPath);
            string? workingDirectory = Path.GetDirectoryName(command);
            string commandParameters = "-sourceFolder \"" + sourceFolder + "\" -nowait -noprompt";

            if (targetFolder != null)
                commandParameters += " -targetFolder \"" + targetFolder + "\"";

            if (parameters != null)
                foreach (string param in parameters.Keys)
                {
                    string? val = parameters[param];
                    commandParameters += !string.IsNullOrWhiteSpace(val) ? " -" + param + " \"" + val + "\"" : "";
                }
            DataReceivedEventHandler dataReceiver = new((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                        WriteDataToControl(e.Data, this.tbOutput);
                });
            FolderTemplatesConsoleAPI.ExecuteCommandAsync(command, commandParameters, workingDirectory, dataReceiver, new EventHandler((sender, e) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (this.cbExitImmediately.Checked)
                        this.Close();
                }));
            }));
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string sourceFolder = tbTemplateFolderPath.Text;
            string targetFolder = tbDestinationFolderPath.Text;
            ProcessFolder(cmdPath, sourceFolder, GetParameters(), !string.IsNullOrWhiteSpace(targetFolder) ? targetFolder : null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbTemplateFolderPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbTemplateFolderPath.Text = folderBrowserDialog1.SelectedPath;
                CreateForm(cmdPath, tbTemplateFolderPath.Text);
            }
        }

        private void tbTemplateFolderPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Space))
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    tbTemplateFolderPath.Text = folderBrowserDialog1.SelectedPath;
                    CreateForm(cmdPath, tbTemplateFolderPath.Text);
                }
        }

        private void tbDestinationFolderPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbDestinationFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void tbDestinationFolderPath_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ((char)Keys.Space))
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    tbDestinationFolderPath.Text = folderBrowserDialog1.SelectedPath;
                }
        }

        private void frmCreateFolder_Load(object sender, EventArgs e)
        {
            this.ActiveControl = tbTemplateFolderPath;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mnuAddSendTo_Click(object sender, EventArgs e)
        {
            bool sendToExists = Shortcut.SendToShortcutExists(shortcutName);
            if (!sendToExists)
            {
                Shortcut.CreateSendToShortcut(shortcutName);
                mnuRemoveSendTo.Visible = true;
                mnuAddSendTo.Visible = false;
            }
        }

        private void mnuRemoveSendTo_Click(object sender, EventArgs e)
        {
            if (Shortcut.SendToShortcutExists(shortcutName))
            {
                Shortcut.DeleteSendToShortcut(shortcutName);
                mnuRemoveSendTo.Visible = false;
                mnuAddSendTo.Visible = true;
            }
        }
    }
}