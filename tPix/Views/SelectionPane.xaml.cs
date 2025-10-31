namespace tPix.Views
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using System.Windows.Controls;
    using tPix.ViewModel;

    /// <summary>
    /// Interaction logic for SelectionPane.xaml
    /// </summary>
    public partial class SelectionPane : UserControl
    {
        public SelectionPane()
        {
            this.InitializeComponent();

            this.DataContext = Ioc.Default.GetService<SelectionPaneViewModel>();
        }
    }
}
