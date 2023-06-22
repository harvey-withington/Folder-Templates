using FolderTemplates.CommandLine;
using FolderTemplates.API;
using FolderTemplates.Data;
using Newtonsoft.Json;

namespace FolderTemplates.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleCommandLine cmd = new();
            cmd.RegisterParameter(new CommandLineParameter("sourceFolder", true, "The path of the Template Folder to process"));
            cmd.RegisterParameter(new CommandLineParameter("templateFile", false, "The path of the Template Folder Definition file to apply"));
            cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
            cmd.RegisterParameter(new CommandLineParameter("listParams", false, "Don't process the template folder, just list its parameters"));
            cmd.RegisterParameter(new CommandLineParameter("nowait", false, "Close the console after processing, do not wait for keypress"));
            cmd.RegisterParameter(new CommandLineParameter("noprompt", false, "Do not prompt for missing parameters, use defaults instead"));
            cmd.Parse(args ?? Array.Empty<string>(), true, "sourceFolder");

            if (cmd.ParsedSuccessfully)
            {
                bool errorOccurred = false;

                // Get the source, target and template paths from supplied parameters
                string sourcePath = Path.GetFullPath(cmd["sourceFolder"].Value ?? "");
                string targetPath = Path.GetFullPath(cmd["targetFolder"].Value ?? Path.GetDirectoryName(cmd["sourceFolder"].Value ?? "") ?? "");
                string templateFile = Path.GetFullPath(cmd["templateFile"].Value ?? Path.Combine(cmd["sourceFolder"].Value ?? "", ".ft/template.json"));

                // Validate paths
                if (!Directory.Exists(sourcePath))
                {
                    Console.WriteLine("The specified source path could not be found.");
                    errorOccurred = true;
                }

                if (!Directory.Exists(targetPath))
                {
                    Console.WriteLine("The specified target path could not be found.");
                    errorOccurred = true;
                }

                if (!System.IO.File.Exists(templateFile))
                {
                    Console.WriteLine("The specified template file could not be found. Make sure the source folder has a properly-configured '.ft' subfolder, or specify the -templateFile parameter.");
                    errorOccurred = true;
                }

                if (!errorOccurred)
                {
                    // Load the template file
                    Template template = FolderTemplateAPI.Load(templateFile);

                    if (cmd["listParams"].Exists)
                    {
                        string? format = string.IsNullOrWhiteSpace(cmd["listParams"]?.Value) ? "plain" : cmd["listParams"].Value;
                        List<ParameterInfo> publicParams = template.Parameters
                            .Where((p) => p.Prompt != null)
                            .Select(p => new ParameterInfo(p.Name, p.Type, p.Prompt, p.Placeholder, p.DefaultValue))
                            .ToList();

                        switch (format)
                        {


                            case "plain":
                                Console.WriteLine("Listing Template Folder params (format = " + format + "):\n");
                                // Prompt for missing tempalte parameters
                                foreach (ParameterInfo param in publicParams)
                                {
                                    Console.WriteLine(param.Name + " (" + param.Type + "): '" + param.Prompt + "' = [" + param.DefaultValue + "]");
                                }
                                break;
                            case "json":
                                //Console.WriteLine("Not implemented: " + format + ".");
                                //Console.WriteLine(new JsonResult(myResponseObject) { SerializerSettings = new JsonSerializerOptions() { WriteIndented = true } };)
                                string serialized = JsonConvert.SerializeObject(publicParams, Formatting.Indented);
                                Console.WriteLine(serialized);
                                break;
                            default:
                                Console.WriteLine("Invalid format: " + format + ".");
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Processing Template Folder...");

                        // Set incoming template parameter values from unregistered command line flags
                        string[] unregisteredNames = cmd.Names.Where((p) => cmd[p].Registered == false).ToArray<string>();
                        foreach (string unregisteredName in unregisteredNames)
                        {
                            var matched = template.Parameters.FirstOrDefault((p) => p != null && p.Name == unregisteredName, null);
                            if (matched != null)
                                matched.Value = cmd[unregisteredName].Value;
                        }

                        if (!cmd["noprompt"].Exists) 
                        { 
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
                        }

                        // Call the API to process the specified folder
                        FolderTemplateAPI.ProcessFolder(sourcePath, targetPath, template);

                        Console.WriteLine("Finished Processing.");
                    }
                }
            }

            if (!cmd["nowait"].Exists)
            {
                Console.WriteLine("\nComplete - Press any key to exit");
                Console.ReadKey();
            }
            
        }
    }
}