using IWshRuntimeLibrary;
using File = System.IO.File;

namespace FolderTemplates.App
{
    public static class Shortcut
    {
        public static void CreateShortcut(string originalFilePathAndName, string? shortcutName, string destinationSavePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalFilePathAndName);
            string? originalFilePath = Path.GetDirectoryName(originalFilePathAndName);

            string link = destinationSavePath + Path.DirectorySeparatorChar + (shortcutName ?? fileName) + ".lnk";
            WshShell shell = new();
            if (shell.CreateShortcut(link) is IWshShortcut shortcut)
            {
                shortcut.TargetPath = originalFilePathAndName;
                shortcut.WorkingDirectory = originalFilePath;
                shortcut.Save();
            }
        }

        public static void DeleteShortcut(string originalFilePathAndName, string? shortcutName, string destinationSavePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalFilePathAndName);

            string link = destinationSavePath + Path.DirectorySeparatorChar + (shortcutName ?? fileName) + ".lnk";
            if (File.Exists(link)) File.Delete(link);
        }

        public static bool ShortcutExists(string originalFilePathAndName, string? shortcutName, string destinationSavePath)
        {
            string fileName = Path.GetFileNameWithoutExtension(originalFilePathAndName);
            string link = destinationSavePath + Path.DirectorySeparatorChar + (shortcutName ?? fileName) + ".lnk";
            
            return Path.Exists(link);
        }

        public static void CreateSendToShortcut(string? shortcutName = null)
        {
            if(Environment.ProcessPath != null)
                CreateShortcut(Environment.ProcessPath, shortcutName, Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
        }

        public static void DeleteSendToShortcut(string? shortcutName = null)
        {
            if (Environment.ProcessPath != null)
                DeleteShortcut(Environment.ProcessPath, shortcutName, Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
        }

        public static bool SendToShortcutExists(string? shortcutName = null)
        {
            return Environment.ProcessPath != null && ShortcutExists(Environment.ProcessPath, shortcutName, Environment.GetFolderPath(Environment.SpecialFolder.SendTo));
        }

        public static void CreateStartupShortcut(string? shortcutName = null)
        {
            if (Environment.ProcessPath != null)
                CreateShortcut(Environment.ProcessPath, shortcutName, Environment.GetFolderPath(Environment.SpecialFolder.Startup));
        }

        public static void DeleteStartupShortcut(string? shortcutName = null)
        {
            if (Environment.ProcessPath != null)
                DeleteShortcut(Environment.ProcessPath, shortcutName, Environment.GetFolderPath(Environment.SpecialFolder.Startup));
        }

        public static bool StartupShortcutExists(string? shortcutName = null)
        {
            return Environment.ProcessPath != null && ShortcutExists(Environment.ProcessPath, shortcutName, Environment.GetFolderPath(Environment.SpecialFolder.Startup));
        }
    }
}
