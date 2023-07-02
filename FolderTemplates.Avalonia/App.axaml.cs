using System;
using System.Collections.Specialized;
using System.Configuration;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

using FolderTemplates.Avalonia.ViewModels;
using FolderTemplates.Avalonia.Views;
using FolderTemplates.CommandLine;

namespace FolderTemplates.Avalonia;

public partial class App : Application
{
    private NameValueCollection? appSettings;
    private ConsoleCommandLine? cmd;
    private string? cmdPath;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var desktopLifetime = ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
            desktopLifetime!.Startup += (sender, args) =>
            {
                MainViewModel vm = new();

                // Do stuff with args
                string defaultCmdPath = "FolderTemplates.Console.exe";
                appSettings = ConfigurationManager.AppSettings;
                cmdPath = (appSettings != null && appSettings["cmdPath"] != null) ? appSettings["cmdPath"] ?? defaultCmdPath : defaultCmdPath;

                vm.CommandPath = cmdPath;

                cmd = new();
                cmd.RegisterParameter(new CommandLineParameter("sourceFolder", false, "The path of the Template Folder to process"));
                //cmd.RegisterParameter(new CommandLineParameter("templateFile", false, "The path of the Template Folder Definition file to apply"));
                cmd.RegisterParameter(new CommandLineParameter("targetFolder", false, "The path of the folder in which to generate the template result"));
                _ = cmd.Parse(args.Args ?? Array.Empty<string>(), true, "sourceFolder");

                if (cmd.ParsedSuccessfully)
                {
                    // Get the source, target and template paths from supplied parameters
                    vm.TargetPath = cmd["targetFolder"].Exists ? cmd["targetFolder"].Value : "";
                    vm.SourcePath = cmd["sourceFolder"].Exists ? cmd["sourceFolder"].Value : "";
                }

                // Create your window
                desktop.MainWindow = new MainWindow
                {
                    DataContext = vm
                };
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
