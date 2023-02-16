using Avalonia.Controls;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public interface IWindowPositionAware
{
    WindowStartupLocation Location { get; set; }
	
    double RequestedTop { get; set; }
	
    double RequestedLeft { get; set; }
}