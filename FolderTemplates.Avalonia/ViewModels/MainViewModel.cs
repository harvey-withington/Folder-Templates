using FolderTemplates.API.Console;
using FolderTemplates.Data;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace FolderTemplates.Avalonia.ViewModels;

public delegate void RequestFolderEventHandler(object sender, RequestFolderEventArgs e);
public class RequestFolderEventArgs : EventArgs
{
    public string? FolderPath { get; set; }
}

public class MainViewModel : ViewModelBase
{
    public event EventHandler? RequestClose;
    public event RequestFolderEventHandler? RequestFolderDialog;

    private string commandPath = "";
    private string sourcePath = "";
    private string targetPath = "";
    private bool exitWhenComplete = true;
    private string logText = "";
    private ObservableCollection<ParameterInfo> parameters = new();

    public string CommandPath { get => commandPath; set => this.RaiseAndSetIfChanged(ref commandPath, value); }
    public string SourcePath { get => sourcePath; set => this.RaiseAndSetIfChanged(ref sourcePath, value); }
    public string TargetPath { get => targetPath; set => this.RaiseAndSetIfChanged(ref targetPath, value); }
    public bool ExitWhenComplete { get => exitWhenComplete; set => this.RaiseAndSetIfChanged(ref exitWhenComplete, value); }
    public string LogText { get => logText; set => this.RaiseAndSetIfChanged(ref logText, value); }
    public ObservableCollection<ParameterInfo> Parameters { get => parameters; set => this.RaiseAndSetIfChanged(ref parameters, value); }

    public MainViewModel()
    {
        this.PropertyChanged += MainViewModel_PropertyChanged;
    }

    private void MainViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(SourcePath))
        {
            LoadTemplate();
        }
    }

    public void LoadTemplate()
    {
        if (!string.IsNullOrWhiteSpace(SourcePath))
        {
            TemplateInfo templateInfo = FolderTemplatesConsoleAPI.getTemplateInfo(CommandPath, SourcePath);
            if (string.IsNullOrWhiteSpace(TargetPath))
            {
                if (!string.IsNullOrWhiteSpace(templateInfo.DefaultTargetPath))
                {
                    string defaultTargetFolder = Path.GetFullPath(templateInfo.DefaultTargetPath, Path.GetDirectoryName(SourcePath) ?? "");
                    TargetPath = defaultTargetFolder;
                }
            }

            Parameters = new ObservableCollection<ParameterInfo>(FolderTemplatesConsoleAPI.getParameterInfo(CommandPath, SourcePath));
        }
    }

    public void ExitApplication()
    {
        RequestClose?.Invoke(this, new EventArgs());
    }

    public void SelectSourcePath()
    {
        RequestFolderEventArgs args = new();
        RequestFolderDialog?.Invoke(this, args);
        if (args.FolderPath != null)
        {
            SourcePath = "";
            TargetPath = "";
            SourcePath = args.FolderPath;
        }

    }

    public void SelectTargetPath()
    {
        RequestFolderEventArgs args = new();
        RequestFolderDialog?.Invoke(this, args);
        TargetPath = args.FolderPath ?? TargetPath;
    }

    public void Process()
    {
        string? command = Path.GetFullPath(CommandPath);
        string? workingDirectory = Path.GetDirectoryName(command);
        string commandParameters = "-sourceFolder \"" + SourcePath + "\" -nowait -noprompt";

        if (TargetPath != null)
            commandParameters += " -targetFolder \"" + TargetPath + "\"";

        if (Parameters != null)
            foreach (ParameterInfo param in Parameters)
                commandParameters += !string.IsNullOrWhiteSpace(param.Value) ? " -" + param.Name + " \"" + param.Value + "\"" : "";

        FolderTemplatesConsoleAPI.ExecuteCommandAsync(command, commandParameters, workingDirectory,
            new((sender, e) => LogText += (!String.IsNullOrEmpty(e.Data)) ? e.Data + "\n" : ""),
            new((sender, e) => { if (ExitWhenComplete) ExitApplication(); })
        );
    }
}