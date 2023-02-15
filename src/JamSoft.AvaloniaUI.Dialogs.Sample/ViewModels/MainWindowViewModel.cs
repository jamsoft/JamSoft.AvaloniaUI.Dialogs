namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    public string Greeting => "Welcome to Avalonia!";

    public MainWindowViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
    }
}