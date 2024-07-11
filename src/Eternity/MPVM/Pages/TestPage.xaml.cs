using Eternity.MPVM.ViewModels;
using ReactiveUI;

namespace Eternity.MPVM.Pages
{
    /// <summary>
    /// Логика взаимодействия для TestPage.xaml
    /// </summary>
    public partial class TestPage : Page, IViewFor<TestPageViewModel>
    {
        public static readonly DependencyProperty ViewModelProperty =
            DependencyProperty.Register(nameof(ViewModel), typeof(TestPageViewModel), typeof(TestPage), new PropertyMetadata(null));

        public TestPageViewModel? ViewModel
        {
            get { return (TestPageViewModel?)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        object? IViewFor.ViewModel
        {
            get { return ViewModel; }
            set { ViewModel = (TestPageViewModel?)value; }
        }

        public TestPage()
        {
            InitializeComponent();
            DataContext = new TestPageViewModel();
        }
    }
}
