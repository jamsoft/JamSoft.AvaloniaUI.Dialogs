using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using JamSoft.AvaloniaUI.Dialogs.MsgBox;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;
using JamSoft.AvaloniaUI.Dialogs.Views;

namespace JamSoft.AvaloniaUI.Dialogs;

internal class MessageBoxService : IMessageBoxService
{
    public Task<MsgBoxResult> Show(string caption, string messageBoxText, MsgBoxButton button, MsgBoxImage icon,
        string? noButtonText = null, string? yesButtonText = null, string? cancelButtonText = null)
    {
        return Show(new MsgBoxViewModel(caption, messageBoxText, button, icon, noButtonText, yesButtonText, cancelButtonText));
    }
    
    public Task<MsgBoxResult> Show(MsgBoxViewModel viewModel)
    {
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

    private Task<MsgBoxResult> ShowWindow(Window owner, MsgBoxViewModel viewModel)
    {
        var view = new MsgBoxView();
        //var viewModel = new MsgBoxViewModel();
        var win = new MsgBoxWindow();
        
        var contentControl = win.FindControl<ContentControl>("Host");
        contentControl!.Content = view;
        win.DataContext = viewModel;
        win.WindowStartupLocation = viewModel.WindowStartupLocation;
        win.Topmost = viewModel.Topmost;

        var tcs = new TaskCompletionSource<MsgBoxResult>();

        win.Closing += (sender, _) =>
        {
            tcs.TrySetResult(viewModel.Result);
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
        // DialogHostStyles style = null;
        // if (!owner.Styles.OfType<DialogHostStyles>().Any())
        // {
        //     style = new DialogHostStyles();
        //     owner.Styles.Add(style);
        // }
        //
        //
        // var parentContent = owner.Content;
        // var dh = new DialogHost();
        // dh.Identifier = "MsBoxIdentifier" + Guid.NewGuid();
        // _viewModel.SetFullApi(_view);
        // owner.Content = null;
        // dh.Content = parentContent;
        // dh.CloseOnClickAway = false;
        // owner.Content = dh;
        var tcs = new TaskCompletionSource<MsgBoxResult>();
        // _view.SetCloseAction(() =>
        // {
        //     tcs.TrySetResult(_view.GetButtonResult());
        //     DialogHost.Close(dh.Identifier);
        //     owner.Content = null;
        //     dh.Content = null;
        //     owner.Content = parentContent;
        //     if (style != null)
        //     {
        //         owner.Styles.Remove(style);
        //     }
        // });
        // DialogHost.Show(_view, dh.Identifier);
        return tcs.Task;
    }
}