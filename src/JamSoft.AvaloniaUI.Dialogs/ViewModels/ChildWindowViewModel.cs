using Avalonia.Controls;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public interface IWindowPositionAware
{
    WindowStartupLocation Location { get; set; }
	
    double RequestedTop { get; set; }
	
    double RequestedLeft { get; set; }
}

public class ChildWindowViewModel : DialogViewModel, IWindowPositionAware
{
    private double _requestedTop;
    private double _requestedLeft;
    private double _requestedWidth;
    private double _requestedHeight;

    public double RequestedTop
    {
        get { return _requestedTop; }
        set { SetProperty(ref _requestedTop, value); }
    }

    public double RequestedLeft
    {
        get { return _requestedLeft; }
        set { SetProperty(ref _requestedLeft, value); }
    }

    public double RequestedWidth
    {
        get { return _requestedWidth; }
        set { SetProperty(ref _requestedWidth, value); }
    }

    public double RequestedHeight
    {
        get { return _requestedHeight; }
        set { SetProperty(ref _requestedHeight, value); }
    }

    public WindowStartupLocation Location { get; set; }
}