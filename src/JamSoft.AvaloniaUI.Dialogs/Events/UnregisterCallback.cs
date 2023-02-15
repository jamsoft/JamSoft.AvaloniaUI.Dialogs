namespace JamSoft.AvaloniaUI.Dialogs.Events;

public delegate void UnregisterCallback<TE>(EventHandler<TE> eventHandler) where TE : EventArgs;