using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MyWizardViewModel : WizardViewModel
{
    private string _valueOne;

    public MyWizardViewModel()
    {
        ValueOne = "From the view model!";
    }

    public string ValueOne
    {
        get => _valueOne;
        set => RaiseAndSetIfChanged(ref _valueOne, value);
    }
}