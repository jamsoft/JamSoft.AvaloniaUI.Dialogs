using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace JamSoft.AvaloniaUI.Dialogs.MsgBox;

public static class IconResolver
{
    private static Dictionary<MsgBoxImage, string> _icons = new()
    {
        { MsgBoxImage.Asterisk, "exclamation.png" },
        { MsgBoxImage.Exclamation, "diamond-exclamation.png" },
        { MsgBoxImage.Hand, "cross-circle.png" },
        { MsgBoxImage.Stop, "cross-circle.png" },
        { MsgBoxImage.Error, "cross-circle.png" },
        { MsgBoxImage.Question, "interrogation.png" },
        { MsgBoxImage.Warning, "diamond-exclamation.png" },
        { MsgBoxImage.Information, "info.png" },
        { MsgBoxImage.Custom, "Custom" },
        { MsgBoxImage.Success, "check-circle.png" },
        { MsgBoxImage.Battery, "battery-half.png" },
        { MsgBoxImage.Database, "database.png" },
        { MsgBoxImage.Folder, "folder-open.png" },
        { MsgBoxImage.Forbidden, "ban.png" },
        { MsgBoxImage.Plus, "add.png" },
        { MsgBoxImage.Setting, "customize.png" },
        { MsgBoxImage.Wifi, "wifi.png" }
    };
    
    public static Bitmap? Resolve(MsgBoxImage icon)
    {
        if (_icons.TryGetValue(icon, out var iconName))
        {
            return new Bitmap(AssetLoader.Open(new Uri($"avares://JamSoft.AvaloniaUI.Dialogs/Assets/{iconName}")));
        }

        return null;
    }
}