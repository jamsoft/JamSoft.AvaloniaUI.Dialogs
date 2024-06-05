namespace JamSoft.AvaloniaUI.Dialogs.MsgBox;

/// <summary>
/// The message box result enum
/// </summary>
public sealed class MsgBoxResult
{
    /// <summary>
    /// The message box button result enum
    /// </summary>
    public MsgBoxButtonResult ButtonResult { get; }
    
    /// <summary>
    /// The message box check box result
    /// </summary>
    public bool CheckBoxResult { get; }

    private MsgBoxResult(bool checkBoxResult, MsgBoxButtonResult buttonResult)
    {
        CheckBoxResult = checkBoxResult;
        ButtonResult = buttonResult;
    }
    
    /// <summary>
    /// Creates a new message box result instance
    /// </summary>
    /// <param name="checkBoxChecked"></param>
    /// <param name="buttonResult"></param>
    /// <returns></returns>
    public static MsgBoxResult CreateResult(bool checkBoxChecked, MsgBoxButtonResult buttonResult) => new(checkBoxChecked, buttonResult);
}