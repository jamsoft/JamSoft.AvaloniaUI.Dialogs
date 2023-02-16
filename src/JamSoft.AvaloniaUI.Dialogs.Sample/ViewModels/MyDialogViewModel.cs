using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MyDialogViewModel : DialogViewModel
{
    private string? _dialogMessage;

    public MyDialogViewModel()
    {
        RequestCloseDialog += OnRequestCloseDialog;
    }

    public string? DialogMessage
    {
        get => _dialogMessage;
        set => RaiseAndSetIfChanged(ref _dialogMessage , value);
    }

    public override bool CanAccept()
    {
        return !string.IsNullOrWhiteSpace(DialogMessage);
    }

    public override bool CanCancel()
    {
        return string.IsNullOrWhiteSpace(DialogMessage);
    }
    
    private void OnRequestCloseDialog(object sender, RequestCloseDialogEventArgs e)
    {
        RequestCloseDialog -= OnRequestCloseDialog;
    }
}