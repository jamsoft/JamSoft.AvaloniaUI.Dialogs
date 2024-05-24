using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.MsgBox;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public class MsgBoxViewModel : INotifyPropertyChanged, IDialogResultVmHelper
{
    private string? _message;
    private string? _msgBoxTitle;
    private string? _noCommandText;
    private ICommand? _noCommand;
    private bool _showNoButton;
    private bool _showYesButton;
    private bool _showOkButton;
    private bool _showCancelButton;
    private ICommand _acceptCommand = null!;
    private string? _acceptCommandText;
    private ICommand _cancelCommand = null!;
    private string? _cancelCommandText;

    public MsgBoxViewModel(string caption, string message, MsgBoxButton buttons, MsgBoxImage image, string? noButtonText = null, string? yesButtonText = null, string? cancelButtonText = null)
    {
        _msgBoxTitle = caption;
        _message = message;
     
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        NoCommandText = noButtonText;
        AcceptCommandText = yesButtonText;
        CancelCommandText = cancelButtonText;
        Buttons = buttons;
        
        SetupCommands();
        SetupButtons();
    }
    
    public MsgBoxResult Result { get; set; }
    
    public MsgBoxButton Buttons { get; set; }
    
    public WindowStartupLocation WindowStartupLocation { get; set; }
    
    public bool Topmost { get; set; }
    
    /// <summary>
    /// The dialog accept command
    /// </summary>
    public ICommand AcceptCommand
    {
        get => _acceptCommand;
        set => RaiseAndSetIfChanged(ref _acceptCommand, value);
    }
    
    /// <summary>
    /// The dialog cancel command
    /// </summary>
    public string? AcceptCommandText
    {
        get => _acceptCommandText;
        set => RaiseAndSetIfChanged(ref _acceptCommandText, value);
    }
    
    /// <summary>
    /// Gets or sets the cancel command.
    /// </summary>
    /// <value>
    /// The cancel command.
    /// </value>
    public ICommand CancelCommand
    {
        get => _cancelCommand;
        set => RaiseAndSetIfChanged(ref _cancelCommand, value);
    }
    
    /// <summary>
    /// The dialog accept command
    /// </summary>
    public ICommand? NoCommand
    {
        get => _noCommand;
        set => RaiseAndSetIfChanged(ref _noCommand, value);
    }
    
    /// <summary>
    /// The dialog cancel command text
    /// </summary>
    public string? CancelCommandText
    {
        get => _cancelCommandText;
        set => RaiseAndSetIfChanged(ref _cancelCommandText, value);
    }

    /// <summary>
    /// The PropertyChanged event
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
    
    /// <summary>
    /// Occurs when [request close dialog] event is fired.
    /// </summary>
    public event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog;
    
    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    public string? MsgBoxTitle
    {
        get => _msgBoxTitle;
        set => this.RaiseAndSetIfChanged(ref _msgBoxTitle, value);
    }

    public string? NoCommandText
    {
        get => _noCommandText;
        set => this.RaiseAndSetIfChanged(ref _noCommandText, value);
    }

    public bool ShowNoButton
    {
        get => _showNoButton;
        set => this.RaiseAndSetIfChanged(ref _showNoButton, value);
    }

    public bool ShowYesButton
    {
        get => _showYesButton;
        set => this.RaiseAndSetIfChanged(ref _showYesButton, value);
    }

    public bool ShowOkButton
    {
        get => _showOkButton;
        set => this.RaiseAndSetIfChanged(ref _showOkButton, value);
    }

    public bool ShowCancelButton
    {
        get => _showCancelButton;
        set => this.RaiseAndSetIfChanged(ref _showCancelButton, value);
    }

    protected void SetupCommands()
    {
        _noCommand = new DelegateCommand(() =>
        {
            Result = MsgBoxResult.No;
            InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(false));
        }, CanNoAccept);
        
        _acceptCommand = new DelegateCommand(() =>
        {
            if (Buttons == MsgBoxButton.YesNo || Buttons == MsgBoxButton.YesNoCancel)
            {
                Result = MsgBoxResult.Yes;
            }
            
            if (Buttons == MsgBoxButton.Ok)
            {
                Result = MsgBoxResult.Ok;
            }

            InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(true));
        }, CanAccept);
        
        _cancelCommand = new DelegateCommand(() =>
        {
            Result = MsgBoxResult.Cancel;
            InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(false));
        }, CanCancel);
    }

    protected void SetupButtons()
    {
        switch (Buttons)
        {
            case MsgBoxButton.Ok:
                AcceptCommandText ??= "OK";
                ShowOkButton = true;
                break;
            case MsgBoxButton.OkCancel:
                AcceptCommandText ??= "OK";
                CancelCommandText ??= "Cancel";
                ShowOkButton = true;
                ShowCancelButton = true;
                break;
            case MsgBoxButton.YesNo:
                AcceptCommandText ??= "Yes";
                NoCommandText ??= "No";
                ShowYesButton = true;
                ShowNoButton = true;
                break;
            case MsgBoxButton.YesNoCancel:
                AcceptCommandText ??= "Yes";
                CancelCommandText ??= "Cancel";
                NoCommandText ??= "No";
                ShowYesButton = true;
                ShowNoButton = true;
                ShowCancelButton = true;
                break;
        }
    }
    
    /// <summary>
    /// Implements the logic that controls the accept command execution validation
    /// </summary>
    /// <returns></returns>
    public virtual bool CanAccept()
    {
        return true;
    }

    /// <summary>
    /// Implements the logic that controls the cancel command execution validation
    /// </summary>
    /// <returns></returns>
    public virtual bool CanCancel()
    {
        return true;
    }
    
    /// <summary>
    /// Implements the logic that controls the accept command execution validation
    /// </summary>
    /// <returns></returns>
    public virtual bool CanNoAccept()
    {
        return true;
    }
    
    protected virtual void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
    {
        RequestCloseDialog?.Invoke(this, e);
    }
    
    /// <summary>
    /// Sets the property value only if the provided value is different from the stored value.<para />
    /// If set to a new value, then the <seealso cref="System.ComponentModel.INotifyPropertyChanged" /> event will be fired
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="storage">The storage.</param>
    /// <param name="value">The value.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    protected virtual bool RaiseAndSetIfChanged<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        OnPropertyChanged(propertyName);
        
        (AcceptCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        (CancelCommand as DelegateCommand)?.RaiseCanExecuteChanged();
        
        return true;
    }
    
    /// <summary>
    /// Raises the <see cref="E:PropertyChanged" /> event. Allows the use of a specific PropertyChangedEventArgs object.  
    /// Is the most performant implementation
    /// </summary>
    /// <param name="prop">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
    protected virtual void OnPropertyChanged(PropertyChangedEventArgs prop)
    {
        PropertyChanged?.Invoke(this, prop);
    }
    
    /// <summary>
    /// Fires the property changed event using either the <seealso cref="CallerMemberNameAttribute" /> or the provided string property name.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}