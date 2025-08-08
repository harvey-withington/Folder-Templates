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
            cmd.Parse(args ?? Array.Empty<string>(), true, "sourceFolder");

            if (cmd.ParsedSuccessfully)
            {
                string folderPath = (cmd["sourceFolder"].Exists ? cmd["sourceFolder"].Value : "") ?? "";
                string ftFolderPath = Path.Combine(folderPath, ".ft");
                string templatePath = Path.Combine(ftFolderPath, "template.json");

                if (!Directory.Exists(ftFolderPath) || !File.Exists(templatePath))
                {
                    // Template doesn't exist, launch template creation form
                    Application.Run(new frmCreateTemplate(args ?? Array.Empty<string>()));
                    return;
                }
            }

            Application.Run(new frmCreateFolder(args ?? Array.Empty<string>()));
        }
    }
}