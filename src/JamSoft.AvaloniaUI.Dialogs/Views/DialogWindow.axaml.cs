using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Views;

public partial class DialogWindow : Window
{
    private bool _isClosed = false;
    
    public DialogWindow()
    {
        InitializeComponent();
// #if DEBUG
//         this.AttachDevTools();
// #endif
        
        this.FindControl<ContentControl>("Host").DataContextChanged += DialogPresenterDataContextChanged;
        Closed += DialogWindowClosed;
    }
    
    void DialogWindowClosed(object? sender, EventArgs e)
    {
        Closed -= DialogWindowClosed;
        _isClosed = true;
    }
    
    private void DialogPresenterDataContextChanged(object? sender, EventArgs e)
    {
        var d = DataContext as IDialogResultVmHelper;

        if (d == null)
        {
            return;
        }

        d.RequestCloseDialog += new EventHandler<RequestCloseDialogEventArgs>(DialogResultTrueEvent)
            .MakeWeak(eh => d.RequestCloseDialog -= eh);
    }

    private void DialogResultTrueEvent(object? sender, RequestCloseDialogEventArgs eventargs)
    {
        // Important: Do not set DialogResult for a closed window
        // GC clears windows anyways and with MakeWeak it
        // closes out with IDialogResultVMHelper
        if (_isClosed)
        {
            return;
        }
	
        Close(eventargs.DialogResult);
    }
    
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}