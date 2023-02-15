using System.ComponentModel;
using System.Runtime.CompilerServices;
using JamSoft.AvaloniaUI.Dialogs.Commands;
using JamSoft.AvaloniaUI.Dialogs.Events;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public class DialogViewModel : INotifyPropertyChanged, IDialogResultVmHelper
{
    private DelegateCommand? _acceptCommand;

    public DelegateCommand? AcceptCommand
    {
        get => _acceptCommand;
        set => SetProperty(ref _acceptCommand, value);
    }

    private string? _acceptCommandText;

    public string? AcceptCommandText
    {
        get => _acceptCommandText;
        set => SetProperty(ref _acceptCommandText, value);
    }

    private DelegateCommand? _cancelCommand;

    public DelegateCommand? CancelCommand
    {
        get => _cancelCommand;
        set => SetProperty(ref _cancelCommand, value);
    }

    private string? _cancelCommandText;

    public string? CancelCommandText
    {
        get => _cancelCommandText;
        set => SetProperty(ref _cancelCommandText, value);
    }

    public event EventHandler<RequestCloseDialogEventArgs>? RequestCloseDialog;

    public DialogViewModel()
    {
        AcceptCommand = new DelegateCommand(() => InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(true)), CanAccept);
        AcceptCommandText = "OK";
        CancelCommand = new DelegateCommand(() => InvokeRequestCloseDialog(new RequestCloseDialogEventArgs(false)));
        CancelCommandText = "Cancel";
    }

    private void InvokeRequestCloseDialog(RequestCloseDialogEventArgs e)
    {
        RequestCloseDialog?.Invoke(this, e);
    }

    protected virtual bool CanAccept()
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
    protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
    {
        if (EqualityComparer<T>.Default.Equals(storage, value))
        {
            return false;
        }

        storage = value;
        OnPropertyChanged(propertyName);
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

    public event PropertyChangedEventHandler? PropertyChanged;
}