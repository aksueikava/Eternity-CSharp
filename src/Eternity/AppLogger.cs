using ReactiveUI;

namespace Eternity
{
    public class AppLogger : ILogger
    {
        // Экземпляр логгера NLog
        private readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public AppLogger() { }

        public void Info(string message)
        {
            _logger.Info(message);
            MessageBus.Current.SendMessage(new ApplicationLog(message));
        }

        public void Error(string message, Exception exception = null)
        {
            _logger.Error(exception, message);

            // Отправляем сообщение в шину
            MessageBus.Current.SendMessage(new ApplicationLog(message));
        }
    }

    public class ApcExceptionHandler : IObserver<Exception>
    {
        private readonly ILogger _logger;

#pragma warning disable IDE0290 // Использовать основной конструктор
        public ApcExceptionHandler(ILogger logger)
        {
            _logger = logger;
        }
#pragma warning restore IDE0290 // Использовать основной конструктор

        public void OnCompleted()
        {
            if (Debugger.IsAttached) Debugger.Break();
        }

        public void OnError(Exception error)
        {
            if (Debugger.IsAttached) Debugger.Break();
            _logger.Error($"{error.Source}: {error.Message}", error);
        }

        public void OnNext(Exception value)
        {
            if (Debugger.IsAttached) Debugger.Break();

            _logger?.Error($"{value.Source}: {value.Message}", value);
        }
    }

    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception exception = null);
    }

    public class ApplicationLog(string message)
    {
        public string Message { get; } = message;
    }
}