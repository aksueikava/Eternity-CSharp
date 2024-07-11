using CommunityToolkit.Mvvm.ComponentModel;
using Eternity.MPVM.Pages;
using ReactiveUI;
using System.Reactive.Linq;

namespace Eternity.MPVM.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {
        private string? _title;
        private object? _currentPage;

        public string? Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public object? CurrentPage
        {
            get { return _currentPage; }
            set { SetProperty(ref _currentPage, value); }
        }

        public MainWindowViewModel()
        {
            Title = "Main Window Title";
            CurrentPage = new MainPage();

            // Подписываемся на сообщения в MessageBus
            MessageBus.Current.Listen<ApplicationLog>()
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(log =>
                {
                    // Логика обработки лога, например добавление в лог
                    LogContent += log.Message;
                });
        }

        private string _logContent = string.Empty;

        public string LogContent
        {
            get { return _logContent; }
            set { SetProperty(ref _logContent, value); }
        }

        public void NavigateToTestPage()
        {
            CurrentPage = new TestPage();
        }

        public void NavigateToMainPage()
        {
            CurrentPage = new MainPage();
        }
    }
}
