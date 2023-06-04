using System.ComponentModel;
using System.Collections;
using FolderTemplates.CommandLine;
using FolderTemplates.API;

namespace FolderTemplates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting Folder Templates App...");

            string shortcutName = "Process with Folder Templates";

            if (args != null && args.Length>0 && args[0]=="/Install")
            {
                Console.Write("Installing... ");
                //Console.ReadLine();
                Shortcut.CreateSendToShortcut(shortcutName);
                Console.WriteLine("Done.");
                Environment.Exit(0);
            }

            if (args != null && args.Length > 0 && args[0] == "/Uninstall")
            {
                Console.Write("Uninstalling... ");
                //Console.ReadLine();
                Shortcut.DeleteSendToShortcut(shortcutName);
                Console.WriteLine("Done.");
                Environment.Exit(0);
            }

            ConsoleCommandLine cmd = new();
            cmd.RegisterParameter(new CommandLineParameter("sourceFolder", true, "The path of the Template Folder to process"));
            cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
            cmd.RegisterParameter(new CommandLineParameter("templateFile", false, "The path of the Template Folder Definition file to apply"));
            cmd.RegisterParameter(new CommandLineParameter("nowait", false, "Close the console after processing, do not wait for keypress"));
            cmd.Parse(args ?? Array.Empty<string>(), true, "sourceFolder");

            if (cmd.ParsedSuccessfully)
            {
                bool parametersOK = true;

                // Get the source, target and template paths from supplied parameters
                string sourcePath = Path.GetFullPath(cmd["sourceFolder"].Value ?? "");
                string targetPath = Path.GetFullPath(cmd["targetFolder"].Value ?? Path.GetDirectoryName(cmd["sourceFolder"].Value ?? "") ?? "");
                string templateFile = Path.GetFullPath(cmd["templateFile"].Value ?? Path.Combine(cmd["sourceFolder"].Value ?? "", ".ft/template.json"));

                // Validate paths
                if (!Directory.Exists(sourcePath))
                {
                    Console.WriteLine("The specified source path could not be found.");
                    parametersOK = false;
                }

                if (!Directory.Exists(targetPath))
                {
                    Console.WriteLine("The specified target path could not be found.");
                    parametersOK = false;
                }

                if (!System.IO.File.Exists(templateFile))
                {
                    Console.WriteLine("The specified template file could not be found. Make sure the source folder has a properly-configured '.ft' subfolder, or specify the -templateFile parameter.");
                    parametersOK = false;
                }

                if (parametersOK)
                {
                    Console.WriteLine("Processing Folders...");

                    // Load the template file
                    Template template = FolderTemplateAPI.Load(templateFile);

                    // Set incoming template parameter values from unregistered command line flags
                    string[] unregisteredNames = cmd.Names.Where((p) => cmd[p].Registered == false).ToArray<string>();
                    foreach (string unregisteredName in unregisteredNames)
                    {
                        var matched = template.Parameters.FirstOrDefault((p) => p != null && p.Name == unregisteredName, null);
                        if (matched != null)
                            matched.Value = cmd[unregisteredName].Value;
                    }

                    // Prompt for missing tempalte parameters
                    foreach (TemplateParameter param in template.Parameters.Where((p) => p != null && p.Value == null))
                    {
                        if (param.Prompt != null)
                        {
                            Console.Write(param.Prompt + ": ");
                            string? input = Console.ReadLine();
                            param.Value = (input != null && input.Trim() != "") ? input.Trim() : param.DefaultValue;
                        }
                    }

                    // Call the API to process the specified folder
                    FolderTemplateAPI.ProcessFolder(sourcePath, targetPath, template);

                    Console.WriteLine("Finished Processing.");
                }
            }

            if (!cmd["nowait"].Exists)
            {
                Console.WriteLine("Complete - Press any key to exit");
                Console.ReadKey();
            }
            
        }
    }
}