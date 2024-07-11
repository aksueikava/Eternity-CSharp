using Eternity.DiscordIntegration;
using Eternity.MPVM;
using ReactiveUI;
using Splat;

namespace Eternity
{
    public partial class App : Application
    {
        private readonly ILogger? _logger;
        private Mutex _mutex;

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
            const string mutexName = "EternityAppMutex";
            _mutex = new Mutex(true, mutexName, out bool isNewInstance);

            if (!isNewInstance)
            {
                var mutexWindow = new MutexWindow();
                mutexWindow.Show();
                return;
            }

            base.OnStartup(e);
            SetupExceptionHandling();

            // Инициализация Discord RPC
            DiscordRpcService.Instance.Initialize();

            var mainWindow = new MainWindow();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DiscordRpcService.Instance.Shutdown();
            _mutex?.ReleaseMutex();
            base.OnExit(e);
        }
    }
}