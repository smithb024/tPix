namespace tPix.ViewModel
{
    using CommunityToolkit.Mvvm.Messaging;
    using NynaeveLib.ViewModel;
    using System.Collections.ObjectModel;
    using tPix.BL;
    using tPix.Common.Enum;
    using tPix.Common.Messages;

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
                this.OnPropertyChanged(nameof(this.LocationsIndex));
                this.SendMessage(LocationType.Location);
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
                this.OnPropertyChanged(nameof(this.LinesIndex));
                this.SendMessage(LocationType.Line);
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
                this.SendMessage(LocationType.County);
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
                this.SendMessage(LocationType.Region);
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

                this.OnPropertyChanged(nameof(this.Big4RegionsIndex));
                this.SendMessage(LocationType.Big4Location);
            }
        }

        private void SendMessage(
            LocationType type)
        {
            string location =
                this.LocationsIndex >= 0 && this.LocationsIndex < this.Locations.Count
                ? this.Locations[this.LocationsIndex]
                : string.Empty;

            string line =
                this.LinesIndex >= 0 && this.LinesIndex < this.Lines.Count
                ? this.Lines[this.LinesIndex]
                : string.Empty;

            string county =
                this.CountiesIndex >= 0 && this.CountiesIndex < this.Counties.Count
                ? this.Counties[this.CountiesIndex]
                : string.Empty;

            string region =
                this.RegionsIndex >= 0 && this.RegionsIndex < this.Regions.Count
                ? this.Regions[this.RegionsIndex]
                : string.Empty;

            string big4Region =
                this.Big4RegionsIndex >= 0 && this.Big4RegionsIndex < this.Big4Regions.Count
                ? this.Big4Regions[this.Big4RegionsIndex]
                : string.Empty;

            NewFiltersMessage message =
                new NewFiltersMessage(
                    type,
                    location,
                    line,
                    county,
                    region,
                    big4Region);

            this.Messenger.Send(message);
        }
    }
}
