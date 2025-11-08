namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System;
    using System.Collections.ObjectModel;
    using tPix.BL.Interfaces;

    /// <summary>
    /// View mode which supports a single line on the Locations Configuration view.
    /// </summary>
    public class LocationConfiguratorViewModel : ViewModelBase
    {
        private ILocation location;
        private ObservableCollection<string> lines;
        private ObservableCollection<string> counties;
        private ObservableCollection<string> regions;
        private ObservableCollection<string> big4Regions;

        /// <summary>
        /// The index of the currently selected line;
        /// </summary>
        private int linesIndex;

        /// <summary>
        /// The index of the currently selected county;
        /// </summary>
        private int countiesIndex;

        /// <summary>
        /// The index of the currently selected region;
        /// </summary>
        private int regionsIndex;

        /// <summary>
        /// The index of the currently selected big region;
        /// </summary>
        private int big4RegionsIndex;

        /// <summary>
        /// Initialises a new instance of the <see cref="LocationConfiguratorViewModel"/> class.
        /// </summary>
        /// <param name="location">The model object of the assocated location.</param>
        /// <param name="lines">Collection of known lines.</param>
        /// <param name="counties">Collection of known counties.</param>
        /// <param name="regions">Collection of known regions.</param>
        /// <param name="big4Regions">Collection of known big regions.</param>
        public LocationConfiguratorViewModel(
          ILocation location,
          ObservableCollection<string> lines,
          ObservableCollection<string> counties,
          ObservableCollection<string> regions,
          ObservableCollection<string> big4Regions)
        {
            this.location = location;
            this.lines = lines;
            this.counties = counties;
            this.regions = regions;
            this.big4Regions = big4Regions;

            this.linesIndex = 0;
            this.countiesIndex = 0;
            this.regionsIndex = 0;
            this.big4RegionsIndex = 0;
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

        /// <summary>
        /// Gets or sets the index of the currently selected line.
        /// </summary>
        public int LinesIndex
        {
            get => this.linesIndex;

            set
            {
                if (this.linesIndex == value) 
                {
                    return;
                }

                this.linesIndex = value;
                this.OnPropertyChanged(nameof(this.LinesIndex));
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected county.
        /// </summary>
        public int CountiesIndex
        {
            get => this.countiesIndex;

            set
            {
                if (this.countiesIndex == value)
                {
                    return;
                }

                this.countiesIndex = value;
                this.OnPropertyChanged(nameof(this.CountiesIndex));
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected region.
        /// </summary>
        public int RegionsIndex
        {
            get => this.regionsIndex;

            set
            {
                if (this.regionsIndex == value)
                {
                    return;
                }

                this.regionsIndex = value;
                this.OnPropertyChanged(nameof(this.RegionsIndex));
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected big region.
        /// </summary>
        public int Big4RegionsIndex
        {
            get => this.big4RegionsIndex;

            set
            {
                if (this.big4RegionsIndex == value)
                {
                    return;
                }

                this.big4RegionsIndex = value;
                this.OnPropertyChanged(nameof(this.Big4RegionsIndex));
            }
        }
    }
}