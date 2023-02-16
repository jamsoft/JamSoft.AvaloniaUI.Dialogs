using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

/// <summary>
/// The default dialog view model
/// </summary>
public class DialogViewModel : IDialogViewModel
{
    private ICommand _acceptCommand;

    /// <summary>
    /// The dialog accept command
    /// </summary>
    public ICommand AcceptCommand
    {
        get => _acceptCommand;
        set => RaiseAndSetIfChanged(ref _acceptCommand, value);
    }

    private string? _acceptCommandText;

    /// <summary>
    /// The dialog cancel command
    /// </summary>
    public string? AcceptCommandText
    {
        get => _acceptCommandText;
        set => RaiseAndSetIfChanged(ref _acceptCommandText, value);
    }

    private ICommand _cancelCommand;

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

    private string? _cancelCommandText;

    /// <summary>
    /// The dialog cancel command text
    /// </summary>
    public string? CancelCommandText
    {
        get => _cancelCommandText;
        set => RaiseAndSetIfChanged(ref _cancelCommandText, value);
    }

    /// <summary>
    /// Occurs when [request close dialog] event is fired.
    /// </summary>
    public event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog;

    /// <summary>
    /// The default constructor
    /// </summary>
    protected DialogViewModel()
    {
        _acceptCommand = new DelegateCommand(() => InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(true)), CanAccept);
        AcceptCommandText = "OK";
        _cancelCommand = new DelegateCommand(() => InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(false)), CanCancel);
        CancelCommandText = "Cancel";
    }

    private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
    {
        RequestCloseDialog?.Invoke(this, e);
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

    /// <summary>
    /// 
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;
}