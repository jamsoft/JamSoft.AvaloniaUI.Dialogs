using System.Collections.ObjectModel;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.Sample.Models;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MyChildWindowViewModel : ChildWindowViewModel
{
    private string? _childMessage;
    private ObservableCollection<ComboBoxItemViewModel>? _comboboxItems;
    private ComboBoxItemViewModel? _selectedItem;

    public MyChildWindowViewModel()
    {
        RequestCloseDialog += OnRequestCloseDialog;

        ComboboxItems = new ObservableCollection<ComboBoxItemViewModel>
        {
            new ComboBoxItemViewModel { Name = string.Empty, Value = null },
            new ComboBoxItemViewModel { Name = "Item One", Value = 1 },
            new ComboBoxItemViewModel { Name = "Item Two", Value = 2 },
            new ComboBoxItemViewModel { Name = "Item Three", Value = 3 }
        };
    }

    public string? ChildMessage
    {
        get => _childMessage;
        set => RaiseAndSetIfChanged(ref _childMessage, value);
    }

    public ObservableCollection<ComboBoxItemViewModel>? ComboboxItems
    {
        get => _comboboxItems;
        set => RaiseAndSetIfChanged(ref _comboboxItems, value);
    }

    public ComboBoxItemViewModel? SelectedItem
    {
        get => _selectedItem;
        set => RaiseAndSetIfChanged(ref _selectedItem, value);
    }

    private void OnRequestCloseDialog(object? sender, RequestCloseDialogEventArgs e)
    {
        MyUserSettings.Instance.Top = RequestedTop;
        MyUserSettings.Instance.Left = RequestedLeft;
        MyUserSettings.Instance.Width = RequestedWidth;
        MyUserSettings.Instance.Height = RequestedHeight;

        RequestCloseDialog -= OnRequestCloseDialog;
    }
}