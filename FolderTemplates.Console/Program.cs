using FolderTemplates.CommandLine;
using FolderTemplates.API;

namespace FolderTemplates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Folder Template App...");
            
            ConsoleCommandLine cmd = new();
            cmd.RegisterParameter(new CommandLineParameter("sourceFolder", true, "The path of the Template Folder to process"));
            cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
            cmd.RegisterParameter(new CommandLineParameter("templateFile", false, "The path of the Template Folder Definition file to apply"));
            cmd.RegisterParameter(new CommandLineParameter("nowait", false, "Close the console after processing, do not wait for keypress"));
            cmd.Parse(args, false);

            Console.WriteLine("Processing Folders...");

            // Get the source, target and template paths from supplied parameters
            string sourcePath = Path.GetFullPath(cmd["sourceFolder"].Value ?? "");
            string targetPath = Path.GetFullPath(cmd["targetFolder"].Value ?? Path.GetDirectoryName(cmd["sourceFolder"].Value ?? "") ?? "");
            string templateFile = Path.GetFullPath(cmd["templateFile"].Value ?? Path.Combine(cmd["sourceFolder"].Value ?? "", ".ft/template.json"));

            // TODO: Validate paths ?

            // Load the template file
            Template template = FolderTemplateAPI.Load(templateFile);

            // TODO:    Need a way to specify the incoming parameters
            //          either via input file, or parameters
            //template.Parameters[0].Value = "overridden project name";
            //template.Parameters[1].Value = "overridden project version";

            // Call the API to process the specified folder
            FolderTemplateAPI.ProcessFolder(sourcePath, targetPath, template);

            Console.WriteLine("Finished Processing.");

            if (!cmd["nowait"].Exists)
            {
                Console.WriteLine("Complete - Press any key to exit");
                Console.ReadKey();
            }
            
        }
    }
}