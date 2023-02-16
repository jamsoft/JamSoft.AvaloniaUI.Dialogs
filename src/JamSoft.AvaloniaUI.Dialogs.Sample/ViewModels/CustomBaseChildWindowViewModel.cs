using System;
using Avalonia.Controls;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.Sample.Models;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;

public class CustomBaseChildWindowViewModel : IChildWindowViewModel
{

    
    public WindowStartupLocation Location { get; set; }
    public double RequestedTop { get; set; }
    public double RequestedLeft { get; set; }
    public string? ChildWindowTitle { get; set; }
    public double RequestedWidth { get; set; }
    public double RequestedHeight { get; set; }
    
    public event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog;
}