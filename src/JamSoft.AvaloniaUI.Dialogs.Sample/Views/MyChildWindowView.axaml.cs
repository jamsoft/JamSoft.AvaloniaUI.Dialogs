using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Views;

public partial class MyChildWindowView : UserControl
{
    public MyChildWindowView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}