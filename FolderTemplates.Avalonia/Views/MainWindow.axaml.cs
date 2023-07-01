using Avalonia.Controls;
using System;
using FolderTemplates.Avalonia.ViewModels;

namespace FolderTemplates.Avalonia.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    protected override void OnDataContextChanged(EventArgs obj)
    {
        if (DataContext is MainViewModel vm)
        {
            vm.RequestClose += (s, e) =>
            {
                Close();
            };
        };
    }
}
