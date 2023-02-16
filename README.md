# Introduction

Provides the ability to show various dialogs and child windows in a DI injectable application dialog service ready to plug into MVVM AvaloniaUI applications.

The general idea is to make it as simple as possible to handle all the basics of using dialogs with as few assumptions as possible whilst also providing a feature rich experience.

More or less everything is replaceable, extendable, customisable.

## GitHub Pages Site

https://jamsoft.github.io/JamSoft.AvaloniaUI.Dialogs/

## Installation
```shell
dotnet add package JamSoft.AvaloniaUI.Dialogs --version 1.0.0
```
```shell
Install-Package JamSoft.AvaloniaUI.Dialogs -Version 1.0.0
```
```xml
<PackageReference Include="JamSoft.AvaloniaUI.Dialogs" Version="1.0.0" />
```
```shell
paket add JamSoft.AvaloniaUI.Dialogs --version 1.0.0
```
## Import Styles
### All Defaults
```xml
<Application.Styles>
    <FluentTheme Mode="Dark"/>
    <StyleInclude Source="avares://JamSoft.AvaloniaUI.Dialogs/Themes/Default.axaml"/>
</Application.Styles>
```
### Individual
```xml
<Application.Styles>
    <FluentTheme Mode="Dark"/>
    <StyleInclude Source="avares://JamSoft.AvaloniaUI.Dialogs/Themes/ChildStyle.axaml"/>
    <StyleInclude Source="avares://JamSoft.AvaloniaUI.Dialogs/Themes/ModalStyle.axaml"/>
</Application.Styles>
```
### Custom Styling
Since we are using plain old `Window` objects, basic styling properties like `Background` colors will be inherited from your own applications default `Window` style, such as:
```xml
<Style Selector="Window">
    <Setter Property="Background" Value="#333333" />
</Style>
```

## Registration Example Using Splat DI

```csharp
public static void Main(string[] args)
{
    RegisterDependencies();
    
    BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
}

private static void RegisterDependencies() =>
    BootStrapper.Register(Locator.CurrentMutable, Locator.Current);
        
```
#### Registering the Service - Bootstrapper.cs
```csharp
public class BootStrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton(() => DialogServiceFactory.Create(new DialogServiceConfiguration
        {
            ApplicationName = "Dialog Sample App", 
            UseApplicationNameInTitle = true,
            ViewsAssemblyName = "JamSoft.AvaloniaUI.Dialogs.Sample"
        }));
        
        services.Register(() => new MainWindowViewModel(resolver.GetService<IDialogService>()));
        services.Register(() => new MyDialogViewModel());
        services.Register(() => new MyChildViewModel());
    }
}        
```
# Usage

## File Paths

Now that we have this setup and registered, we can make use of the service from view models like this. First add it as a constructor parameter.
```csharp
private readonly IDialogService _dialogService;

public MainWindowViewModel(IDialogService dialogService)
{
    _dialogService = dialogService;
}
...
```
### Open Any File
```csharp
string path = await _dialogService.OpenFile("Open Any File");
```
### Open A Specific File Type
```csharp
string path = await _dialogService.OpenFile("Open Word File", new List<FileDialogFilter>
{
    new()
    {
        Name = "Docx Word File", 
        Extensions = new List<string> { "docx" }
    }
});
```
You can also make use of the built in `CommonFilters` helper class.
```csharp
string path = await _dialogService.OpenFile("Open Word File", new List<FileDialogFilter>
{
    CommonFilters.WordFilter
});
```
### Open Multiple Files
```csharp
string[] paths = await _dialogService.OpenFiles("Open Multiple Files");
```
### Save Any Path
```csharp
string path = await _dialogService.SaveFile("Save Any File");
```
### Save To Your Custom File Type
```csharp
string path = await _dialogService.SaveFile("Save New MyApp Project", new List<FileDialogFilter>
{
    new()
    {
        Name = "MyApp Project", 
        Extensions = new List<string> { "myappext" }
    }
});
```
# Usage - Dialogs

There are two base view model classes already baked in for ease of use of the library. These are provided as defaults and a starting point. Create a suitable view model and inherit from either `DialogViewModel` or `ChildWindowViewModel` as base class.

## Show Dialog
```csharp
_dialogService.ShowDialog(Locator.Current.GetService<MyDialogViewModel>(), DialogCallback);

private void DialogCallback(MyDialogViewModel obj)
{
    Message = obj.DialogMessage;
}
```
### Custom Button Text
```csharp
private void ShowCustomizedDialogCommandExecuted()
{
    var vm = Locator.Current.GetService<MyDialogViewModel>();
    vm.AcceptCommandText = "Accept";
    vm.CancelCommandText = "Oh No!";
    
    _dialogService.ShowDialog(vm, DialogCallback);
}
```
### Alternate View
```csharp
private void ShowCustomizedDialogCommandExecuted()
{
    var vm = Locator.Current.GetService<MyDialogViewModel>();
    vm.AcceptCommandText = "Accept";
    vm.CancelCommandText = "Oh No!";
    
    _dialogService.ShowDialog(new MyAlternateDialogView(), vm, DialogCallback);
}
```
## Show Child Window
```csharp
private void ShowChildWindowCommandExecuted()
{
    var vm = Locator.Current.GetService<MyChildViewModel>();

    // these values could be stored in user settings and loaded at runtime etc.
    vm.RequestedLeft = 50;
    vm.RequestedTop = 50;
    vm.RequestedHeight = 600;
    vm.RequestedWidth = 800;
    vm.ChildMessage = "Child Message Value";
    vm.ChildWindowTitle = "My Child Window Title";
    
    _dialogService.ShowChildWindow(vm, model =>
    {
        Message = $"Child Closed - {model.ChildMessage}";
    });
}
```

The child windows are draggable and also update these properties in real-time. This means that your application can easily restore child window positions between application runs by storing these values.

## Show Child Window - Alternate View Parameter
```csharp
private void ShowChildWindowCommandExecuted()
{
    var vm = Locator.Current.GetService<MyChildViewModel>();

    ...
    
    _dialogService.ShowChildWindow(new MyAlternateChildView(), vm, model =>
    {
        Message = $"Child Closed - {model.ChildMessage}";
    });
}
```
## Saving & Restoring Window Positions
First you need a mechanism to store positions as set by the user moving things around.
```csharp
public class MyUserSettings : SettingsBase<MyUserSettings>
{
    public double Left { get; set; } = 50;
    public double Top { get; set; } = 50;
    public double Height { get; set; } = 600;
    public double Width { get; set; } = 800;
}
```
`SettingsBase<T>` can be found in the JamSoft.Helpers package https://github.com/jamsoft/JamSoft.Helpers

```xml
Install-Package JamSoft.Helpers
```
Then in your view model you can listen for the `RequestCloseDialog` event and respond accordingly by storing the settings in the `OnRequestCloseDialog` method.
```csharp
public class MyChildWindowViewModel : ChildWindowViewModel
{
    private string? _childMessage;
    
    public MyChildWindowViewModel()
    {
        RequestCloseDialog += OnRequestCloseDialog;
    }
    
    public string? ChildMessage
    {
        get => _childMessage;
        set => RaiseAndSetIfChanged(ref _childMessage, value);
    }
    
    private void OnRequestCloseDialog(object? sender, RequestCloseDialogEventArgs e)
    {
        MyUserSettings.Instance.Top = RequestedTop;
        MyUserSettings.Instance.Left = RequestedLeft;
        MyUserSettings.Instance.Width = RequestedWidth;
        MyUserSettings.Instance.Height = RequestedHeight;
        
        RequestCloseDialog -= OnRequestCloseDialog;
    }
}
```
The next time this view model is requested by the user you can then restore these values.
```csharp
vm.RequestedLeft = MyUserSettings.Instance.Left;
vm.RequestedTop = MyUserSettings.Instance.Top;
vm.RequestedHeight = MyUserSettings.Instance.Height;
vm.RequestedWidth = MyUserSettings.Instance.Width;

vm.ChildWindowTitle = "My Custom Child Window Title Auto Find";

_dialogService.ShowChildWindow(vm, model =>
{
    Message = $"Child Remember Position Closed - {model.GetType()}";
});
```
See the Sample Application for a complete implementation example and guidance.
