namespace tPix.ViewModel
{
    using NynaeveLib.Commands;
    using NynaeveLib.ViewModel;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using tPix.BL;
    using tPix.BL.Interfaces;
    using tPix.BL.Model;

    /// <summary>
    /// View model which supports the Location Configuration dialog.
    /// </summary>
    public class LocationUpdateWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The instance of the <see cref="BLManager"/>.
        /// </summary>
        private readonly BLManager blManager;

        private Func<string, ObservableCollection<Location>> getLocations;

        ObservableCollection<LocationConfiguratorViewModel> locationViewModels;
        ObservableCollection<LetterButtonViewModel> letterButtonViewModels;

        private ObservableCollection<string> lines;
        private ObservableCollection<string> counties;
        private ObservableCollection<string> regions;
        private ObservableCollection<string> big4Regions;

        /// <summary>
        /// Initialises a new instance of the <see cref="LocationUpdateWindowViewModel"/> class.
        /// </summary>
        /// <param name="blManager">The instance of the <see cref="BLManager"/></param>
        public LocationUpdateWindowViewModel(
            BLManager blManager)
        {
            this.blManager = blManager;
            this.getLocations = this.blManager.GetLocationsByLetter;
            this.lines = this.blManager.GetLines();
            this.counties = this.blManager.GetCounties();
            this.regions = this.blManager.GetRegions();
            this.big4Regions = this.blManager.GetBig4Regions();

            this.SetLocations("A");

            this.letterButtonViewModels =
                new ObservableCollection<LetterButtonViewModel>
                {
                    new LetterButtonViewModel("A", this.SetLocations),
                    new LetterButtonViewModel("B", this.SetLocations),
                    new LetterButtonViewModel("C", this.SetLocations),
                    new LetterButtonViewModel("D", this.SetLocations),
                    new LetterButtonViewModel("E", this.SetLocations),
                    new LetterButtonViewModel("F", this.SetLocations),
                    new LetterButtonViewModel("G", this.SetLocations),
                    new LetterButtonViewModel("H", this.SetLocations),
                    new LetterButtonViewModel("I", this.SetLocations),
                    new LetterButtonViewModel("J", this.SetLocations),
                    new LetterButtonViewModel("K", this.SetLocations),
                    new LetterButtonViewModel("L", this.SetLocations),
                    new LetterButtonViewModel("M", this.SetLocations),
                    new LetterButtonViewModel("N", this.SetLocations),
                    new LetterButtonViewModel("O", this.SetLocations),
                    new LetterButtonViewModel("P", this.SetLocations),
                    new LetterButtonViewModel("Q", this.SetLocations),
                    new LetterButtonViewModel("R", this.SetLocations),
                    new LetterButtonViewModel("S", this.SetLocations),
                    new LetterButtonViewModel("T", this.SetLocations),
                    new LetterButtonViewModel("U", this.SetLocations),
                    new LetterButtonViewModel("V", this.SetLocations),
                    new LetterButtonViewModel("W", this.SetLocations),
                    new LetterButtonViewModel("X", this.SetLocations),
                    new LetterButtonViewModel("Y", this.SetLocations),
                    new LetterButtonViewModel("Z", this.SetLocations)
            };

            this.SaveCommand =
                new CommonCommand(
                    this.Save);
            this.CheckCommand =
                new CommonCommand(
                    this.Check);
        }

        /// <summary>
        /// Gets the set of locations to show on the view.
        /// </summary>
        public ObservableCollection<LocationConfiguratorViewModel> Locations => this.locationViewModels;

        /// <summary>
        /// Gets the collection of letter commands. Each command is used to find all the locations
        /// beginning with that letter.
        /// </summary>
        public ObservableCollection<LetterButtonViewModel> Buttons => this.letterButtonViewModels;

        /// <summary>
        /// Gets a command which is used to save the current setting.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        /// <summary>
        /// Gets a command which is used to check for new locations.
        /// </summary>
        public ICommand CheckCommand { get; private set; }

        /// <summary>
        /// Create a view model for each location in the set.
        /// </summary>
        /// <param name="letter">
        /// The set consists of all known locations starting which this letter.
        /// </param>
        private void SetLocations(string letter)
        {
            ObservableCollection<Location> locations = this.getLocations.Invoke(letter);

            this.locationViewModels = new ObservableCollection<LocationConfiguratorViewModel>();

            foreach (Location location in locations)
            {
                LocationConfiguratorViewModel locationViewModel =
                  new LocationConfiguratorViewModel(
                    location,
                    this.lines,
                    this.counties,
                    this.regions,
                    this.big4Regions);

                this.locationViewModels.Add(locationViewModel);
            }

            this.OnPropertyChanged(nameof(this.Locations));
        }

        /// <summary>
        /// Save the current location details.
        /// </summary>
        private void Save()
        {
            this.blManager.Save();
        }

        /// <summary>
        /// Check for new Locations and load them into the model.
        /// </summary>
        private void Check()
        {
            this.blManager.Check();
            this.Save();
            this.SetLocations("A");
        }
    }
}