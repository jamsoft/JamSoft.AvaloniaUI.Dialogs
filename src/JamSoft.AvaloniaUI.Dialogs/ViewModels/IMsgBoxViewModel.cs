using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using JamSoft.AvaloniaUI.Dialogs.MsgBox;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The message box view model interface
/// </summary>
public interface IMsgBoxViewModel : INotifyPropertyChanged, IDialogResultVmHelper
{
    /// <summary>
    /// The CheckBoxResult
    /// </summary>
    public bool CheckBoxResult { get; set; }

    /// <summary>
    /// The CheckBoxText value
    /// </summary>
    public string? CheckBoxText { get; set; }
    
    /// <summary>
    /// Determines if the dialog has an icon to show
    /// </summary>
    bool HasIcon { get; }

    /// <summary>
    /// The dialog image enum
    /// </summary>
    MsgBoxImage MsgBoxImage { get; set; }

    /// <summary>
    /// The dialog result
    /// </summary>
    MsgBoxButtonResult Result { get; set; }

    /// <summary>
    /// The dialog buttons
    /// </summary>
    MsgBoxButton Buttons { get; set; }

    /// <summary>
    /// The dialog Startup location
    /// </summary>
    WindowStartupLocation WindowStartupLocation { get; set; }

    /// <summary>
    /// The dialog topmost flag
    /// </summary>
    bool Topmost { get; set; }

    /// <summary>
    /// The dialog icon
    /// </summary>
    Bitmap? Icon { get; set; }

    /// <summary>
    /// The dialog cancel command
    /// </summary>
    string? AcceptCommandText { get; set; }

    /// <summary>
    /// The dialog accept command
    /// </summary>
    ICommand NoCommand { get; set; }

    /// <summary>
    /// The dialog cancel command text
    /// </summary>
    string? CancelCommandText { get; set; }

    /// <summary>
    /// The message box message
    /// </summary>
    string? Message { get; set; }

    /// <summary>
    /// The message box title
    /// </summary>
    string? MsgBoxTitle { get; set; }

    /// <summary>
    /// The no button text
    /// </summary>
    string? NoCommandText { get; set; }

    /// <summary>
    /// Boolean flag to show the no button
    /// </summary>
    bool ShowNoButton { get; set; }

    /// <summary>
    /// Boolean flag to show the yes button
    /// </summary>
    bool ShowYesButton { get; set; }

    /// <summary>
    /// Boolean flag to show the ok button
    /// </summary>
    bool ShowOkButton { get; set; }

    /// <summary>
    /// Boolean flag to show the cancel button
    /// </summary>
    bool ShowCancelButton { get; set; }
}