using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Views;

public partial class CustomBaseChildWindowView : UserControl
{
    public CustomBaseChildWindowView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}