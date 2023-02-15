namespace JamSoft.AvaloniaUI.Dialogs;

public static class DialogServiceFactory
{
    public static IDialogService Create(DialogServiceConfiguration config)
    {
        return new DialogService(config);
    }
}