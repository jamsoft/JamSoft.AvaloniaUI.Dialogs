namespace JamSoft.AvaloniaUI.Dialogs.MsgBox;

public sealed class MsgBoxResult
{
    public MsgBoxButtonResult ButtonResult { get; }
    
    public bool CheckBoxResult { get; }

    private MsgBoxResult(bool checkBoxResult, MsgBoxButtonResult buttonResult)
    {
        CheckBoxResult = checkBoxResult;
        ButtonResult = buttonResult;
    }
    
    public static MsgBoxResult CreateResult(bool checkBoxChecked, MsgBoxButtonResult buttonResult) => new(checkBoxChecked, buttonResult);
}