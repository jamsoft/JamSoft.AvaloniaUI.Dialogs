using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public interface IDialogResultVmHelper
{
    /// <summary>
    /// Occurs when [request close dialog].
    /// </summary>
    event EventHandler<RequestCloseDialogEventArgs> RequestCloseDialog;

    /// <summary>
    /// Gets or sets the accept command.
    /// </summary>
    /// <value>
    /// The accept command.
    /// </value>
    DelegateCommand? AcceptCommand { get; set; }

    /// <summary>
    /// Gets or sets the cancel command.
    /// </summary>
    /// <value>
    /// The cancel command.
    /// </value>
    DelegateCommand? CancelCommand { get; set; }
}