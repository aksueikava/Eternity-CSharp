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

        public ReactiveCommand<Unit, Unit> NavigateToMainPageCommand { get; }

        public TestPageViewModel()
        {
            PageTitle = "Это тестовая страница";

            NavigateToMainPageCommand = ReactiveCommand.Create(() =>
            {
                ((MainWindowViewModel)App.Current.MainWindow.DataContext).NavigateToMainPage();
            });
        }
    }
}