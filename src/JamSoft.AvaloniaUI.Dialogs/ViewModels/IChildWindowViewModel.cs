using JamSoft.AvaloniaUI.Dialogs.Events;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The child window view model interface
/// </summary>
public interface IChildWindowViewModel : IDialogViewModel, IWindowPositionAware
{
    /// <summary>
    /// The child window title
    /// </summary>
    string? ChildWindowTitle { get; set; }
    
    /// <summary>
    /// The child window width
    /// </summary>
    double RequestedWidth { get; set; }
    
    /// <summary>
    /// The child window height
    /// </summary>
    double RequestedHeight { get; set; }
}