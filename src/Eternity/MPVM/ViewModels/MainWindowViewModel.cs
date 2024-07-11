using CommunityToolkit.Mvvm.ComponentModel;
using Eternity.DiscordIntegration;
using Eternity.MPVM.Pages;
using ReactiveUI;

namespace Eternity.MPVM.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private string? _title;
        private object? _currentPage;
        private readonly Init _discordRpc;

        public string? Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public object? CurrentPage
        {
            get { return _currentPage; }
            set
            {
                SetProperty(ref _currentPage, value);

                // Обновление состояния Discord RPC при смене страницы
                _discordRpc?.SetCurrentPage(value.GetType().Name);
            }
        }

        public MainWindowViewModel()
        {
            Title = "Main Window Title";
            CurrentPage = new MainPage();

            // Инициализация Discord RPC
            _discordRpc = Init.Instance;
            _discordRpc.Initialize();

            // Подписка на сообщения в MessageBus
            MessageBus.Current.Listen<ApplicationLog>()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(log =>
                {
                    // Логика обработки лога, например добавление в лог
                    LogContent += log.Message;
                });

            // Подписка на сообщения о навигации
            MessageBus.Current.Listen<string>()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(Navigate);

            NavigateCommand = ReactiveCommand.Create<string>(Navigate);
        }

        private string _logContent = string.Empty;

        public string LogContent
        {
            get { return _logContent; }
            set { SetProperty(ref _logContent, value); }
        }

        public ReactiveCommand<string, Unit> NavigateCommand { get; }

        private void Navigate(string targetPage)
        {
            switch (targetPage)
            {
                case "MainPage":
                    CurrentPage = new MainPage();
                    break;
                case "TestPage":
                    CurrentPage = new TestPage();
                    break;
                default:
                    throw new ArgumentException($"Неподдерживаемый тип страницы: {targetPage}");
            }
        }
    }
}