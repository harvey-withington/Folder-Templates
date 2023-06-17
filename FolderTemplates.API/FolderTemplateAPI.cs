using System.IO;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace FolderTemplates.API
{
    public class Template
    {
        public string? Name { get; set; }
        public List<TemplateParameter> Parameters { get; set; } = new List<TemplateParameter>();
    }

    public class ParameterInfo
    {
        public ParameterInfo()
        {

        }

        public ParameterInfo(string? name, string? type, string? prompt, string? placeholder, string? defaultValue)
        {
            Name = name;
            Type = type;
            Prompt = prompt;
            Placeholder = placeholder;
            DefaultValue = defaultValue;
        }

        public string? Name { get; set; }
        public string? Type { get; set; } = "text";
        public string? Prompt { get; set; } = null;
        public string? Placeholder { get; set; } = null;
        public string? DefaultValue { get; set; } = null;
    }

    public class TemplateParameter: ParameterInfo
    {
        public TemplateParameter(string match) 
        {
            Match = match;
        }
       
        public string Match { get; set; }
        public bool ReplaceInFileNames { get; set; } = false; 
        public bool ReplaceInFiles { get; set; } = false;
        public string? Value { get; set; } = null;
    }

    public class ReplaceParameter
    {
        public ReplaceParameter(string? fileNameMatch, string? fileContentToken, string value)
        {
            FileNameMatch = fileNameMatch;
            FileContentToken = fileContentToken;
            Value = value;
        }

        public string? FileNameMatch { get; set; }
        public string? FileContentToken { get; set; }
        public string Value { get; set; }
    }

    public partial class FolderTemplateAPI
    {
        [GeneratedRegex(@"{{\$(\w+)}}", RegexOptions.IgnoreCase)]
        private static partial Regex FindTokens();

        public static Template Load(string TemplatePath)
        {
            using StreamReader r = new(TemplatePath);
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<Template>(json) ?? throw new FileLoadException("Could not load template file");
        }

        public static void ProcessFolder(string sourceFolder, string targetFolder, Template template)
        {
            string sourcePath = Path.GetDirectoryName(sourceFolder) ?? "";
            string sourceFolderName = Path.GetFileNameWithoutExtension(sourceFolder);

            CopyEntireDirectory(
                new DirectoryInfo(sourceFolder), 
                new DirectoryInfo(Path.Combine(targetFolder, sourceFolderName)), 
                new string[] { "^\\.ft$" },
                template.Parameters.ConvertAll<ReplaceParameter>( 
                    p => new ReplaceParameter(
                        p.ReplaceInFileNames ? p.Match ?? "\\{" + p.Name + "\\}" : null, 
                        p.ReplaceInFiles ? p.Name : null, 
                        p.Value ?? p.DefaultValue ?? "")).ToArray());
        }

        private static string ProcessName(string inputName, ReplaceParameter[] replaceList)
        {
            string currentName = inputName;
            // Perform any replacements on the folder or file name from the replaceList
            foreach (ReplaceParameter replace in replaceList)
                if(replace.FileNameMatch != null)
                    currentName = Regex.Replace(currentName, replace.FileNameMatch, replace.Value);

            return currentName;
        }

        private static void ProcessFile(string sourceFile, string targetFile, ReplaceParameter[] replaceList)
        {
            // Copy entire text file, replacing any tokens found
            // NOTE: This reads the entire file into memory, so will only work well with small-ish files
            // TODO: Can we use streams to both copy and transform the file?
            // TODO: If not, perhaps there should be an upper limit on file size ?
            // TODO: We should also make sure this is a text file as we can't replace tokens in binary files.

            // Read the entire source file
            string fileContents = File.ReadAllText(sourceFile);

            // Use regex to identify all tokens in the form {{$tokenName}} in the file and replace them
            // with the matching template values
            string replaced =
                FindTokens().Replace(fileContents, 
                    match =>
                    {
                        // Find the matching FileContentToken string in the replaceList
                        var found = replaceList.FirstOrDefault((p) => p != null && p.FileContentToken == match.Groups[1].Value, null);
                        // If we found a replacement value, return it, otherwise, return
                        // the entire token to pass it through invisibly
                        return found != null ? found.Value : match.Value;
                    });

            // Write the entire target file with the result
            File.WriteAllText(targetFile, replaced);
        }

        public static void CopyEntireDirectory(DirectoryInfo source, DirectoryInfo target, string[]? skipList = null, ReplaceParameter[]? replaceList = null, bool overwiteFiles = true)
        {
            skipList ??= Array.Empty<string>();
            replaceList ??= Array.Empty<ReplaceParameter>();

            if (!source.Exists) return;

            // Skip any files & folders that match a string in the skip array
            foreach (string skip in skipList)
            {
                if (Regex.IsMatch(source.Name, skip)) return;
            }

            // Split the directory and file info into separate variables
            string? targetPath = Path.GetDirectoryName(target.FullName);
            string targetFolderName = target.Name;

            // Create the target path string by replacing template variables
            string replacedTargetPath = Path.Combine(targetPath ?? "", ProcessName(targetFolderName, replaceList));

            // Replace variable names in the target folder name
            DirectoryInfo replacedTarget = new(replacedTargetPath);

            // Create the new target folder
            if (!replacedTarget.Exists) replacedTarget.Create();

            // Recursively process all subfolders
            Parallel.ForEach(source.GetDirectories(), (sourceChildDirectory) =>
                CopyEntireDirectory(
                    sourceChildDirectory, 
                    new DirectoryInfo(Path.Combine(replacedTarget.FullName, sourceChildDirectory.Name)), 
                    skipList, replaceList, overwiteFiles));

            // Process each file in the folder
            Parallel.ForEach(source.GetFiles(), sourceFile =>
            {
                string targetFileName = Path.Combine(replacedTarget.FullName, ProcessName(sourceFile.Name, replaceList));

                // If the file has an .ft$ extension, do a find-and-replace in the contents
                // TODO: Should the file extension .ft$ be configurable?
                if (Path.GetExtension(sourceFile.FullName).ToLower() == ".ft$") {
                    string targetFilenameFixed = Path.Combine(Path.GetDirectoryName(targetFileName) ?? "", Path.GetFileNameWithoutExtension(targetFileName));
                    ProcessFile(sourceFile.FullName, targetFilenameFixed, replaceList);
                }
                // Otherwise, just copy the file without attempting find-and-replace
                else {
                    sourceFile.CopyTo(targetFileName, overwiteFiles);
                }
            });

        }
    }
}