using Avalonia.Controls;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs;

/// <summary>
/// The dialog service interface
/// </summary>
public interface IDialogService
{
    /// <summary>
    /// Shows a dialog with a callback to return the view model based on the result of the dialog.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">The callback.</param>
    void ShowDialog<TViewModel>(TViewModel viewModel, Action<TViewModel> callback) where TViewModel : IDialogViewModel;
    
    /// <summary>
    /// Shows a dialog with a callback to return the view model based on the result of the dialog.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <param name="view">The view.</param>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">The callback.</param>
    void ShowDialog<TViewModel, TView>(TView view, TViewModel viewModel, Action<TViewModel> callback) where TView : Control where TViewModel : IDialogViewModel;

    /// <summary>
    /// Shows a child window.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">the callback to received the view model instance on close</param>
    void ShowChildWindow<TViewModel>(TViewModel viewModel, Action<TViewModel>? callback) where TViewModel : IChildWindowViewModel;
    
    /// <summary>
    /// Shows a child window.
    /// </summary>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    /// <typeparam name="TView">The type of the view.</typeparam>
    /// <param name="view">The view.</param>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">the callback to received the view model instance on close</param>
    void ShowChildWindow<TViewModel, TView>(TView view, TViewModel viewModel, Action<TViewModel>? callback = null) where TView : Control where TViewModel : IChildWindowViewModel;

    /// <summary>
    /// Shows a wizard view
    /// </summary>
    /// <param name="viewModel">The view model.</param>
    /// <param name="callback">the callback to received the view model instance on close</param>
    /// <typeparam name="TViewModel">The type of the view model.</typeparam>
    void StartWizard<TViewModel>(TViewModel viewModel, Action<TViewModel> callback) where TViewModel : IWizardViewModel;
    
    /// <summary>
    /// Launches a system folder dialog so the user can pick a system folder on disk.
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="startDirectory">the root directory to browse</param>
    /// <returns>the selected folder path or null if the dialog was cancelled</returns>
    Task<string?> OpenFolder(string title, string? startDirectory = null);

    /// <summary>
    /// Gets a path for a new file
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="filters">The file extension filters</param>
    /// <param name="defaultExtension">The default file extension</param>
    /// <returns>the selected file path or null if the dialog was cancelled</returns>
    Task<string?> SaveFile(string title, IEnumerable<FileDialogFilter>? filters = null, string? defaultExtension = null);

    /// <summary>
    /// The an individual file path
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="filters">The file extension filters</param>
    /// <returns>the selected file path or null if the dialog was cancelled</returns>
    Task<string?> OpenFile(string title, IEnumerable<FileDialogFilter>? filters = null);

    /// <summary>
    /// Returns multiple existing file paths
    /// </summary>
    /// <param name="title">The dialog title</param>
    /// <param name="filters">The file extension filters</param>
    /// <returns>the selected file paths or null if the dialog was cancelled</returns>
    Task<string[]?> OpenFiles(string title, IEnumerable<FileDialogFilter>? filters = null);
}