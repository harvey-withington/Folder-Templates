using FolderTemplates.Data;
using System.Diagnostics;
using Newtonsoft.Json;

namespace FolderTemplates.API.Console
{
    public class FolderTemplatesConsoleAPI
    {
        public static List<ParameterInfo> getParameterInfo(string cmdPath, string sourceFolder)
        {
            string? command = Path.GetFullPath(cmdPath);
            string? commandParameters = "-sourceFolder \"" + sourceFolder + "\" -listParams json -nowait";
            string? workingDirectory = Path.GetDirectoryName(command);
            string? strOutput = ExecuteCommand(command, commandParameters, workingDirectory);
            List<ParameterInfo> parameters = JsonConvert.DeserializeObject<List<ParameterInfo>>(strOutput ?? "") ?? new List<ParameterInfo>();
            return parameters;
        }

        public static TemplateInfo getTemplateInfo(string cmdPath, string sourceFolder)
        {
            string? command = Path.GetFullPath(cmdPath);
            string? commandParameters = "-sourceFolder \"" + sourceFolder + "\" -getTemplateInfo json -nowait";
            string? workingDirectory = Path.GetDirectoryName(command);
            string? strOutput = ExecuteCommand(command, commandParameters, workingDirectory);
            TemplateInfo templateInfo = JsonConvert.DeserializeObject<TemplateInfo>(strOutput ?? "") ?? new TemplateInfo();
            return templateInfo;
        }

        public static string? ExecuteCommand(string command, string commandParameters, string? workingDirectory)
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

        public static void ExecuteCommandAsync(string command, string commandParameters, string? workingDirectory, DataReceivedEventHandler? onDataReceived = null, EventHandler? onExited = null)
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

            if (onDataReceived != null)
            {
                process.EnableRaisingEvents = true;
                process.OutputDataReceived += onDataReceived;
                process.ErrorDataReceived += onDataReceived;
            }

            if (onExited != null)
                process.Exited += onExited;

            //Start the process
            process.Start();
            process.BeginOutputReadLine();
        }
    }
}