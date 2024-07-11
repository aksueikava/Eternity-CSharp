using CommunityToolkit.Mvvm.ComponentModel;

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

        public MainPageViewModel()
        {
            PageTitle = "Это главная страница";
        }
    }
}