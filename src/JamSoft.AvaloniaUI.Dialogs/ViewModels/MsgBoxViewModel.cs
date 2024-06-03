using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.MsgBox;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The default view model for the message box dialog
/// </summary>
public class MsgBoxViewModel : IMsgBoxViewModel
{
    private string? _message;
    private string? _msgBoxTitle;
    private string? _noCommandText;
    private bool _showNoButton;
    private bool _showYesButton;
    private bool _showOkButton;
    private bool _showCancelButton;
    private string? _acceptCommandText;
    private string? _cancelCommandText;
    private ICommand _cancelCommand = null!;
    private ICommand _noCommand = null!;
    private ICommand _acceptCommand = null!;
    private Bitmap? _icon;
    private MsgBoxImage _msgBoxImage;

    /// <summary>
    /// The default constructor
    /// </summary>
    /// <param name="caption"></param>
    /// <param name="message"></param>
    /// <param name="buttons"></param>
    /// <param name="icon"></param>
    /// <param name="noButtonText"></param>
    /// <param name="yesButtonText"></param>
    /// <param name="cancelButtonText"></param>
    public MsgBoxViewModel(string caption, string message, MsgBoxButton buttons, MsgBoxImage icon = MsgBoxImage.None, string? noButtonText = null, string? yesButtonText = null, string? cancelButtonText = null)
    {
        _msgBoxTitle = caption;
        _message = message;
     
        WindowStartupLocation = WindowStartupLocation.CenterOwner;
        NoCommandText = noButtonText;
        AcceptCommandText = yesButtonText;
        CancelCommandText = cancelButtonText;
        Buttons = buttons;
        MsgBoxImage = icon;
        Topmost = false;
        
        SetupCommands();
        SetupButtons();
    }

    /// <summary>
    /// Determines if the dialog has an icon to show
    /// </summary>
    public bool HasIcon => Icon is not null;
    
    /// <summary>
    /// The dialog image enum
    /// </summary>
    public MsgBoxImage MsgBoxImage
    {
        get => _msgBoxImage;
        set
        {
            RaiseAndSetIfChanged(ref _msgBoxImage, value);
            SetImage();
        }
    }

    /// <summary>
    /// Configures the icon for the dialog
    /// </summary>
    protected virtual void SetImage()
    {
        if (MsgBoxImage == MsgBoxImage.Custom)
        {
            return;
        }
        
        switch (MsgBoxImage)
        {
            case MsgBoxImage.Asterisk:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/exclamation.png")));
                break;
            case MsgBoxImage.Information:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/info.png")));
                break;
            case MsgBoxImage.Error:
            case MsgBoxImage.Hand:
            case MsgBoxImage.Stop:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/cross-circle.png")));
                break;
            case MsgBoxImage.Exclamation:
            case MsgBoxImage.Warning:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/diamond-exclamation.png")));
                break;
            case MsgBoxImage.Question:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/interrogation.png")));
                break;
            case MsgBoxImage.Success:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/check-circle.png")));
                break;
            case MsgBoxImage.Battery:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/battery-half.png")));
                break;
            case MsgBoxImage.Database:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/database.png")));
                break;
            case MsgBoxImage.Folder:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/folder-open.png")));
                break;
            case MsgBoxImage.Forbidden:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/ban.png")));
                break;
            case MsgBoxImage.Plus:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/add.png")));
                break;
            case MsgBoxImage.Setting:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/customize.png")));
                break;
            case MsgBoxImage.Wifi:
                Icon = new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/wifi.png")));
                break;
            default:
                Icon = null;
                break;
        }
    }

    /// <summary>
    /// The dialog result
    /// </summary>
    public MsgBoxResult Result { get; set; }
    
    /// <summary>
    /// The dialog buttons
    /// </summary>
    public MsgBoxButton Buttons { get; set; }
    
    /// <summary>
    /// The dialog Startup location
    /// </summary>
    public WindowStartupLocation WindowStartupLocation { get; set; }
    
    /// <summary>
    /// The dialog topmost flag
    /// </summary>
    public bool Topmost { get; set; }
    
    /// <summary>
    /// The dialog icon
    /// </summary>
    public Bitmap? Icon
    {
        get => _icon;
        set => RaiseAndSetIfChanged(ref _icon, value);
    }
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
    public ICommand NoCommand
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
    
    /// <summary>
    /// The message box message
    /// </summary>
    public string? Message
    {
        get => _message;
        set => this.RaiseAndSetIfChanged(ref _message, value);
    }

    /// <summary>
    /// The message box title
    /// </summary>
    public string? MsgBoxTitle
    {
        get => _msgBoxTitle;
        set => this.RaiseAndSetIfChanged(ref _msgBoxTitle, value);
    }

    /// <summary>
    /// The no button text
    /// </summary>
    public string? NoCommandText
    {
        get => _noCommandText;
        set => this.RaiseAndSetIfChanged(ref _noCommandText, value);
    }

    /// <summary>
    /// Boolean flag to show the no button
    /// </summary>
    public bool ShowNoButton
    {
        get => _showNoButton;
        set => this.RaiseAndSetIfChanged(ref _showNoButton, value);
    }

    /// <summary>
    /// Boolean flag to show the yes button
    /// </summary>
    public bool ShowYesButton
    {
        get => _showYesButton;
        set => this.RaiseAndSetIfChanged(ref _showYesButton, value);
    }

    /// <summary>
    /// Boolean flag to show the ok button
    /// </summary>
    public bool ShowOkButton
    {
        get => _showOkButton;
        set => this.RaiseAndSetIfChanged(ref _showOkButton, value);
    }

    /// <summary>
    /// Boolean flag to show the cancel button
    /// </summary>
    public bool ShowCancelButton
    {
        get => _showCancelButton;
        set => this.RaiseAndSetIfChanged(ref _showCancelButton, value);
    }

    /// <summary>
    /// Sets up the various commands for the dialog
    /// </summary>
    private void SetupCommands()
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
            
            if (Buttons == MsgBoxButton.Ok || Buttons == MsgBoxButton.OkCancel)
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

    /// <summary>
    /// Sets up the buttons for the dialog
    /// </summary>
    private void SetupButtons()
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
    protected virtual bool CanAccept()
    {
        return true;
    }

    /// <summary>
    /// Implements the logic that controls the cancel command execution validation
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanCancel()
    {
        return true;
    }
    
    /// <summary>
    /// Implements the logic that controls the accept command execution validation
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanNoAccept()
    {
        return true;
    }
    
    /// <summary>
    /// Invokes the request close dialog event
    /// </summary>
    /// <param name="e"></param>
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