using JamSoft.Helpers.Configuration;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Models;

public class MyUserSettings : SettingsBase<MyUserSettings>
{
    public double Left { get; set; }
    public double Top { get; set; }
    public double Height { get; set; }
    public double Width { get; set; }
}