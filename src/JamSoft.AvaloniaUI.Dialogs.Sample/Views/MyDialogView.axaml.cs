using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Views;

public partial class MyDialogView : UserControl
{
    public MyDialogView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}