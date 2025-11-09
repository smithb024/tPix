namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System;
    using System.Collections.ObjectModel;
    using System.Security.Cryptography;
    using tPix.BL.Interfaces;
    using tPix.BL.Model;

    /// <summary>
    /// View mode which supports a single line on the Locations Configuration view.
    /// </summary>
    public class LocationConfiguratorViewModel : ViewModelBase
    {
        private Location location;
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
          Location location,
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

            this.linesIndex = 
                this.FindIndex(
                    location.Line,
                    this.lines);
            this.countiesIndex =
                this.FindIndex(
                    location.County,
                    this.counties);
            this.regionsIndex =
                this.FindIndex(
                    location.Region,
                    this.regions);
            this.big4RegionsIndex =
                this.FindIndex(
                    location.Big4,
                    this.big4Regions);
        }

        /// <summary>
        /// Gets the name of the location.
        /// </summary>
        public string LocationName => this.location.Name;

        /// <summary>
        /// Gets the collection of all known lines.
        /// </summary>
        public ObservableCollection<string> LineCollection
        {
            get => this.lines;
        }

        /// <summary>
        /// Gets the collection of all known counties.
        /// </summary>
        public ObservableCollection<string> CountyCollection
        {
            get => this.counties;
        }

        /// <summary>
        /// Gets the collection of all known regions.
        /// </summary>
        public ObservableCollection<string> RegionCollection 
        {
            get => this.regions;
        }

        /// <summary>
        /// Gets the collection of all known big regions.
        /// </summary>
        public ObservableCollection<string> Big4Collection
        {
            get => this.big4Regions;
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
                this.location.Line = this.LineCollection[this.LinesIndex];
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
                this.location.County = this.CountyCollection[this.CountiesIndex];
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
                this.location.Region = this.RegionCollection[this.RegionsIndex];
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
                this.location.Big4 = this.Big4Collection[this.Big4RegionsIndex];
            }
        }

        /// <summary>
        /// Search the <paramref name="collection"/> for the value which equals <paramref name="searchValue"/>.
        /// Return the index of the value. If no value is found, return an index of 0.
        /// </summary>
        /// <param name="searchValue">The value to search for</param>
        /// <param name="collection">The collection to search</param>
        /// <returns>The found index.</returns>
        private int FindIndex(
            string searchValue,
            ObservableCollection<string> collection)
        {
            for (int i = 0; i < collection.Count; i++)
            {
                if (string.Equals(collection[i], searchValue, StringComparison.OrdinalIgnoreCase))
                {
                    return i;
                }
            }

            return 0;
        }
    }
}