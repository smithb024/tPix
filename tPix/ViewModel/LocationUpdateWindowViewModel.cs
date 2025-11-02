namespace tPix.ViewModel
{
    using NynaeveLib.Commands;
    using NynaeveLib.ViewModel;
    using System;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using tPix.BL.Interfaces;
    using tPix.BL.Model;
    using tPix.ViewModel.Cmd;

    /// <summary>
    /// View model which supports the Location Configuration dialog.
    /// </summary>
    public class LocationUpdateWindowViewModel : ViewModelBase
    {
        private Func<string, ObservableCollection<ILocation>> getLocations;

        ObservableCollection<LocationConfiguratorViewModel> locationViewModels;
        ObservableCollection<LetterButtonViewModel> letterButtonViewModels;

        private ObservableCollection<string> lines;
        private ObservableCollection<string> counties;
        private ObservableCollection<string> regions;
        private ObservableCollection<string> big4Regions;
        private Action<ILocation> saveLocation;

        /// <summary>
        /// Initialises a new instance of the <see cref="LocationUpdateWindowViewModel"/> class.
        /// </summary>
        /// <param name="getLocations"></param>
        /// <param name="saveLocation"></param>
        /// <param name="lines"></param>
        /// <param name="counties"></param>
        /// <param name="regions"></param>
        /// <param name="big4Regions"></param>
        public LocationUpdateWindowViewModel(
          Func<string, ObservableCollection<ILocation>> getLocations,
          Action<ILocation> saveLocation,
          ObservableCollection<string> lines,
          ObservableCollection<string> counties,
          ObservableCollection<string> regions,
          ObservableCollection<string> big4Regions)
        {
            this.getLocations = getLocations;
            this.lines = lines;
            this.counties = counties;
            this.regions = regions;
            this.big4Regions = big4Regions;
            this.saveLocation = saveLocation;

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
        }

        public ObservableCollection<LocationConfiguratorViewModel> Locations => this.locationViewModels;
        public ObservableCollection<LetterButtonViewModel> Buttons => this.letterButtonViewModels;

        /// <summary>
        /// Gets a command which is used to save the current setting.
        /// </summary>
        public ICommand SaveCommand { get; private set; }

        private void SetLocations(string letter)
        {
            ObservableCollection<ILocation> locations = this.getLocations.Invoke(letter);

            this.locationViewModels = new ObservableCollection<LocationConfiguratorViewModel>();

            foreach (ILocation location in locations)
            {
                LocationConfiguratorViewModel locationViewModel =
                  new LocationConfiguratorViewModel(
                    location,
                    lines,
                    counties,
                    regions,
                    big4Regions,
                    this.saveLocation);

                this.locationViewModels.Add(locationViewModel);
            }

            this.OnPropertyChanged(nameof(this.Locations));
        }

        /// <summary>
        /// Save the current location details.
        /// </summary>
        private void Save()
        {
            ILocation test =
                new Location(
                    "TestLocation",
                    3,
                    6,
                    null,
                    7);
            ILocation test2 =
                new Location(
                    "Two",
                    1,
                    2,
                    6,
                    7);
            ILocation test3 =
                new Location(
                    "Three",
                    null,
                    3,
                    9,
                    33);
            ILocationCollection locations = new LocationCollection();
            locations.Locations.Add(test);
            locations.Locations.Add(test2);
            locations.Locations.Add(test3);

            NynaeveLib.Json.JsonFileIo.WriteJson(locations, "C:\\tp\\test.json");

            LocationCollection read;

            try
            {
                read = NynaeveLib.Json.JsonFileIo.ReadJson<LocationCollection>("C:\\tp\\test.json");
            }
            catch (Exception ex) 
            {
                int i = 0;
                ++i;
            }

            int j = 2;
            ++j;
        }
    }
}