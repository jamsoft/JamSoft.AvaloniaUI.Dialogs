using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Views;

public partial class MyWizardView : UserControl
{
    public MyWizardView()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}