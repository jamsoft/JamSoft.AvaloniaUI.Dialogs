using System.Windows.Input;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The dialog result helper
/// </summary>
public interface IDialogResultVmHelper
{
    /// <summary>
    /// Occurs when [request close dialog] event is fired.
    /// </summary>
    event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;

    /// <summary>
    /// Gets or sets the accept command.
    /// </summary>
    /// <value>
    /// The accept command.
    /// </value>
    ICommand AcceptCommand { get; set; }

    /// <summary>
    /// Gets or sets the cancel command.
    /// </summary>
    /// <value>
    /// The cancel command.
    /// </value>
    ICommand CancelCommand { get; set; }
}