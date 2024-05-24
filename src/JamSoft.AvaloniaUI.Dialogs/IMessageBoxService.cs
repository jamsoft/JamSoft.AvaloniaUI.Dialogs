using JamSoft.AvaloniaUI.Dialogs.MsgBox;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs;

public interface IMessageBoxService
{
    Task<MsgBoxResult> Show(string caption, string messageBoxText, MsgBoxButton button, MsgBoxImage icon, string? noButtonText = null, string? yesButtonText = null, string? cancelButtonText = null);
    
    Task<MsgBoxResult> Show(MsgBoxViewModel viewModel);
}