namespace FolderTemplates.App
{
    using FolderTemplates.CommandLine;
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            if(args == null || args.Length == 0 )
            {
                return;
            }

            ConsoleCommandLine cmd = new();
            cmd.RegisterParameter(new CommandLineParameter("sourceFolder", true, "The path of the Template Folder to process"));
            cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
            cmd.Parse(args ?? Array.Empty<string>(), true, "sourceFolder");

            if (cmd.ParsedSuccessfully)
            {
                string sourcePath = (cmd["sourceFolder"].Exists ? cmd["sourceFolder"].Value : "") ?? "";
                string? targetPath = (cmd["targetFolder"].Exists ? cmd["targetFolder"].Value : null);
                string ftFolderPath = Path.Combine(sourcePath, ".ft");
                string templatePath = Path.Combine(ftFolderPath, "template.json");

                if (!Directory.Exists(ftFolderPath) || !File.Exists(templatePath))
                {
                    // Template doesn't exist, launch template creation form
                    Application.Run(new frmCreateTemplate(sourcePath));
                    return;
                } else
                {
                    Application.Run(new frmCreateFolder(sourcePath, targetPath));
                }
            }
        }
    }
}