namespace JamSoft.AvaloniaUI.Dialogs.Events;

/// <summary>
/// Event handler extensions
/// </summary>
public static class EventHandlerUtils
{
    /// <summary>
    /// Makes a weak handler
    /// </summary>
    /// <typeparam name="TE"></typeparam>
    /// <param name="eventHandler"></param>
    /// <param name="unregister"></param>
    /// <returns></returns>
    public static EventHandler<TE>? MakeWeak<TE>(this EventHandler<TE> eventHandler, UnregisterCallback<TE> unregister) where TE : EventArgs
    {
        if (eventHandler == null)
        {
            throw new ArgumentNullException(nameof(eventHandler));
        }

        if (eventHandler.Method.IsStatic || eventHandler.Target == null)
        {
            throw new ArgumentException(@"Only instance methods are supported.", nameof(eventHandler));
        }

        if (eventHandler.Method.DeclaringType != null)
        {
            var wehType = typeof(WeakEventHandler<,>).MakeGenericType(eventHandler.Method.DeclaringType, typeof(TE));

            var wehConstructor = wehType.GetConstructor(new Type[]
            {
                typeof(EventHandler<TE>), typeof(UnregisterCallback<TE>)
            });

            if (wehConstructor != null)
            {
                IWeakEventHandler<TE> weh = (IWeakEventHandler<TE>)wehConstructor.Invoke(new object[] { eventHandler, unregister });

                return weh.Handler;
            }
        }

        return null;
    }
}