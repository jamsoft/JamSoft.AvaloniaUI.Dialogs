namespace JamSoft.AvaloniaUI.Dialogs;

/// <summary>
/// Provides startup configuration details
/// </summary>
public class DialogServiceConfiguration
{
    /// <summary>
    /// Set the application name in titles
    /// </summary>
    public bool UseApplicationNameInTitle { get; set; }
    
    /// <summary>
    /// The application name to display in titles
    /// </summary>
    public string? ApplicationName { get; set; }
    
    /// <summary>
    /// All
    /// </summary>
    public string? ViewsAssemblyName { get; set; }
}