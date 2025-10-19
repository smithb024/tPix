namespace tPix.ViewModel
{
    using System;
    using System.Collections.ObjectModel;
    using NynaeveLib.ViewModel;
    using tPix.BL.Interfaces;

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
        }

        public ObservableCollection<LocationConfiguratorViewModel> Locations => this.locationViewModels;
        public ObservableCollection<LetterButtonViewModel> Buttons => this.letterButtonViewModels;

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
    }
}