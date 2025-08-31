using FolderTemplates.Data;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Text;

namespace FolderTemplates.App
{
    public partial class frmCreateFolder : Form
    {
        private readonly NameValueCollection? appSettings;
        private readonly string cmdPath;
        private readonly string shortcutNameProcess = "Folder Template - Process";
        private readonly string shortcutNameEdit = "Folder Template - Edit";
        //private readonly string shortcutNameCreate = "Folder Template - Create";

        public frmCreateFolder(string? sourcePath, string? targetPath)
        {
            InitializeComponent();

            bool sendToExists = Shortcut.SendToShortcutExists(shortcutNameProcess);
            mnuRemoveSendTo.Visible = sendToExists;
            mnuAddSendTo.Visible = !sendToExists;

            string defaultCmdPath = "FolderTemplates.Console.exe";
            appSettings = ConfigurationManager.AppSettings;
            cmdPath = (appSettings != null && appSettings["cmdPath"] != null) ? appSettings["cmdPath"] ?? defaultCmdPath : defaultCmdPath;

            tbTemplateFolderPath.Text = sourcePath;
            tbDestinationFolderPath.Text = targetPath;

            CreateForm(cmdPath, sourcePath, targetPath);
        }

        private void CreateForm(string cmdPath, string? sourceFolder, string? targetFolder = null)
        {
            if (!string.IsNullOrWhiteSpace(sourceFolder))
            {
                TemplateInfo templateInfo = getTemplateInfo(cmdPath, sourceFolder);
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

                List<ParameterInfo> parameters = getParameterInfo(cmdPath, sourceFolder);
                RenderForm(parameters);

                btnProcess.Enabled = true;
                btnEditTemplate.Enabled = true;
            } else
            {
                btnProcess.Enabled = false;
                btnEditTemplate.Enabled = false;
            }
        }

        private static List<ParameterInfo> getParameterInfo(string cmdPath, string sourceFolder)
        {
            string? command = Path.GetFullPath(cmdPath);
            string? commandParameters = "-sourceFolder \"" + sourceFolder + "\" -listParams json -nowait";
            string? workingDirectory = Path.GetDirectoryName(command);
            string? strOutput = ExecuteCommand(command, commandParameters, workingDirectory);
            List<ParameterInfo> parameters = JsonConvert.DeserializeObject<List<ParameterInfo>>(strOutput ?? "") ?? new List<ParameterInfo>();
            return parameters;
        }

        private static TemplateInfo getTemplateInfo(string cmdPath, string sourceFolder)
        {
            string? command = Path.GetFullPath(cmdPath);
            string? commandParameters = "-sourceFolder \"" + sourceFolder + "\" -getTemplateInfo json -nowait";
            string? workingDirectory = Path.GetDirectoryName(command);
            string? strOutput = ExecuteCommand(command, commandParameters, workingDirectory);
            TemplateInfo templateInfo = JsonConvert.DeserializeObject<TemplateInfo>(strOutput ?? "") ?? new TemplateInfo();
            return templateInfo;
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

        private static string? ExecuteCommand(string command, string commandParameters, string? workingDirectory)
        {
            //Create process
            System.Diagnostics.Process process = new();

            //strCommand is path and file name of command to run
            process.StartInfo.FileName = command;

            //strCommandParameters are parameters to pass to program
            process.StartInfo.Arguments = commandParameters;

            process.StartInfo.UseShellExecute = false;

            //Set output of program to be written to process output stream
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //Optional
            process.StartInfo.WorkingDirectory = workingDirectory;

            //Start the process
            process.Start();

            //Get program output
            string strOutput = process.StandardOutput.ReadToEnd();

            //Wait for process to finish
            process.WaitForExit();

            return strOutput;
        }

        private static void ExecuteCommandAsync(string command, string commandParameters, string? workingDirectory, Control? ctrlOutput = null, EventHandler? onExited = null)
        {
            //Create process
            System.Diagnostics.Process process = new();

            //strCommand is path and file name of command to run
            process.StartInfo.FileName = command;

            //strCommandParameters are parameters to pass to program
            process.StartInfo.Arguments = commandParameters;

            process.StartInfo.UseShellExecute = false;

            //Set output of program to be written to process output stream
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            //Optional
            process.StartInfo.WorkingDirectory = workingDirectory;

            if (ctrlOutput != null)
            {
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                        WriteDataToControl(e.Data, ctrlOutput);
                });

                process.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
                {
                    if (!String.IsNullOrEmpty(e.Data))
                        WriteDataToControl(e.Data, ctrlOutput);
                });
            }

            if (onExited != null)
                process.Exited += onExited;

            //Start the process
            process.Start();
            process.BeginOutputReadLine();
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

            ExecuteCommandAsync(command, commandParameters, workingDirectory, this.tbOutput, new EventHandler((sender, e) =>
            {
                this.BeginInvoke(new Action(() =>
                {
                    if (this.cbExitImmediately.Checked)
                        this.Close();
                }));
            }));
        }

        private static void Restart(string sourceFolder, string targetFolder, string[] args)
        {
            // Start a new process with the new command line arguments
            try
            {
                // Get the current executable path
                string executablePath = Application.ExecutablePath;

                // Build the new arguments string
                string newArgs = args != null ? string.Join(" ", args) : string.Empty;

                // Add the source folder to the arguments
                if (!string.IsNullOrEmpty(sourceFolder))
                    newArgs += " -sourceFolder \"" + sourceFolder + "\"";

                // Add the target folder to the arguments
                if (!string.IsNullOrEmpty(targetFolder))
                    newArgs += " -targetFolder \"" + targetFolder + "\"";

                // Create process start info with quoted executable path
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = executablePath,
                    Arguments = newArgs,
                    UseShellExecute = true
                };

                // Start the new instance
                Process.Start(startInfo);

                // Exit the current instance
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to restart application: {ex.Message}",
                               "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string sourceFolder = tbTemplateFolderPath.Text;
            string targetFolder = tbDestinationFolderPath.Text;
            ProcessFolder(cmdPath, sourceFolder, GetParameters(), !string.IsNullOrWhiteSpace(targetFolder) ? targetFolder : null);
        }

        private void btnEditTemplate_Click(object sender, EventArgs e)
        {
            string sourceFolder = tbTemplateFolderPath.Text;
            string targetFolder = tbDestinationFolderPath.Text;
            Restart(sourceFolder, targetFolder, new string[] { "-edit" });
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
            bool sendToExistsProcess = Shortcut.SendToShortcutExists(shortcutNameProcess);
            if (!sendToExistsProcess)
            {
                Shortcut.CreateSendToShortcut(shortcutNameProcess);
                mnuRemoveSendTo.Visible = true;
                mnuAddSendTo.Visible = false;
            }
            bool sendToExistsEdit = Shortcut.SendToShortcutExists(shortcutNameEdit);
            if (!sendToExistsEdit)
            {
                Shortcut.CreateSendToShortcut(shortcutNameEdit, new string[] { "-edit", "-sourceFolder" });
                mnuRemoveSendTo.Visible = true;
                mnuAddSendTo.Visible = false;
            }
        }

        private void mnuRemoveSendTo_Click(object sender, EventArgs e)
        {
            if (Shortcut.SendToShortcutExists(shortcutNameProcess))
            {
                Shortcut.DeleteSendToShortcut(shortcutNameProcess);
                mnuRemoveSendTo.Visible = false;
                mnuAddSendTo.Visible = true;
            }
        }
    }
}