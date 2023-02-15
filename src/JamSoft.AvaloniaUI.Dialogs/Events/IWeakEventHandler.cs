namespace JamSoft.AvaloniaUI.Dialogs.Events;

public interface IWeakEventHandler<TE> where TE : EventArgs
{
    EventHandler<TE> Handler { get; }
}