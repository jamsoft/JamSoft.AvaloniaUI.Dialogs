using JamSoft.AvaloniaUI.Dialogs.Events;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public interface IChildWindowViewModel : IWindowPositionAware
{
    string? ChildWindowTitle { get; set; }
    double RequestedWidth { get; set; }
    double RequestedHeight { get; set; }
    event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog;
}