namespace JamSoft.AvaloniaUI.Dialogs.Events;

/// <summary>
/// The unregister delegate
/// </summary>
/// <typeparam name="TE"></typeparam>
public delegate void UnregisterCallback<TE>(EventHandler<TE> eventHandler) where TE : EventArgs;