using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using JamSoft.AvaloniaUI.Dialogs.MsgBox;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;
using JamSoft.AvaloniaUI.Dialogs.Views;

namespace JamSoft.AvaloniaUI.Dialogs;

internal class MessageBoxService : IMessageBoxService
{
    public Task<MsgBoxResult> Show(string caption, string messageBoxText, MsgBoxButton button, MsgBoxImage icon = MsgBoxImage.None,
        string? noButtonText = null, string? yesButtonText = null, string? cancelButtonText = null, string? checkBoxText = null)
    {
        return Show(new MsgBoxViewModel(caption, messageBoxText, button, icon, noButtonText, yesButtonText, cancelButtonText, checkBoxText));
    }
    
    public Task<MsgBoxResult> Show(IMsgBoxViewModel? viewModel)
    {
        if (viewModel == null)
            return Task.FromResult(MsgBoxResult.CreateResult(false, MsgBoxButtonResult.None));
        
        if (Application.Current != null &&
            Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop && desktop.MainWindow != null)
        {
            return ShowWindow(desktop.MainWindow, viewModel);
        }

        if (Application.Current != null &&
            Application.Current.ApplicationLifetime is ISingleViewApplicationLifetime lifetime)
        {
            return ShowPopup(lifetime.MainView as ContentControl);
        }

        throw new NotSupportedException("ApplicationLifetime is not supported");
    }

    private Task<MsgBoxResult> ShowWindow(Window owner, IMsgBoxViewModel viewModel)
    {
        var view = new MsgBoxView();
        var win = new MsgBoxWindow();
        
        var contentControl = win.FindControl<ContentControl>("Host");
        contentControl!.Content = view;
        win.DataContext = viewModel;
        win.WindowStartupLocation = viewModel.WindowStartupLocation;
        win.Topmost = viewModel.Topmost;

        var tcs = new TaskCompletionSource<MsgBoxResult>();
        win.Closing += (_, _) =>
        {
            tcs.TrySetResult(MsgBoxResult.CreateResult(viewModel.CheckBoxResult, viewModel.Result));
        };

        win.Show(owner);
        return tcs.Task;
    }
    
    /// <summary>
    ///  Show messagebox as popup
    /// </summary>
    /// <param name="owner"></param>
    /// <returns></returns>
    public Task<MsgBoxResult> ShowPopup(ContentControl? owner)
    {
        var tcs = new TaskCompletionSource<MsgBoxResult>();
        return tcs.Task;
    }
}