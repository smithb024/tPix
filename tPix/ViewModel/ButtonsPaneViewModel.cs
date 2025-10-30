namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System.Windows.Input;
    using tPix.ViewModel.Cmd;

    /// <summary>
    /// View model which supports the buttons pane which is present on the main window.
    /// </summary>
    public class ButtonsPaneViewModel : ViewModelBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ButtonsPaneViewModel"/> class.
        /// </summary>
        public ButtonsPaneViewModel() 
        {
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

            ////LocationUpdateWindowViewModel locationUpdateViewModel =
            ////  new LocationUpdateWindowViewModel(
            ////    this.BLL.GetLocationsByLetter,
            ////    this.BLL.SaveLocation,
            ////    this.Lines,
            ////    this.Counties,
            ////    this.Regions,
            ////    this.Big4Regions);

            ////DialogService service = new DialogService();

            ////service.ShowDialog(
            ////  new LocationUpdateWindow()
            ////  {
            ////      DataContext = locationUpdateViewModel
            ////  });
        }
    }
}
