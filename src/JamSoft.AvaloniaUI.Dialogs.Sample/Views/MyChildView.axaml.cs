using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Views;

public partial class MyChildView : UserControl
{
    public MyChildView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}