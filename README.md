# Introduction

Provides the ability to perform various dialog and child window tasks in a DI injectable application dialog service ready to plug into MVVM AvaloniaUI applications.

This idea is to make it as simple as possible to handle all the basics of using dialogs with as few assumptions as possible whilst also providing a feature rich experience.

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
# Usage - File Paths

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
You can also make use of the built in `CommonFilters` for this task.
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
### Save A Specific File Type
```csharp
string path = await _dialogService.SaveFile("Save Word File", new List<FileDialogFilter>
{
    new()
    {
        Name = "Docx Word File", 
        Extensions = new List<string> { "docx" }
    }
});
```
# Usage - Dialogs
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