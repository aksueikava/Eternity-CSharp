using CommunityToolkit.Mvvm.ComponentModel;

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

        public TestPageViewModel()
        {
            PageTitle = "Это тестовая страница";
        }
    }
}