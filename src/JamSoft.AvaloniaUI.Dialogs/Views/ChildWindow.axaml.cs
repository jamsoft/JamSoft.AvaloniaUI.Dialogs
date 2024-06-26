﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Reactive;
using JamSoft.AvaloniaUI.Dialogs.Events;
using JamSoft.AvaloniaUI.Dialogs.ViewModels;

namespace JamSoft.AvaloniaUI.Dialogs.Views;

/// <summary>
/// The default child window
/// </summary>
public partial class ChildWindow : Window
{
    private bool _isClosed;

    private IChildWindowViewModel? _vm;

    /// <summary>
    /// The default constructor
    /// </summary>
    public ChildWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        this.FindControl<DockPanel>("ChromeDockPanel")!.PointerPressed += OnChromePointerPressed;
        this.FindControl<ContentControl>("Host")!.DataContextChanged += DialogPresenterDataContextChanged;
        Closed += ChildWindowClosed;
        PositionChanged += OnPositionChanged;
    }

    private void OnPositionChanged(object? sender, PixelPointEventArgs e)
    {
        if (_vm == null) return;
        
        _vm.RequestedLeft = e.Point.X;
        _vm.RequestedTop = e.Point.Y;
    }
    
    private void OnChromePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (_vm == null) return;
        
        var p = e.GetCurrentPoint(null);
        if (p.Properties.IsLeftButtonPressed)
        {
            this.GetObservable(ClientSizeProperty).Subscribe(new AnonymousObserver<Size>((_)=>
            {
                _vm.RequestedLeft = Position.X;
                _vm.RequestedTop = Position.Y;
            }));

            BeginMoveDrag(e);
            e.Handled = false;
        }
    }
    
    void ChildWindowClosed(object? sender, EventArgs e)
    {
        PointerPressed -= OnChromePointerPressed;
        PositionChanged -= OnPositionChanged;
        Closed -= ChildWindowClosed;
        _isClosed = true;
    }
    
    private void DialogPresenterDataContextChanged(object? sender, EventArgs e)
    {
        _vm = DataContext as IChildWindowViewModel;
        var d = DataContext as IDialogResultVmHelper;
        var windowPositionAware = DataContext as IWindowPositionAware;

        if (d == null)
        {
            return;
        }

        d.RequestCloseDialog += new EventHandler<RequestCloseDialogEventArgs>(DialogResultTrueEvent)
            .MakeWeak(eh => d.RequestCloseDialog -= eh);
		
        if (windowPositionAware == null) return;
		
        Position = new PixelPoint(
            Convert.ToInt32(windowPositionAware.RequestedLeft), 
            Convert.ToInt32(windowPositionAware.RequestedTop));
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
    
    private void DialogResultTrueEvent(object? sender, RequestCloseDialogEventArgs e)
    {
        // Important: Do not set DialogResult for a closed window
        // GC clears windows anyway and with MakeWeak it
        // closes out with IDialogResultVMHelper
        if (_isClosed)
        {
            return;
        }

        Close();
    }
}