﻿using JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;
using Splat;

namespace JamSoft.AvaloniaUI.Dialogs.Sample;

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
        services.Register(() => new MyChildWindowViewModel());
        services.Register(() => new CustomBaseChildWindowViewModel());
    }
}