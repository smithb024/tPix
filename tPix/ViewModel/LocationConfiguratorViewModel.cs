namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System;
    using System.Collections.ObjectModel;
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

        public ObservableCollection<string> LineCollection
        {
            get => this.lines;
            set => this.SetProperty(ref this.lines, value);
        }

        public ObservableCollection<string> CountyCollection
        {
            get => this.counties;
            set => this.SetProperty(ref this.counties, value);
        }

        public ObservableCollection<string> RegionCollection 
        {
            get => this.regions;
            set => this.SetProperty(ref this.regions, value);
        }

        public ObservableCollection<string> Big4Collection
        {
            get => this.big4Regions;
            set => this.SetProperty(ref this.big4Regions, value);
        }

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

            set
            {
                if (value == 0)
                {
                    this.location.Line = null;
                }
                else
                {
                    this.location.Line = value - 1;
                }

                this.OnPropertyChanged(nameof(this.LinesIndex));
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

            set
            {
                if (value == 0)
                {
                    this.location.County = null;
                }
                else
                {
                    this.location.County = value - 1;
                }

                this.OnPropertyChanged(nameof(this.CountiesIndex));
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

            set
            {
                if (value == 0)
                {
                    this.location.Region = null;
                }
                else
                {
                    this.location.Region = value - 1;
                }

                this.OnPropertyChanged(nameof(this.RegionsIndex));
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

            set
            {
                if (value == 0)
                {
                    this.location.Big4 = null;
                }
                else
                {
                    this.location.Big4 = value - 1;
                }

                this.OnPropertyChanged(nameof(this.Big4RegionsIndex));
                this.Save();
            }
        }

        private void Save()
        {
            this.saveLocation.Invoke(this.location);
        }
    }
}