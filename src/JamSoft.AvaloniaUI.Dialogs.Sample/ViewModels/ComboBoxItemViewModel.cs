using JamSoft.Helpers.AvaloniaUI.Patterns.Mvvm;
using ReactiveUI;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class ComboBoxItemViewModel : AvaloniaViewModelBase
{
    private string _name;
    private object? _value;

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public object? Value
    {
        get => _value;
        set => this.RaiseAndSetIfChanged(ref _value, value);
    }
}