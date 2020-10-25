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

      this.letterButtonViewModels = new ObservableCollection<LetterButtonViewModel>();
      this.letterButtonViewModels.Add(new LetterButtonViewModel("A", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("B", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("C", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("D", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("E", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("F", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("G", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("H", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("I", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("J", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("K", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("L", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("M", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("N", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("O", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("P", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("Q", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("R", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("S", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("T", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("U", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("V", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("W", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("X", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("Y", this.SetLocations));
      this.letterButtonViewModels.Add(new LetterButtonViewModel("Z", this.SetLocations));
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

      this.RaisePropertyChangedEvent(nameof(this.Locations));
    }
  }
}