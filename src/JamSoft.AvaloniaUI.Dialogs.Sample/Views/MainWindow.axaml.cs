using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using JamSoft.AvaloniaUI.Dialogs.Sample.Models;

namespace JamSoft.AvaloniaUI.Dialogs.Sample.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.AttachDevTools();
    }

    private void TopLevel_OnClosed(object? sender, EventArgs e)
    {
        MyUserSettings.Save();
    }
}