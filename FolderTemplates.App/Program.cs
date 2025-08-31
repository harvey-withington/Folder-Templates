namespace FolderTemplates.App
{
    using FolderTemplates.CommandLine;
    using System.Diagnostics;

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //#if DEBUG
            //WaitForDebugger();
            //#endif

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if (args != null && args.Length > 0)
            {
                ConsoleCommandLine cmd = new();
                cmd.RegisterParameter(new CommandLineParameter("sourceFolder", true, "The path of the Template Folder to process"));
                cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
                cmd.RegisterParameter(new CommandLineParameter("edit", false, "Open the template process form, or edit the template file"));
                cmd.Parse(args ?? Array.Empty<string>(), true, "sourceFolder");

                if (cmd.ParsedSuccessfully)
                {
                    string sourcePath = (cmd["sourceFolder"].Exists ? cmd["sourceFolder"].Value : "") ?? "";
                    string? targetPath = (cmd["targetFolder"].Exists ? cmd["targetFolder"].Value : null);
                    string ftFolderPath = Path.Combine(sourcePath, ".ft");
                    string templatePath = Path.Combine(ftFolderPath, "template.json");

                    if (!Directory.Exists(ftFolderPath) || !File.Exists(templatePath) || cmd["edit"].Exists)
                    {
                        // Template doesn't exist, launch template creation form
                        Application.Run(new frmCreateTemplate(sourcePath));
                        return;
                    }
                    else
                    {
                        Application.Run(new frmCreateFolder(sourcePath, targetPath));
                    }
                }
            }
            else
            {
                Application.Run(new frmCreateFolder(null, null));
            }
        }
        static void WaitForDebugger()
        {
            if (!Debugger.IsAttached)
            {
                Console.WriteLine("Waiting for debugger to attach...");
                Debugger.Launch(); // This will prompt to attach a debugger
                Debugger.Break(); // This will break execution if a debugger is attached
            }
        }
    }
}