namespace tPix
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using System.Windows;
    using tPix.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.DataContext = Ioc.Default.GetService<MainWindowViewModel>();
        }
    }
}
