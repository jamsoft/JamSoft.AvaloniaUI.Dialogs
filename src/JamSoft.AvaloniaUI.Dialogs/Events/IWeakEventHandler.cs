namespace JamSoft.AvaloniaUI.Dialogs.Events;

/// <summary>
/// The weak event handler interface
/// </summary>
/// <typeparam name="TE"></typeparam>
public interface IWeakEventHandler<TE> where TE : EventArgs
{
    /// <summary>
    /// The event handler
    /// </summary>
    EventHandler<TE> Handler { get; }
}