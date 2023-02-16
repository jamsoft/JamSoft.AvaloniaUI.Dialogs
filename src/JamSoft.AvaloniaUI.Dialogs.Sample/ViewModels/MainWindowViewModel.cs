using System.Collections.Generic;
using System.Windows.Input;
using Avalonia.Controls;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Helpers;
using JamSoft.AvaloniaUI.Dialogs.Sample.Models;
using JamSoft.AvaloniaUI.Dialogs.Sample.Views;
using ReactiveUI;
using Splat;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly IDialogService _dialogService;
    private string? _message;
    private ICommand? _openFileCommand;
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
    private DelegateCommand _childWindowRememberPositionCommand;

    public MainWindowViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;

        Message = "Welcome to JamSoft Avalonia Dialogs!";
        
        OpenFileCommand = new DelegateCommand(OpenFileCommandExecuted, () => true);
        
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
    }

    public DelegateCommand ChildWindowRememberPositionCommand
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

    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
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

    private async void OpenFileCommandExecuted()
    {
        Message = await _dialogService.OpenFile("Open Any File");
    }
    
    private async void OpenWordFileCommandExecuted()
    {
        Message = await _dialogService.OpenFile("Open Word File", new List<FileDialogFilter>
        {
            CommonFilters.WordFilter
        });
    }
    
    private async void OpenFilesCommandExecuted()
    {
        Message = string.Concat(await _dialogService.OpenFiles("Open Multiple Files") ?? new string[0]);
    }
    
    private async void SaveFileCommandExecuted()
    {
        Message = await _dialogService.SaveFile("Save Any File");
    }
    
    private async void SaveWordFileCommandExecuted()
    {
        Message = await _dialogService.SaveFile("Save Word File", new List<FileDialogFilter>
        {
            CommonFilters.WordFilter
        });
    }
    
    private void ShowDialogCommandExecuted()
    {
        _dialogService.ShowDialog(new MyDialogView(), Locator.Current.GetService<MyDialogViewModel>(), DialogCallback);
    }
    
    private void ShowDialogAutoFindViewCommandExecuted()
    {
        _dialogService.ShowDialog(Locator.Current.GetService<MyDialogViewModel>(), DialogCallback);
    }
    
    private void ShowCustomizedDialogCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyDialogViewModel>();
        vm.AcceptCommandText = "Accept";
        vm.CancelCommandText = "Oh No!";
        
        _dialogService.ShowDialog(new MyDialogView(), vm, DialogCallback);
    }

    private void DialogCallback(MyDialogViewModel obj)
    {
        Message = obj.DialogMessage;
    }
    
    private void ShowChildWindowCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyChildWindowViewModel>();

        // these values can be stored in user settings and loaded at runtime etc.
        vm.RequestedLeft = 50;
        vm.RequestedTop = 50;
        vm.RequestedHeight = 600;
        vm.RequestedWidth = 800;
        vm.ChildMessage = "Child Message Value";
        vm.ChildWindowTitle = "My Child Window Title";
        
        _dialogService.ShowChildWindow(new MyChildWindowView(), vm, model =>
        {
            Message = $"Child Closed - {model.ChildMessage}";
        });
    }
    
    private void ShowChildWindowAutoFindViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyChildWindowViewModel>();

        vm.RequestedLeft = 50;
        vm.RequestedTop = 50;
        vm.RequestedHeight = 600;
        vm.RequestedWidth = 800;
        vm.ChildMessage = "Child Message Value";
        vm.ChildWindowTitle = "My Child Window Title Auto Find";
        vm.Location = WindowStartupLocation.CenterScreen;
        
        _dialogService.ShowChildWindow(vm, model =>
        {
            Message = $"Child Closed - {model.ChildMessage}";
        });
    }
    
    private void ShowCustomChildWindowAutoFindViewCommandExecuted()
    {
        var vm = Locator.Current.GetService<CustomBaseChildWindowViewModel>();

        // these values can be stored in user settings and loaded at runtime etc.
        vm.RequestedLeft = 50;
        vm.RequestedTop = 50;
        vm.RequestedHeight = 600;
        vm.RequestedWidth = 800;
        
        vm.ChildWindowTitle = "My Custom Child Window Title Auto Find";
        
        _dialogService.ShowChildWindow(vm, model =>
        {
            Message = $"Custom Child View Model Closed - {model.GetType()}";
        });
    }
    
    private void ChildWindowRememberPositionCommandExecuted()
    {
        var vm = Locator.Current.GetService<MyChildWindowViewModel>();

        // these values can be stored in user settings and loaded at runtime etc.
        vm.RequestedLeft = MyUserSettings.Instance.Left;
        vm.RequestedTop = MyUserSettings.Instance.Top;
        vm.RequestedHeight = MyUserSettings.Instance.Height;
        vm.RequestedWidth = MyUserSettings.Instance.Width;
        
        vm.ChildWindowTitle = "My Custom Child Window Title Auto Find";
        
        _dialogService.ShowChildWindow(vm, model =>
        {
            Message = $"Child Remember Position Closed - {model.GetType()}";
        });
    }
}