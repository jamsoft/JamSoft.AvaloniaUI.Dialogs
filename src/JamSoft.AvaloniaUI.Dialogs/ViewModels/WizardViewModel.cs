using System.Windows.Input;
using JamSoft.AvaloniaUI.Dialogs.Commands;

namespace JamSoft.AvaloniaUI.Dialogs.ViewModels;

public interface IWizardViewModel : IChildWindowViewModel
{
    ICommand PreviousPageCommand { get; set; }
    ICommand NextPageCommand { get; set; }
}

public class WizardViewModel : ChildWindowViewModel, IWizardViewModel
{
    private ICommand _nextPageCommand;
    private ICommand _previousPageCommand;

    public ICommand PreviousPageCommand
    {
        get => _previousPageCommand;
        set => RaiseAndSetIfChanged(ref _previousPageCommand, value);
    }

    public ICommand NextPageCommand
    {
        get => _nextPageCommand;
        set => RaiseAndSetIfChanged(ref _nextPageCommand, value);
    }

    protected WizardViewModel()
    {
        _previousPageCommand = new DelegateCommand(PreviousPageCommandExecuted);
        _nextPageCommand = new DelegateCommand(NextPageCommandExecuted);
    }

    public virtual void PreviousPageCommandExecuted()
    {
        throw new NotImplementedException();
    }

    public virtual void NextPageCommandExecuted()
    {
        throw new NotImplementedException();
    }
}