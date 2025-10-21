namespace tPix.Views
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using System.Windows.Controls;
    using tPix.ViewModel;

    /// <summary>
    /// Interaction logic for ImagePane.xaml
    /// </summary>
    public partial class ImagePane : UserControl
    {
        public ImagePane()
        {
            this.InitializeComponent();

            this.DataContext = Ioc.Default.GetService<ImagePaneViewModel>();
        }
    }
}
