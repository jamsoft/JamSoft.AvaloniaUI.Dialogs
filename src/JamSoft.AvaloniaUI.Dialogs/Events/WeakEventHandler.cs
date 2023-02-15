namespace JamSoft.AvaloniaUI.Dialogs.Events;

public class WeakEventHandler<T, TE> : IWeakEventHandler<TE> where T : class where TE : EventArgs
{
    private delegate void OpenEventHandler(T @this, object sender, TE e);

    private readonly WeakReference _mTargetRef;
    private readonly OpenEventHandler _mOpenHandler;
    private readonly EventHandler<TE> _mHandler;
    // ReSharper disable once NotAccessedField.Local
    private UnregisterCallback<TE> _mUnregister;

    public WeakEventHandler(EventHandler<TE> eventHandler,
        UnregisterCallback<TE> unregister)
    {
        _mTargetRef = new WeakReference(eventHandler.Target);

        _mOpenHandler = (OpenEventHandler)Delegate.CreateDelegate(
            typeof(OpenEventHandler), null, eventHandler.Method);

        _mHandler = Invoke;
        _mUnregister = unregister;
    }

    public void Invoke(object? sender, TE e)
    {
        T target = (T)_mTargetRef.Target!;

        if (sender != null) _mOpenHandler.Invoke(target, sender, e);
    }

    public EventHandler<TE> Handler
    {
        get { return _mHandler; }
    }

    public static implicit operator EventHandler<TE>(WeakEventHandler<T, TE> weh)
    {
        return weh._mHandler;
    }
}