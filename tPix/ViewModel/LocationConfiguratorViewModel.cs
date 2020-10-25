namespace tPix.ViewModel
{
  using System;
  using System.Collections.ObjectModel;
  using NynaeveLib.ViewModel;
  using tPix.BL.Interfaces;

  public class LocationConfiguratorViewModel : ViewModelBase
  {
    private ILocation location;
    private ObservableCollection<string> lines;
    private ObservableCollection<string> counties;
    private ObservableCollection<string> regions;
    private ObservableCollection<string> big4Regions;
    Action<ILocation> saveLocation;

    public LocationConfiguratorViewModel(
      ILocation location,
      ObservableCollection<string> lines,
      ObservableCollection<string> counties,
      ObservableCollection<string> regions,
      ObservableCollection<string> big4Regions,
      Action<ILocation> saveLocation)
    {
      this.location = location;
      this.lines = lines;
      this.counties = counties;
      this.regions = regions;
      this.big4Regions = big4Regions;
      this.saveLocation = saveLocation;
    }

    public string LocationName => this.location.Name;

    public ObservableCollection<string> LineCollection => this.lines;
    public ObservableCollection<string> CountyCollection => this.counties;
    public ObservableCollection<string> RegionCollection => this.regions;
    public ObservableCollection<string> Big4Collection => this.big4Regions;

    public int LinesIndex
    {
      get
      {
        if (this.location.Line != null)
        {
          return (int)this.location.Line + 1;
        }

        return 0;
      }

      private set
      {
        if (value == 0)
        {
          this.location.Line = null;
        }
        else
        {
          this.location.Line = value - 1;
        }

        this.RaisePropertyChangedEvent(nameof(this.LinesIndex));
        this.Save();
      }
    }

    public int CountiesIndex
    {
      get
      {
        if (this.location.County != null)
        {
          return (int)this.location.County + 1;
        }

        return 0;
      }

      private set
      {
        if (value == 0)
        {
          this.location.County = null;
        }
        else
        {
          this.location.County = value - 1;
        }

        this.RaisePropertyChangedEvent(nameof(this.CountiesIndex));
        this.Save();
      }
    }

    public int RegionsIndex
    {
      get
      {
        if (this.location.Region != null)
        {
          return (int)this.location.Region + 1;
        }

        return 0;
      }

      private set
      {
        if (value == 0)
        {
          this.location.Region = null;
        }
        else
        {
          this.location.Region = value - 1;
        }

        this.RaisePropertyChangedEvent(nameof(this.RegionsIndex));
        this.Save();
      }
    }

    public int Big4RegionsIndex
    {
      get
      {
        if (this.location.Big4 != null)
        {
          return (int)this.location.Big4 + 1;
        }

        return 0;
      }

      private set
      {
        if (value == 0)
        {
          this.location.Big4 = null;
        }
        else
        {
          this.location.Big4 = value - 1;
        }

        this.RaisePropertyChangedEvent(nameof(this.Big4RegionsIndex));
        this.Save();
      }
    }

    private void Save()
    {
      this.saveLocation.Invoke(this.location);
    }
  }
}