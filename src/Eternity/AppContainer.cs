using Autofac;
using Eternity.MPVM;
using ReactiveUI;
using Mapster;
using Splat.Autofac;
using Eternity.MPVM.ViewModels;
using Eternity.MPVM.Pages;

namespace Eternity
{
    public static class AppContainer
    {
        public static void BuildIoC()
        {
            /*
			 * Создание контейнера Autofac.
			 * Регистрирация сервисов и представления
			 */
            var builder = new ContainerBuilder();
            RegisterServices(builder);
            RegisterViews(builder);

            // Регистрирация Autofac контейнера в Splat
            var autofacResolver = builder.UseAutofacDependencyResolver();
            builder.RegisterInstance(autofacResolver);

            // Вызываем InitializeReactiveUI(), чтобы переопределить дефолтный Service Locator
            autofacResolver.InitializeReactiveUI();

            var lifetimeScope = builder.Build();
            autofacResolver.SetLifetimeScope(lifetimeScope);
        }

        private static void RegisterServices(ContainerBuilder builder)
        {
            //builder.RegisterModule(new ApcCoreModule());
            builder.RegisterType<AppLogger>().As<ILogger>();

            // Регистрируем профили ObjectMapper путем сканирования сборки
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        }

        private static void RegisterViews(ContainerBuilder builder)
        {
            builder.RegisterType<MainWindow>().As<IViewFor<MainWindowViewModel>>();
            builder.RegisterType<MainPage>().As<IViewFor<MainPageViewModel>>();
            builder.RegisterType<TestPage>().As<IViewFor<TestPageViewModel>>();
            builder.RegisterType<MainWindowViewModel>();
            builder.RegisterType<MainPageViewModel>();
            builder.RegisterType<TestPageViewModel>();
        }
    }
}
