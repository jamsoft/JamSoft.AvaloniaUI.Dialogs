using System.Windows.Input;

namespace JamSoft.AvaloniaUI.Dialogs.Commands;

/// <summary>
/// The default delegate command
/// </summary>
public class DelegateCommand : ICommand
{
    private readonly Func<bool>? _canExecute;
    private readonly Action? _execute;
 
    /// <summary>
    /// The can execute changed handler
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// The default constructor 
    /// </summary>
    /// <param name="execute"></param>
    /// <param name="canExecute"></param>
    public DelegateCommand(Action? execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
 
    /// <summary>
    /// The can execute method
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object parameter)
    {
        if (_canExecute == null) return true;
        return _canExecute();
    }
 
    /// <summary>
    /// The execute method
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object parameter)
    {
        if (_execute == null) return;
        _execute();
    }
 
    /// <summary>
    /// Raises the can execute change event
    /// </summary>
    public void RaiseCanExecuteChanged()
    {
        if( CanExecuteChanged != null )
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}