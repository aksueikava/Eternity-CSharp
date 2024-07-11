using CommunityToolkit.Mvvm.ComponentModel;
using ReactiveUI;

namespace Eternity.MPVM.ViewModels
{
    public class MainPageViewModel : ObservableRecipient
    {
        private string _pageTitle = string.Empty;

        public string PageTitle
        {
            get { return _pageTitle; }
            set { SetProperty(ref _pageTitle, value); }
        }

        public ReactiveCommand<Unit, Unit> NavigateToTestPageCommand { get; }

        public MainPageViewModel()
        {
            PageTitle = "Это главная страница";

            NavigateToTestPageCommand = ReactiveCommand.Create(() =>
            {
                ((MainWindowViewModel)App.Current.MainWindow.DataContext).NavigateToTestPage();
            });
        }
    }
}