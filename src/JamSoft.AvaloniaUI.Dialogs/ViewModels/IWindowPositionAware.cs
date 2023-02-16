using Avalonia.Controls;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The window position aware interface
/// </summary>
public interface IWindowPositionAware
{
	/// <summary>
	/// The child window startup location
	/// </summary>
    WindowStartupLocation Location { get; set; }
	
	/// <summary>
	/// The child window requested top value
	/// </summary>
    double RequestedTop { get; set; }
	
	/// <summary>
	/// The child window requested left value
	/// </summary>
    double RequestedLeft { get; set; }
}