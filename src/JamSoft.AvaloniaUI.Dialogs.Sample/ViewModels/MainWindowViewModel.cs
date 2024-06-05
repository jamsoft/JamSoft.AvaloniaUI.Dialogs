using System;
using System.Collections.Generic;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Helpers;
using JamSoft.AvaloniaUI.Dialogs.MsgBox;
using JamSoft.AvaloniaUI.Dialogs.Sample.Models;
using JamSoft.AvaloniaUI.Dialogs.Sample.Views;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;
using ReactiveUI;
using Splat;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private readonly IMessageBoxService _messageBoxService;
    private ICommand? _openFileCommand;
    private ICommand? _openFolderCommand;
    private ICommand? _openWordFileCommand;
    private ICommand? _saveFileCommand;
    private ICommand? _saveWordFileCommand;
    private ICommand? _openFilesCommand;
    private ICommand? _showDialogCommand;
    private ICommand? _showCustomizedDialogCommand;
    private ICommand? _showChildWindowCommand;
    private ICommand? _showDialogAutoFindViewCommand;
    private ICommand? _showChildWindowAutoFindViewCommand;
    private ICommand? _showCustomChildWindowCommand;
    private ICommand? _childWindowRememberPositionCommand;
    private ICommand? _missingViewCommand;
    private ICommand? _wizardViewCommand;
    private ICommand? _showMessageBoxCommand;
    private string? _message;
    
    public MainWindowViewModel(IDialogService dialogService, IMessageBoxService messageBoxService)
    {
        _dialogService = dialogService;
        _messageBoxService = messageBoxService;

        Message = "Welcome to JamSoft Avalonia Dialogs!";
        
        OpenFileCommand = new DelegateCommand(OpenFileCommandExecuted, () => true);
        OpenFolderCommand = new DelegateCommand(OpenFolderCommandExecuted, () => true);
        OpenWordFileCommand = new DelegateCommand(OpenWordFileCommandExecuted, () => true);
        OpenFilesCommand = new DelegateCommand(OpenFilesCommandExecuted, () => true);
        SaveFileCommand = new DelegateCommand(SaveFileCommandExecuted, () => true);
        SaveWordFileCommand = new DelegateCommand(SaveWordFileCommandExecuted, () => true);
        ShowDialogCommand = new DelegateCommand(ShowDialogCommandExecuted, () => true);
        ShowDialogAutoFindViewCommand = new DelegateCommand(ShowDialogAutoFindViewCommandExecuted, () => true);
        ShowCustomizedDialogCommand = new DelegateCommand(ShowCustomizedDialogCommandExecuted, () => true);
        ShowChildWindowCommand = new DelegateCommand(ShowChildWindowCommandExecuted, () => true);
        ShowChildWindowAutoFindViewCommand = new DelegateCommand(ShowChildWindowAutoFindViewCommandExecuted, () => true);
        ShowCustomChildWindowCommand = new DelegateCommand(ShowCustomChildWindowAutoFindViewCommandExecuted, () => true);
        ChildWindowRememberPositionCommand = new DelegateCommand(ChildWindowRememberPositionCommandExecuted, () => true);
        MissingViewCommand = new DelegateCommand(MissingViewCommandExecuted, () => true);
        WizardViewCommand = new DelegateCommand(WizardViewCommandExecuted, () => true);
        ShowMessageBoxCommand = new DelegateCommand(ShowMessageBoxCommandExecuted, () => true);
    }

    public ICommand? ShowMessageBoxCommand
    {
        get => _showMessageBoxCommand;
        set => this.RaiseAndSetIfChanged(ref _showMessageBoxCommand, value);
    }
    
    public ICommand? OpenFolderCommand    
    {
        get => _openFolderCommand;
        set => this.RaiseAndSetIfChanged(ref _openFolderCommand, value);
    }

    public ICommand? WizardViewCommand
    {
        get => _wizardViewCommand;
        set => this.RaiseAndSetIfChanged(ref _wizardViewCommand, value);
    }

    public ICommand? MissingViewCommand
    {
        get => _missingViewCommand;
        set => this.RaiseAndSetIfChanged(ref _missingViewCommand, value);
    }

    public ICommand? ChildWindowRememberPositionCommand
    {
        get => _childWindowRememberPositionCommand;
        set => this.RaiseAndSetIfChanged(ref _childWindowRememberPositionCommand, value);
    }

    public ICommand? ShowCustomChildWindowCommand
    {
        get => _showCustomChildWindowCommand;
        set => this.RaiseAndSetIfChanged(ref _showCustomChildWindowCommand, value);
    }

    public ICommand? ShowChildWindowAutoFindViewCommand
    {
        get => _showChildWindowAutoFindViewCommand;
        set => this.RaiseAndSetIfChanged(ref _showChildWindowAutoFindViewCommand, value);
    }

    public ICommand? ShowDialogAutoFindViewCommand
    {
        get => _showDialogAutoFindViewCommand;
        set => this.RaiseAndSetIfChanged(ref _showDialogAutoFindViewCommand, value);
    }

    public ICommand? ShowChildWindowCommand
    {
        get => _showChildWindowCommand;
        set => this.RaiseAndSetIfChanged(ref _showChildWindowCommand, value);
    }

    public ICommand? ShowDialogCommand
    {
        get => _showDialogCommand;
        set => this.RaiseAndSetIfChanged(ref _showDialogCommand, value);
    }
    
    public ICommand? ShowCustomizedDialogCommand
    {
        get => _showCustomizedDialogCommand;
        set => this.RaiseAndSetIfChanged(ref _showCustomizedDialogCommand, value);
    }

    public ICommand? OpenFilesCommand
    {
        get => _openFilesCommand;
        set => this.RaiseAndSetIfChanged(ref _openFilesCommand, value);
    }

    public ICommand? SaveFileCommand
    {
        get => _saveFileCommand;
        set => this.RaiseAndSetIfChanged(ref _saveFileCommand, value);
    }
    
    public ICommand? SaveWordFileCommand
    {
        get => _saveWordFileCommand;
        set => this.RaiseAndSetIfChanged(ref _saveWordFileCommand, value);
    }

    public ICommand? OpenFileCommand
    {
        get => _openFileCommand;
        set => this.RaiseAndSetIfChanged(ref _openFileCommand, value);
    }
    
    public ICommand? OpenWordFileCommand
    {
        get => _openWordFileCommand;
        set => this.RaiseAndSetIfChanged(ref _openWordFileCommand, value);
    }

    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }
    
    private async void OpenFileCommandExecuted()
    {
        Message = await _dialogService.OpenFile("Open Any File");
    }
    
    private async void OpenFolderCommandExecuted()
    {
        Message = await _dialogService.OpenFolder("Open Any Folder");
    }
    
    private async void OpenWordFileCommandExecuted()
    {
        Message = await _dialogService.OpenFile("Open Word File", new List<FilePickerFileType>
        {
            CommonFilters.WordFilter
        });
    }
    
    private async void OpenFilesCommandExecuted()
    {
        Message = string.Join(Environment.NewLine, await _dialogService.OpenFiles("Open Multiple Files") ?? []);
    }
    
    private async void SaveFileCommandExecuted()
    {
        Message = await _dialogService.SaveFile("Save Any File");
    }
    
    private async void SaveWordFileCommandExecuted()
    {
        Message = await _dialogService.SaveFile("Save Word File", new List<FilePickerFileType>
        {
            CommonFilters.WordFilter
        });
    }
    
    private void ShowDialogCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyDialogViewModel>();
        if (vm != null) _dialogService.ShowDialog(new MyDialogView(), vm, DialogCallback);
    }
    
    private void ShowDialogAutoFindViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyDialogViewModel>();
        if (vm != null) _dialogService.ShowDialog(vm, DialogCallback);
    }
    
    private void ShowCustomizedDialogCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyDialogViewModel>();
        if (vm != null)
        {
            vm.AcceptCommandText = "Accept";
            vm.CancelCommandText = "Oh No!";
            
            _dialogService.ShowDialog(new MyDialogView(), vm, DialogCallback);
        }
    }

    private void DialogCallback(MyDialogViewModel? obj)
    {
        Message = obj?.DialogMessage;
    }
    
    private void ShowChildWindowCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyChildWindowViewModel>();
        if (vm != null)
        {
            vm.RequestedLeft = 50;
            vm.RequestedTop = 50;
            vm.RequestedHeight = 500;
            vm.RequestedWidth = 600;
            // these values can be stored in user settings and loaded at runtime etc.
            vm.ChildMessage = "Child Message Value";
            vm.ChildWindowTitle = "My Child Window Title";

            _dialogService.ShowChildWindow(new MyChildWindowView(), vm,
                model => { Message = $"Child Closed - {model.ChildMessage}"; });
        }
    }
    
    private void ShowChildWindowAutoFindViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyChildWindowViewModel>();
        if (vm != null)
        {
            vm.RequestedLeft = 50;
            vm.RequestedTop = 50;
            vm.RequestedHeight = 500;
            vm.RequestedWidth = 600;
            vm.ChildMessage = "Child Message Value";
            vm.ChildWindowTitle = "My Child Window Title Auto Find";
            vm.Location = WindowStartupLocation.CenterScreen;

            _dialogService.ShowChildWindow(vm, model => { Message = $"Child Closed - {model.ChildMessage}"; });
        }
    }
    
    private void ShowCustomChildWindowAutoFindViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<CustomBaseChildWindowViewModel>();
        if (vm != null)
        {
            vm.RequestedLeft = 50;
            vm.RequestedTop = 50;
            vm.RequestedHeight = 500;
            vm.RequestedWidth = 600;

            vm.ChildWindowTitle = "My Custom Child Window Title Auto Find";

            _dialogService.ShowChildWindow(vm,
                model => { Message = $"Custom Child View Model Closed - {model.GetType()}"; });
        }
    }
    
    private void ChildWindowRememberPositionCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyChildWindowViewModel>();
        if (vm != null)
        {
            // these values can be stored in user settings and loaded at runtime etc.
            vm.RequestedLeft = MyUserSettings.Instance.Left;
            vm.RequestedTop = MyUserSettings.Instance.Top;
            vm.RequestedHeight = MyUserSettings.Instance.Height;
            vm.RequestedWidth = MyUserSettings.Instance.Width;

            vm.ChildWindowTitle = "My Custom Child Window Title Auto Find";

            _dialogService.ShowChildWindow(vm,
                model => { Message = $"Child Remember Position Closed - {model.GetType()}"; });
        }
    }
    
    private void MissingViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<SirNotAppearingInThisAppViewModel>();

        try
        {
            if (vm != null) _dialogService.ShowChildWindow(vm, model => { });
        }
        catch (Exception ex)
        {
            Message = ex.Message;
        }
    }
    
    private void WizardViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyWizardViewModel>();
        if (vm != null)
        {
            vm.RequestedLeft = MyUserSettings.Instance.Left;
            vm.RequestedTop = MyUserSettings.Instance.Top;
            vm.RequestedHeight = MyUserSettings.Instance.Height;
            vm.RequestedWidth = MyUserSettings.Instance.Width;

            vm.ChildWindowTitle = "My Wizard";

            _dialogService.StartWizard(vm, model => { Message = $"Wizard Closed - {model.GetType()}"; });
        }
    }
    
    private async void ShowMessageBoxCommandExecuted()
    {
        var resultOkCancel = await _messageBoxService.Show("OK Cancel", "Do you want to carry on?", MsgBoxButton.OkCancel, MsgBoxImage.Error);
        Message = $"{resultOkCancel.ButtonResult} clicked";
        
        var resultOk = await _messageBoxService.Show("Ok", "Do you want to carry on?", MsgBoxButton.Ok, MsgBoxImage.Information);
        Message = $"{resultOk.ButtonResult} clicked";
        
        var resultYesNo = await _messageBoxService.Show("Yes No", "Do you want to carry on?", MsgBoxButton.YesNo, MsgBoxImage.Asterisk);
        Message = $"{resultYesNo.ButtonResult} clicked";
        
        var resultYesNoCancel = await _messageBoxService.Show("Yes No Cancel", "Do you want to carry on?", MsgBoxButton.YesNoCancel, MsgBoxImage.Question);
        Message = $"{resultYesNoCancel.ButtonResult} clicked";
        
        var resultYesNoWarning = await _messageBoxService.Show("Yes No", "Do you want to carry on?", MsgBoxButton.YesNo, MsgBoxImage.Warning);
        Message = $"{resultYesNoWarning.ButtonResult} clicked";
        
        var resultYesNoNoIcon = await _messageBoxService.Show("Yes No Without Icon", "Do you want to carry on?", MsgBoxButton.YesNo);
        Message = $"{resultYesNoNoIcon.ButtonResult} clicked";
        
        var viewModel = new MsgBoxViewModel("Yes No With Icon", "Do you want to carry on?", MsgBoxButton.YesNo, MsgBoxImage.Forbidden);
        var resultVm = await _messageBoxService.Show(viewModel);
        Message = $"{resultVm.ButtonResult} clicked";
        
        var resultYesNoCancelCustomButtonText = await _messageBoxService.Show("German Yes No Cancel", "Möchten Sie weitermachen?", MsgBoxButton.YesNoCancel, MsgBoxImage.Wifi, "Nein", "Ja", "Abbrechen");
        Message = $"{resultYesNoCancelCustomButtonText.ButtonResult} clicked";
        
        var msgBoxResult = await _messageBoxService.Show("OK Cancel With Checkbox", "Do you want to carry on?", MsgBoxButton.OkCancel, MsgBoxImage.Error, checkBoxText:"Don't ask me again");
        Message = $"Button: {msgBoxResult.ButtonResult}, Checkbox: {msgBoxResult.CheckBoxResult}";
    }
}