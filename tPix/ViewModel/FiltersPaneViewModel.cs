namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System.Collections.ObjectModel;
    using tPix.BL;
    using tPix.Common.Enum;

    /// <summary>
    /// View model which supports the filters pane which is present on the main window.
    /// </summary>
    public class FiltersPaneViewModel : ViewModelBase
    {
        /// <summary>
        /// The instance of the <see cref="BLManager"/>.
        /// </summary>
        private readonly BLManager bLManager;

        /// <summary>
        /// The collection of all locations.
        /// </summary>
        private ObservableCollection<string> locations;

        /// <summary>
        /// The collection of all lines.
        /// </summary>
        private ObservableCollection<string> lines;

        /// <summary>
        /// The collection of all counties.
        /// </summary>
        private ObservableCollection<string> counties;

        /// <summary>
        /// The collection of all regions.
        /// </summary>
        private ObservableCollection<string> regions;

        /// <summary>
        /// The collection of all big 4 regions.
        /// </summary>
        private ObservableCollection<string> big4Regions;

        /// <summary>
        /// The index of the currently selected location.
        /// </summary>
        private int locationsIndex;

        /// <summary>
        /// The index of the currently selected line.
        /// </summary>
        private int linesIndex;

        /// <summary>
        /// The index of the currently selected county.
        /// </summary>
        private int countiesIndex;

        /// <summary>
        /// The index of the currently selected region.
        /// </summary>
        private int regionsIndex;

        /// <summary>
        /// The index of the currently selected big 4 region.
        /// </summary>
        private int big4RegionsIndex;


        /// <summary>
        /// Initialises a new instance of the <see cref="FiltersPaneViewModel"/> class.
        /// </summary>
        /// <param name="bLManager">The instance of the <see cref="BLManager"/></param>
        public FiltersPaneViewModel(
            BLManager bLManager) 
        {
            this.bLManager = bLManager;
        }

        /// <summary>
        /// Gets the collection of all locations.
        /// </summary>
        public ObservableCollection<string> Locations => this.locations;

        /// <summary>
        /// Gets the collections of all lines.
        /// </summary>
        public ObservableCollection<string> Lines => this.lines;

        /// <summary>
        /// Gets the collections of all counties.
        /// </summary>
        public ObservableCollection<string> Counties => this.counties;

        /// <summary>
        /// Gets the collection of all regions.
        /// </summary>
        public ObservableCollection<string> Regions => this.regions;

        /// <summary>
        /// Gets the collection of all big 4 regions.
        /// </summary>
        public ObservableCollection<string> Big4Regions => this.big4Regions;

        /// <summary>
        /// Gets or sets the index of the currently selected location.
        /// </summary>
        public int LocationsIndex
        {
            get => this.locationsIndex;

            set
            {
                if (this.locationsIndex == value)
                {
                    return;
                }

                this.locationsIndex = value;
                this.SelectLocationSelector(LocationType.Location, true);
                this.OnPropertyChanged(nameof(this.LocationsIndex));
                this.OnPropertyChanged(nameof(this.CurrentLocation));
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected line.
        /// </summary>
        public int LinesIndex
        {
            get
            {
                return this.linesIndex;
            }

            set
            {
                if (this.linesIndex == value)
                {
                    return;
                }

                this.linesIndex = value;
                this.SelectLocationSelector(LocationType.Line, true);
                this.OnPropertyChanged(nameof(this.LinesIndex));
                this.OnPropertyChanged(nameof(this.CurrentLine));
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
                this.SelectLocationSelector(LocationType.County, true);
                this.OnPropertyChanged(nameof(this.CountiesIndex));
                this.OnPropertyChanged(nameof(this.CurrentCounty));
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
                this.SelectLocationSelector(LocationType.Region, true);
                this.OnPropertyChanged(nameof(this.RegionsIndex));
                this.OnPropertyChanged(nameof(this.CurrentRegion));
            }
        }

        /// <summary>
        /// Gets or sets the index of the currently selected big region,
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
                this.SelectLocationSelector(LocationType.Big4Location, true);
                this.OnPropertyChanged(nameof(this.Big4RegionsIndex));
                this.OnPropertyChanged(nameof(this.CurrentBig4));
            }
        }
    }
}
