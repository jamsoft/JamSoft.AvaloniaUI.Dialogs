using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;
using JamSoft.AvaloniaUI.Dialogs.Views;

namespace JamSoft.AvaloniaUI.Dialogs;

public class DialogService : IDialogService
{
    private readonly DialogServiceConfiguration _config;
    private string? _lastDirectorySelected;
    private readonly HashSet<ChildWindowViewModel?> _openChildren = new();
    
    public DialogService(DialogServiceConfiguration config)
    {
        _config = config;
    }
    
    public async void ShowDialog<TViewModel, TView>(TView view, TViewModel viewModel, Action<TViewModel> callback)
        where TView : Control where TViewModel : DialogViewModel
    {
        var win = new DialogWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };

        var contentControl = win.FindControl<ContentControl>("Host");
        contentControl.Content = view;
        win.DataContext = viewModel;
        
        var accept = await win.ShowDialog<bool>(GetActiveWindowOrMainWindow());
        if (accept)
        {
            callback(viewModel);
        }
    }
    
    public void ShowChildWindow<TViewModel, TView>(TView view, TViewModel viewModel)
        where TView : Control where TViewModel : ChildWindowViewModel
    {
        // prevent multiple instances of the same child window
        if (_openChildren.FirstOrDefault(x => x?.GetType() == typeof(TViewModel)) != null)
            return;

        var win = new ChildWindow();
        
        var contentControl = win.FindControl<ContentControl>("Host");
        contentControl.Content = view;
        win.DataContext = viewModel;

        _openChildren.Add(viewModel);
        win.Closing += Window_Closing;
        win.Show();
    }
    
    private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        if (sender is ChildWindow window)
        {
            _openChildren.Remove(window.DataContext as ChildWindowViewModel);
            window.Closing -= Window_Closing;
        }
    }
    
    public async Task<string?> OpenFolder(string dialogDescription, bool showNewFolderButton, string? startDirectory = null)
    {
        var fd = new OpenFolderDialog
        {
            Directory = startDirectory ?? _lastDirectorySelected,
            Title = $@"{_config.ApplicationName} - {dialogDescription}"
        };

        var path = await fd.ShowAsync(GetActiveWindowOrMainWindow());

        _lastDirectorySelected = path!;

        return path;
    }
    
    public async Task<string?> SaveFile(bool overWritePrompt = true, IEnumerable<FileDialogFilter>? filters = null, string? defaultExtension = null)
    {
        var fd = new SaveFileDialog
        {
            Title = $@"{_config.ApplicationName} - Save",
            Filters = filters?.ToList(),
            Directory = _lastDirectorySelected,
            DefaultExtension = defaultExtension
        };

        return await fd.ShowAsync(GetActiveWindowOrMainWindow());
    }

    public async Task<string?> OpenFile(IEnumerable<FileDialogFilter>? filters = null)
    {
        var fd = new OpenFileDialog
        {
            Title = $@"{_config.ApplicationName} - Open File",
            AllowMultiple = false,
            Filters = filters?.ToList(),
            Directory = _lastDirectorySelected
        };

        var paths = await fd.ShowAsync(GetActiveWindowOrMainWindow());
        if (paths != null &&
            paths.Any())
        {
            return paths[0];
        }

        return null;
    }
    
    private static Window GetActiveWindowOrMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return desktop.Windows.SingleOrDefault(x => x.IsActive) ?? desktop.MainWindow;
        }

        throw new InvalidOperationException(
            "Cannot find a Window when ApplicationLifetime is not ClassicDesktopStyleApplicationLifetime");
    }
}