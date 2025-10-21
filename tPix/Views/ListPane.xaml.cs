namespace tPix.Views
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using System.Windows.Controls;
    using tPix.ViewModel;

    /// <summary>
    /// Interaction logic for ListPane.xaml
    /// </summary>
    public partial class ListPane : UserControl
    {
        public ListPane()
        {
            this.InitializeComponent();

            this.DataContext = Ioc.Default.GetService<ListPaneViewModel>();
        }
    }
}
