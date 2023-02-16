using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MyChildViewModel : ChildWindowViewModel
{
    private string? _childMessage;

    public string? ChildMessage
    {
        get => _childMessage;
        set => RaiseAndSetIfChanged(ref _childMessage, value);
    }
}