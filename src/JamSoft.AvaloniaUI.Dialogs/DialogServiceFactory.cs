namespace JamSoft.AvaloniaUI.Dialogs;

/// <summary>
/// The dialog service factory
/// </summary>
public static class DialogServiceFactory
{
    /// <summary>
    /// Creates a new instance of the dialog service using the provided configuration object
    /// </summary>
    /// <param name="config">The configuration object instance</param>
    /// <returns>a new instance of <see cref="IDialogService"/></returns>
    public static IDialogService Create(DialogServiceConfiguration config)
    {
        return new DialogService(config);
    }

    public static IMessageBoxService CreateMessageBoxService()
    {
        return new MessageBoxService();
    }
}