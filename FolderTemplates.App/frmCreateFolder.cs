using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using FolderTemplates.Data;
using Newtonsoft.Json;

namespace FolderTemplates.App
{
    public partial class frmCreateFolder : Form
    {
        readonly NameValueCollection appSettings = ConfigurationManager.AppSettings;

        public frmCreateFolder()
        {
            InitializeComponent();

            CreateForm(appSettings["cmdPath"] ?? "");
        }

        private void CreateForm(string path)
        {
            string? command = Path.GetFullPath(path);
            string? commandParameters = "-sourceFolder \"D:\\Folder Templates\\Test Files\\_Basic Template - {projectName} - {projectVersion}\" -listParams json -nowait";
            string? workingDirectory = Path.GetDirectoryName(command);
            string? strOutput = ExecuteCommand(command, commandParameters, workingDirectory);
            List<ParameterInfo> parameters = JsonConvert.DeserializeObject<List<ParameterInfo>>(strOutput ?? "") ?? new List<ParameterInfo>();
            RenderForm(parameters);
        }

        private void RenderForm(List<ParameterInfo> parameters)
        {
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowCount = 0;

            int row = 0;
            foreach (ParameterInfo parameter in parameters)
            {
                Label label1 = new() { Text = parameter.Prompt, TextAlign = ContentAlignment.MiddleLeft, Dock = DockStyle.Fill, Tag = parameter };
                tableLayoutPanel1.Controls.Add(label1, 0, row);

                TextBox textbox1 = new() { PlaceholderText = parameter.Placeholder, Dock = DockStyle.Fill, Tag = parameter };
                tableLayoutPanel1.Controls.Add(textbox1, 1, row);

                row++;
            }
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

        private static string? ProcessFolder(string cmdPath, string sourceFolder, NameValueCollection? parameters = null, string? targetFolder = null)
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

            return ExecuteCommand(command, commandParameters, workingDirectory);
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string sourceFolder = tbTemplateFolderPath.Text;
            string targetFolder = tbDestinationFolderPath.Text;
            string? strOutput = ProcessFolder(appSettings["cmdPath"] ?? "", sourceFolder, GetParameters(), !string.IsNullOrWhiteSpace(targetFolder) ? targetFolder : null);
            MessageBox.Show(strOutput);
            this.Close();
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
            }
        }

        private void tbDestinationFolderPath_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog2.ShowDialog() == DialogResult.OK)
            {
                tbDestinationFolderPath.Text = folderBrowserDialog2.SelectedPath;
            }
        }
    }
}