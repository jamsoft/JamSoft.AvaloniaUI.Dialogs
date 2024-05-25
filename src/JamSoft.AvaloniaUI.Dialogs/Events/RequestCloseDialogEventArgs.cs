using JamSoft.AvaloniaUI.Dialogs.MsgBox;

namespace JamSoft.AvaloniaUI.Dialogs.Events;

/// <summary>
/// The request close dialog event
/// </summary>
public class RequestCloseDialogEventArgs : EventArgs
{
    /// <summary>
    /// The dialog result
    /// </summary>
    public bool DialogResult { get; set; }
    
    /// <summary>
    /// Default constructor
    /// </summary>
    /// <param name="dialogresult">the dialog result instance</param>
    public RequestCloseDialogEventArgs(bool dialogresult)
    {
        DialogResult = dialogresult;
    }
}