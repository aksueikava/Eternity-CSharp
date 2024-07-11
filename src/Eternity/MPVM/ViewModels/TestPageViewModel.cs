using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace Eternity.MPVM.ViewModels
{
    public class TestPageViewModel : ObservableRecipient
    {
        private string _pageTitle = string.Empty;

        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }

        public ReactiveCommand<string, Unit> NavigateToPageCommand { get; }

        public TestPageViewModel()
        {
            PageTitle = "Это тестовая страница";

            NavigateToPageCommand = ReactiveCommand.Create<string>(Navigate);
        }

        private void Navigate(string pageName)
        {
            // Отправляем сообщение о навигации на указанную страницу
            MessageBus.Current.SendMessage(pageName);
        }
    }
}