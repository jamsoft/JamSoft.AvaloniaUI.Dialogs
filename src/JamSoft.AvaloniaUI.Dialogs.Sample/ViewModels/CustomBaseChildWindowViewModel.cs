using System;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class CustomBaseChildWindowViewModel : IChildWindowViewModel
{
    private ICommand _cancelCommand;

    public CustomBaseChildWindowViewModel()
    {
        AcceptCommand = new DelegateCommand(null);
        CancelCommand = new DelegateCommand(null);
        CloseIcon = new Bitmap(AssetLoader.Open(new Uri("avares://JamSoft.AvaloniaUI.Dialogs/Assets/CloseIcon/icons8-close-30.png")));
        
        _cancelCommand = new DelegateCommand(() => InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(false)), CanCancel);
        CancelCommandText = "Cancel";
    }
    
    private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
    {
        RequestCloseDialog?.Invoke(this, e);
    }
    
    public WindowStartupLocation Location { get; set; }
    public double RequestedTop { get; set; }
    public double RequestedLeft { get; set; }
    public string? ChildWindowTitle { get; set; }
    public double RequestedWidth { get; set; }
    public double RequestedHeight { get; set; }
    public IImage CloseIcon { get; set; }

    public event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog = delegate { };
    public ICommand AcceptCommand { get; set; }

    public ICommand CancelCommand
    {
        get => _cancelCommand;
        set => _cancelCommand = value;
    }

    public event PropertyChangedEventHandler? PropertyChanged = delegate { };
    
    public string? AcceptCommandText { get; set; }
    public string? CancelCommandText { get; set; }
    public bool CanAccept()
    {
        return true;
    }

    public bool CanCancel()
    {
        return true;
    }

    public bool HideCancelButton { get; set; }
}