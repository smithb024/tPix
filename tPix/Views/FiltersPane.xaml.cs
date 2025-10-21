namespace tPix.Views
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using System.Windows.Controls;
    using tPix.ViewModel;

    /// <summary>
    /// Interaction logic for FiltersPane.xaml
    /// </summary>
    public partial class FiltersPane : UserControl
    {
        public FiltersPane()
        {
            this.InitializeComponent();

            this.DataContext = Ioc.Default.GetService<FiltersPaneViewModel>();
        }
    }
}
