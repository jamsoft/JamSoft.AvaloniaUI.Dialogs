using System.Windows.Input;

namespace JamSoft.AvaloniaUI.Dialogs.Commands;

public class DelegateCommand : ICommand
{
    private readonly Func<bool>? _canExecute;
    private readonly Action? _execute;
 
    public event EventHandler? CanExecuteChanged;

    public DelegateCommand(Action? execute, Func<bool>? canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }
 
    public bool CanExecute(object parameter)
    {
        if (_canExecute == null) return true;
        return _canExecute();
    }
 
    public void Execute(object parameter)
    {
        if (_execute == null) return;
        _execute();
    }
 
    public void RaiseCanExecuteChanged()
    {
        if( CanExecuteChanged != null )
        {
            CanExecuteChanged(this, EventArgs.Empty);
        }
    }
}