using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;
using JamSoft.AvaloniaUI.Dialogs.Views;

namespace JamSoft.AvaloniaUI.Dialogs;

/// <summary>
/// The dialog service
/// </summary>
internal class DialogService : IDialogService
{
    private readonly DialogServiceConfiguration _config;
    private string? _lastDirectorySelected;
    private readonly HashSet<IChildWindowViewModel?> _openChildren = new();
    
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="config"></param>
    public DialogService(DialogServiceConfiguration config)
    {
        _config = config;
    }
    
    /// <summary>
    /// Shows a dialog with a callback to return the view model based on the result of the dialog.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">The callback.</param>
    public void ShowDialog<TViewModel>(TViewModel viewModel, Action<TViewModel> callback)
        where TViewModel : IDialogViewModel
    {
        var viewName = GetViewName(viewModel);
        var viewType = Type.GetType(viewName);
        if (viewType != null)
        {
            Control viewInstance = CreateViewInstance(viewType, viewName);
            ShowDialog(viewInstance, viewModel, callback);
        }
        else
        {
            throw new ArgumentNullException($"Could not find type {viewName}");
        }
    }

    /// <summary>
    /// Shows a dialog with a callback to return the view model based on the result of the dialog.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <param name="view">The view.</param>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">The callback.</param>
    public async void ShowDialog<TViewModel, TView>(TView view, TViewModel viewModel, Action<TViewModel> callback)
        where TView : Control where TViewModel : IDialogViewModel
    {
        var win = new DialogWindow
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner
        };
        
        var contentControl = win.FindControl<ContentControl>("Host");
        contentControl!.Content = view;
        win.DataContext = viewModel;
        
        var accept = await win.ShowDialog<bool>(GetActiveWindowOrMainWindow());
        if (accept)
        {
            callback(viewModel);
        }
    }

    /// <summary>
    /// Shows a child window.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">the callback to received the view model instance on close</param>
    public void ShowChildWindow<TViewModel>(TViewModel viewModel, Action<TViewModel>? callback)
        where TViewModel : class, IChildWindowViewModel
    {
        var viewName = GetViewName(viewModel);
        var viewType = Type.GetType(viewName);
        if (viewType != null)
        {
            Control viewInstance = CreateViewInstance(viewType, viewName);
            ShowChildWindow(viewInstance, viewModel, callback);
        }
        else
        {
            throw new TypeLoadException($"Could not find type {viewName}");
        }
    }

    /// <summary>
    /// Shows a child window.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <param name="view">The view.</param>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">the callback to receive the view model instance on close</param>
    public void ShowChildWindow<TViewModel, TView>(TView view, TViewModel viewModel, Action<TViewModel>? callback)
        where TView : Control where TViewModel : class, IChildWindowViewModel
    {
        // prevent multiple instances of the same child window
        if (_openChildren.FirstOrDefault(x => x?.GetType() == typeof(TViewModel)) != null)
            return;

        var win = new ChildWindow();

        viewModel.ChildWindowTitle = CreateTitle(viewModel.ChildWindowTitle);
        
        var contentControl = win.FindControl<ContentControl>("Host");
        contentControl!.Content = view;
        win.DataContext = viewModel;

        _openChildren.Add(viewModel);
        win.Closing += (sender, _) =>
        {
            if (sender is ChildWindow)
            {
                _openChildren.Clear();
            }
            
            if (callback != null)
                callback(viewModel);
        };
        win.Show();
    }

    public void StartWizard<TViewModel>(TViewModel viewModel, Action<TViewModel>? callback) where TViewModel : class, IWizardViewModel
    {
        // prevent multiple instances of the same child window
        if (_openChildren.FirstOrDefault(x => x?.GetType() == typeof(TViewModel)) != null)
            return;

        var win = new ChildWindow();
        
        viewModel.ChildWindowTitle = CreateTitle(viewModel.ChildWindowTitle);
        
        var contentControl = win.FindControl<ContentControl>("Host");

        var viewName = GetViewName(viewModel);
        var viewType = Type.GetType(viewName);

        var viewInstance = CreateViewInstance(viewType!, viewName);
        
        win.DataContext = viewModel;
        contentControl!.Content = viewInstance;
        
        _openChildren.Add(viewModel);
        win.Closing += (sender, _) =>
        {
            if (sender is ChildWindow)
            {
                _openChildren.Clear();
            }
            
            if (callback != null)
                callback(viewModel);
        };
        
        win.Show();
    }

    /// <summary>
    /// Launches a system folder dialog so the user can pick a system folder on disk.
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="startDirectory">the root directory to browse</param>
    /// <returns>the selected folder path or null if the dialog was cancelled</returns>
    public async Task<string?> OpenFolder(string? title, string? startDirectory = null)
    {
        var storageProvider = GetStorageProvider();
        var folder = await storageProvider.TryGetFolderFromPathAsync((startDirectory ?? _lastDirectorySelected)!);    
        var path = await storageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions
        {
            SuggestedStartLocation = folder,
            Title = CreateTitle(title)
        });

        if (path.Count < 1)
            return null;

        _lastDirectorySelected = path[0].Path.LocalPath;

        return _lastDirectorySelected;
    }
    
    /// <summary>
    /// Gets a path for a new file
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="filters">The file extension filters</param>
    /// <param name="defaultExtension">The default file extension</param>
    /// <returns>the selected file path or null if the dialog was cancelled</returns>
    public async Task<string?> SaveFile(string title, IEnumerable<FilePickerFileType>? filters = null, string? defaultExtension = null)
    {
        var storageProvider = GetStorageProvider();
        var folder = await storageProvider.TryGetFolderFromPathAsync(_lastDirectorySelected!);
        var fd = await storageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = CreateTitle(title),
            FileTypeChoices = filters?.ToList(),
            SuggestedStartLocation = folder,
            DefaultExtension = defaultExtension
        });

        return fd?.Path.LocalPath;
    }

    /// <summary>
    /// The individual file path
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="filters">The file extension filters</param>
    /// <returns>the selected file path or null if the dialog was cancelled</returns>
    public async Task<string?> OpenFile(string title, IEnumerable<FilePickerFileType>? filters = null)
    {
        var paths = await OpenFile(title, false, filters);
        if (paths != null && paths.Any())
        {
            return paths[0];
        }

        return null;
    }
    
    /// <summary>
    /// Returns multiple existing file paths
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="filters">The file extension filters</param>
    /// <returns>the selected file paths or null if the dialog was cancelled</returns>
    public async Task<string[]?> OpenFiles(string title, IEnumerable<FilePickerFileType>? filters = null)
    {
        var paths = await OpenFile(title, true, filters);
        if (paths != null && paths.Any())
        {
            return paths;
        }

        return null;
    }
    
    private async Task<string[]?> OpenFile(string title, bool allowMultiFileSelection, IEnumerable<FilePickerFileType>? filters = null)
    {
        var storageProvider = GetStorageProvider();
        var folder = await storageProvider.TryGetFolderFromPathAsync(_lastDirectorySelected!);
        var fd = await storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = CreateTitle(title),
            AllowMultiple = allowMultiFileSelection,
            FileTypeFilter = filters?.ToList(),
            SuggestedStartLocation = folder
        });

        return fd.Select(x => x.Path.LocalPath).ToArray();
    }
    
    private IStorageProvider GetStorageProvider()
    {
        var topLevel = TopLevel.GetTopLevel(GetActiveWindowOrMainWindow());
        if (topLevel != null)
        {
            return topLevel.StorageProvider;
        }

        throw new InvalidOperationException("Cannot find a StorageProvider when TopLevel is null");
    }
    
    private Window GetActiveWindowOrMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            return (desktop.Windows.SingleOrDefault(x => x.IsActive) ?? desktop.MainWindow)!;
        }

        throw new InvalidOperationException("Cannot find a Window when ApplicationLifetime is not ClassicDesktopStyleApplicationLifetime");
    }

    private string? CreateTitle(string? title)
    {
        if (_config.UseApplicationNameInTitle)
            return $"{_config.ApplicationName}-{title}";

        return title;
    }
    
    private string GetViewName<TViewModel>(TViewModel viewModel) where TViewModel : IDialogViewModel
    {
        if (viewModel == null)
            throw new ArgumentNullException(nameof(viewModel));
        
        if (string.IsNullOrWhiteSpace(_config.ViewsAssemblyName))
            throw new ArgumentNullException(nameof(_config.ViewsAssemblyName),
                "You must set the assembly name containing your views in the DialogServiceConfiguration instance");

        var viewName = !string.IsNullOrWhiteSpace(_config.ViewsAssemblyName)
            ? $"{viewModel.GetType().FullName!.Replace("ViewModel", "View")},{_config.ViewsAssemblyName}"
            : "";
        return viewName;
    }
    
    private static Control CreateViewInstance(Type viewType, string viewName)
    {
        Control? viewInstance;
        try
        {
            viewInstance = (Control)Activator.CreateInstance(viewType)!;
        }
        catch (Exception ex)
        {
            throw new TypeInitializationException(viewName, ex);
        }

        return viewInstance;
    }
}