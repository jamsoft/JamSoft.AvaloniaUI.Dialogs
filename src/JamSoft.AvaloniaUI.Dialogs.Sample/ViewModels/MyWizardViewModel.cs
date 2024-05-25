using System.Collections.ObjectModel;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MyWizardViewModel : WizardViewModel
{
    private string? _valueOne;
    private string? _valueTwo;
    private string? _valueThree;
    private string? _valueFour;
    private bool _wizardStepOneComplete;
    private bool _wizardStepTwoComplete;
    private bool _wizardStepThreeComplete;
    private bool _wizardStepFourComplete;
    private ObservableCollection<ComboBoxItemViewModel>? _comboboxItems;
    private ComboBoxItemViewModel? _selectedItem;
    
    public MyWizardViewModel()
    {
        ComboboxItems = new ObservableCollection<ComboBoxItemViewModel>
        {
            new ComboBoxItemViewModel { Name = string.Empty, Value = null },
            new ComboBoxItemViewModel { Name = "Item One", Value = 1 },
            new ComboBoxItemViewModel { Name = "Item Two", Value = 2 },
            new ComboBoxItemViewModel { Name = "Item Three", Value = 3 }
        };
    }
    
    public ObservableCollection<ComboBoxItemViewModel>? ComboboxItems
    {
        get => _comboboxItems;
        set => RaiseAndSetIfChanged(ref _comboboxItems, value);
    }

    public ComboBoxItemViewModel? SelectedItem
    {
        get => _selectedItem;
        set
        {
            RaiseAndSetIfChanged(ref _selectedItem, value);
            WizardStepOneComplete = !string.IsNullOrWhiteSpace(ValueOne) && value?.Value != null;
        }
    }

    public string? ValueOne
    {
        get => _valueOne;
        set
        {
            RaiseAndSetIfChanged(ref _valueOne, value);
            WizardStepOneComplete = !string.IsNullOrWhiteSpace(value) && SelectedItem?.Value != null;
        }
    }

    public string? ValueTwo
    {
        get => _valueTwo;
        set
        {
            RaiseAndSetIfChanged(ref _valueTwo, value);
            WizardStepTwoComplete = !string.IsNullOrWhiteSpace(value);
        }
    }

    public string? ValueThree
    {
        get => _valueThree;
        set
        {
            RaiseAndSetIfChanged(ref _valueThree, value);
            WizardStepThreeComplete = !string.IsNullOrWhiteSpace(value);
        }
    }

    public string? ValueFour
    {
        get => _valueFour;
        set
        {
            RaiseAndSetIfChanged(ref _valueFour, value);
            WizardStepFourComplete = !string.IsNullOrWhiteSpace(value);
        }
    }

    public bool WizardStepOneComplete
    {
        get => _wizardStepOneComplete;
        set => RaiseAndSetIfChanged(ref _wizardStepOneComplete, value);
    }

    public bool WizardStepTwoComplete
    {
        get => _wizardStepTwoComplete;
        set => RaiseAndSetIfChanged(ref _wizardStepTwoComplete, value);
    }

    public bool WizardStepThreeComplete
    {
        get => _wizardStepThreeComplete;
        set => RaiseAndSetIfChanged(ref _wizardStepThreeComplete, value);
    }

    public bool WizardStepFourComplete
    {
        get => _wizardStepFourComplete;
        set => RaiseAndSetIfChanged(ref _wizardStepFourComplete, value);
    }
}