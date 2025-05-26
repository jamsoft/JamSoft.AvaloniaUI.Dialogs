using System.ComponentModel;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The dialog view model interface
/// </summary>
public interface IDialogViewModel : INotifyPropertyChanged, IDialogResultVmHelper
{
    /// <summary>
    /// The dialog accept command text
    /// </summary>
    string? AcceptCommandText { get; set; }
    
    /// <summary>
    /// The dialog cancel command text
    /// </summary>
    string? CancelCommandText { get; set; }
    
    /// <summary>
    /// Implements the logic that controls the accept command execution validation
    /// </summary>
    /// <returns></returns>
    bool CanAccept();

    /// <summary>
    /// Implements the logic that controls the cancel command execution validation
    /// </summary>
    /// <returns></returns>
    bool CanCancel();
    
    /// <summary>
    /// If true, the cancel button will not be shown
    /// </summary>
    bool HideCancelButton { get; set; }
}