using Eternity.MPVM.ViewModels;
using ReactiveUI;

namespace Eternity.MPVM.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page, IViewFor<MainPageViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(MainPageViewModel), typeof(MainPage), new PropertyMetadata(null));

        public MainPageViewModel? ViewModel
        {
            get { return (MainPageViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object? IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (MainPageViewModel?)value; }
        }

        public MainPage()
        {
            InitializeComponent();
            DataContext = new MainPageViewModel();
        }
    }
}