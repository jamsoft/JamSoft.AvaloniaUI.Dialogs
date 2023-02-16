using System.ComponentModel;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public interface IDialogViewModel : INotifyPropertyChanged, IDialogResultVmHelper
{
    string? AcceptCommandText { get; set; }
    
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
}