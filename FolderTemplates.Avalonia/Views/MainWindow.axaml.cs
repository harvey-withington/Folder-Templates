using Avalonia.Controls;
using System;
using FolderTemplates.Avalonia.ViewModels;
using System.Threading.Tasks;
using Avalonia.Threading;

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
                Dispatcher.UIThread.Post(() => Close(), DispatcherPriority.Background);
            };

            vm.RequestFolderDialog += (s, e) =>
            {
                OpenFolderDialog dialog = new OpenFolderDialog();
                var result = Task.Run(() => dialog.ShowAsync(this)).Result;
                e.FolderPath = result;
            };
        };
    }
}
