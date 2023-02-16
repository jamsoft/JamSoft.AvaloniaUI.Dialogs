using JamSoft.Helpers.Configuration;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Models;

public class MyUserSettings : SettingsBase<MyUserSettings>
{
    public double Left { get; set; } = 50;
    public double Top { get; set; } = 50;
    public double Height { get; set; } = 600;
    public double Width { get; set; } = 800;
}