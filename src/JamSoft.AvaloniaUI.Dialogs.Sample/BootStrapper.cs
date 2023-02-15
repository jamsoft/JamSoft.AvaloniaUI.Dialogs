using JamSoft.AvaloniaUI.Dialogs.Sample.ViewModels;
using Splat;

namespace JamSoft.AvaloniaUI.Dialogs.Sample;

public class BootStrapper
{
    public static void Register(IMutableDependencyResolver services, IReadonlyDependencyResolver resolver)
    {
        services.RegisterLazySingleton(() => DialogServiceFactory.Create(new DialogServiceConfiguration()));
        
        services.Register(() => new MainWindowViewModel(resolver.GetService<IDialogService>()));
    }
}