using System;
using System.ComponentModel;
using System.Windows.Input;
using Avalonia.Controls;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class CustomBaseChildWindowViewModel : IChildWindowViewModel
{
    public CustomBaseChildWindowViewModel()
    {
        AcceptCommand = new DelegateCommand(null, null);
        CancelCommand = new DelegateCommand(null, null);
    }
    public WindowStartupLocation Location { get; set; }
    public double RequestedTop { get; set; }
    public double RequestedLeft { get; set; }
    public string? ChildWindowTitle { get; set; }
    public double RequestedWidth { get; set; }
    public double RequestedHeight { get; set; }
    
    public event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog = delegate { };
    public ICommand AcceptCommand { get; set; }
    public ICommand CancelCommand { get; set; }
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
}