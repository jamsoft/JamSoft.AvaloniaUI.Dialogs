using JamSoft.AvaloniaUI.Dialogs.MsgBox;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs;

/// <summary>
/// The message box service interface
/// </summary>
public interface IMessageBoxService
{
    /// <summary>
    /// Show a message box with the specified parameters
    /// </summary>
    /// <param name="caption">the messagebox caption</param>
    /// <param name="messageBoxText">the message text</param>
    /// <param name="button">the button configuration</param>
    /// <param name="icon">the icon enum</param>
    /// <param name="noButtonText">the negative response button text value</param>
    /// <param name="yesButtonText">the positive response button text</param>
    /// <param name="cancelButtonText">the cancel response button text</param>
    /// <returns><see cref="MsgBoxResult"/></returns>
    Task<MsgBoxResult> Show(string caption, string messageBoxText, MsgBoxButton button, MsgBoxImage icon = MsgBoxImage.None, string? noButtonText = null, string? yesButtonText = null, string? cancelButtonText = null);
    
    /// <summary>
    /// Show a message box with the specified view model
    /// </summary>
    /// <param name="viewModel">the view model instance</param>
    /// <returns><see cref="MsgBoxResult"/></returns>
    Task<MsgBoxResult> Show(IMsgBoxViewModel viewModel);
}