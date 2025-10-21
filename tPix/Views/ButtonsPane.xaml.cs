namespace tPix.Views
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using System.Windows.Controls;
    using tPix.ViewModel;

    /// <summary>
    /// Interaction logic for ButtonsPane.xaml
    /// </summary>
    public partial class ButtonsPane : UserControl
    {
        public ButtonsPane()
        {
            this.InitializeComponent();

            this.DataContext = Ioc.Default.GetService<ButtonsPaneViewModel>();
        }
    }
}
