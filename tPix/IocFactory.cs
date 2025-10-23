namespace tPix
{
    using CommunityToolkit.Mvvm.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using tPix.BL;
    using tPix.ViewModel;

    /// <summary>
    /// Factory class, used to set up dependency injection.
    /// </summary>
    public static class IocFactory
    {
        /// <summary>
        /// Setup IOC.
        /// </summary>
        public static void Setup()
        {
            Ioc.Default.ConfigureServices(
                new ServiceCollection()
                .AddSingleton<BLManager>()
                .AddSingleton<ButtonsPaneViewModel>()
                .AddSingleton<FiltersPaneViewModel>()
                .AddSingleton<ImagePaneViewModel>()
                .AddSingleton<ListPaneViewModel>()
                .AddSingleton<SelectionPaneViewModel>()
                .AddSingleton<MainWindowViewModel>()
                .BuildServiceProvider());
        }
    }
}
