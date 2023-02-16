using Avalonia.Controls;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public class ChildWindowViewModel : DialogViewModel, IWindowPositionAware, IChildWindowViewModel
{
    private double _requestedTop;
    private double _requestedLeft;
    private double _requestedWidth;
    private double _requestedHeight;
    private string? _childWindowTitle;

    public string? ChildWindowTitle
    {
        get => _childWindowTitle;
        set => RaiseAndSetIfChanged(ref _childWindowTitle, value);
    }

    public double RequestedTop
    {
        get => _requestedTop;
        set => RaiseAndSetIfChanged(ref _requestedTop, value);
    }

    public double RequestedLeft
    {
        get => _requestedLeft;
        set => RaiseAndSetIfChanged(ref _requestedLeft, value);
    }

    public double RequestedWidth
    {
        get => _requestedWidth;
        set => RaiseAndSetIfChanged(ref _requestedWidth, value);
    }

    public double RequestedHeight
    {
        get => _requestedHeight;
        set => RaiseAndSetIfChanged(ref _requestedHeight, value);
    }

    public WindowStartupLocation Location { get; set; }
}