using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.Sample.Models;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MyChildWindowViewModel : ChildWindowViewModel
{
    private string? _childMessage;

    public MyChildWindowViewModel()
    {
        RequestCloseDialog += OnRequestCloseDialog;
    }

    public string? ChildMessage
    {
        get => _childMessage;
        set => RaiseAndSetIfChanged(ref _childMessage, value);
    }

    private void OnRequestCloseDialog(object sender, RequestCloseDialogEventArgs e)
    {
        MyUserSettings.Instance.Top = RequestedTop;
        MyUserSettings.Instance.Left = RequestedLeft;
        MyUserSettings.Instance.Width = RequestedWidth;
        MyUserSettings.Instance.Height = RequestedHeight;

        RequestCloseDialog -= OnRequestCloseDialog;
    }
}