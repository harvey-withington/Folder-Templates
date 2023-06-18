using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Forms;
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

        private void ProcessFolder(string cmdPath, string sourceFolder)
        {
            string? command = Path.GetFullPath(cmdPath);
            string commandParameters = "-sourceFolder \"" + sourceFolder + "\" -nowait -noprompt";
            string? workingDirectory = Path.GetDirectoryName(command);

            // Process all the textbox values and add them
            // to the command parameters
            foreach (Control ctrl in tableLayoutPanel1.Controls)
            {
                if (ctrl is TextBox tb)
                {
                    ParameterInfo? param = tb != null ? tb.Tag as ParameterInfo : null;
                    string? val = tb?.Text;
                    commandParameters += (!string.IsNullOrWhiteSpace(val) && param != null) ? " -" + param.Name + " \"" + val + "\"" : "";
                }
            }

            string? strOutput = ExecuteCommand(command, commandParameters, workingDirectory);

            MessageBox.Show(strOutput);

            this.Close();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            string sourceFolder = tbTemplateFolderPath.Text;
            ProcessFolder(appSettings["cmdPath"] ?? "", sourceFolder);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tbTemplateFolderPath_Click(object sender, EventArgs e)
        {
            if(folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                tbTemplateFolderPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}