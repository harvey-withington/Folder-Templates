using Avalonia;
using ReactiveUI;
using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using FolderTemplates.API.Console;
using FolderTemplates.Data;
using System.Linq;
using System.Diagnostics;

namespace FolderTemplates.Avalonia.ViewModels;

public class MainViewModel : ViewModelBase
{
    public event EventHandler? RequestClose;

    private string sourcePath = "";
    private string targetPath = "";
    private bool exitWhenComplete = true;
    private string logText = "";
    private ObservableCollection<ParameterInfo>? parameters = null;

    public string SourcePath { get => sourcePath; set => this.RaiseAndSetIfChanged(ref sourcePath, value); }
    public string TargetPath { get => targetPath; set => this.RaiseAndSetIfChanged(ref targetPath, value); }
    public bool ExitWhenComplete { get => exitWhenComplete; set => this.RaiseAndSetIfChanged(ref exitWhenComplete, value); }
    public string LogText { get => logText; set => this.RaiseAndSetIfChanged(ref logText, value); }
    public List<ParameterInfo>? Parameters { 
        get => parameters?.ToList(); 
        set => parameters = (value != null ? new ObservableCollection<ParameterInfo>(value) : null); 
    }

    public MainViewModel() {
    }

    public void LoadTemplate(string cmdPath)
    {
        if (!string.IsNullOrWhiteSpace(SourcePath))
        {
            TemplateInfo templateInfo = FolderTemplatesConsoleAPI.getTemplateInfo(cmdPath, SourcePath);
            if (string.IsNullOrWhiteSpace(TargetPath))
            {
                if (!string.IsNullOrWhiteSpace(templateInfo.DefaultTargetPath))
                {
                    string defaultTargetFolder = Path.GetFullPath(templateInfo.DefaultTargetPath, Path.GetDirectoryName(SourcePath) ?? "");
                    TargetPath = defaultTargetFolder;
                }
            }

            Parameters = FolderTemplatesConsoleAPI.getParameterInfo(cmdPath, SourcePath);
        }
    }

    public void ExitApplication()
    {
        RequestClose?.Invoke(this, new EventArgs());
    }

    public void Process()
    {

    }
}
