namespace tPix.ViewModel
{
    using NynaeveLib.DialogService;
    using NynaeveLib.ViewModel;
    using System.Windows.Input;
    using tPix.BL;
    using tPix.ViewModel.Cmd;

    /// <summary>
    /// View model which supports the buttons pane which is present on the main window.
    /// </summary>
    public class ButtonsPaneViewModel : ViewModelBase
    {
        /// <summary>
        /// The instance of the <see cref="BLManager"/>.
        /// </summary>
        private readonly BLManager bLManager;

        /// <summary>
        /// Initialises a new instance of the <see cref="ButtonsPaneViewModel"/> class.
        /// </summary>
        /// <param name="bLManager">The instance of the <see cref="BLManager"/></param>
        public ButtonsPaneViewModel(
            BLManager bLManager) 
        {
            this.bLManager = bLManager;
            this.ConfigLocationsCommand = 
                new UpdateLocationCmd(
                    this.ShowConfigLocationsWindow);
        }

        /// <summary>
        /// Gets a command which is used to open a locations dialog.
        /// </summary>
        public ICommand ConfigLocationsCommand { get; private set; }

        /// <summary>
        /// Show the config locations window.
        /// </summary>
        private void ShowConfigLocationsWindow()
        {
            // TODO, prior to any changes, this code crashed when opening the dialog.

            LocationUpdateWindowViewModel locationUpdateViewModel =
              new LocationUpdateWindowViewModel(
                this.bLManager.GetLocationsByLetter,
                this.bLManager.SaveLocation,
                this.bLManager.GetLines(),
                this.bLManager.GetCounties(),
                this.bLManager.GetRegions(),
                this.bLManager.GetBig4Regions());

            DialogService service = new DialogService();

            service.ShowDialog(
              new LocationUpdateWindow()
              {
                  DataContext = locationUpdateViewModel
              });
        }
    }
}
