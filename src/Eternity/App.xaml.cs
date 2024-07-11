using Eternity.DiscordIntegration;
using Eternity.MPVM;
using ReactiveUI;
using Splat;

namespace Eternity
{
    public partial class App : Application
    {
        private readonly ILogger? _logger;

        public App()
        {
            AppContainer.BuildIoC(); // Настраиваем IoC 
            _logger = Locator.Current.GetService<ILogger>();
        }

        private void LogException(Exception e, string source)
        {
            _logger?.Error($"{source}: {e.Message}", e);
        }

        private void SetupExceptionHandling()
        {
            // Подключение Observer-обработчика исключений
            RxApp.DefaultExceptionHandler = new ApcExceptionHandler(_logger);
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SetupExceptionHandling();

            // Инициализация Discord RPC
            Init.Instance.Initialize();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Init.Instance.Shutdown();
            base.OnExit(e);
        }
    }
}