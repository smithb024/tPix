namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System.Collections.ObjectModel;
    using tPix.BL;
    using tPix.Common.Enum;

    /// <summary>
    /// View model which supports the selection pane which is present on the main window.
    /// </summary>
    public class SelectionPaneViewModel : ViewModelBase
    {
        /// <summary>
        /// The instance of the <see cref="BLManager"/>.
        /// </summary>
        private readonly BLManager bLManager;

        /// <summary>
        /// The base path the app works from.
        /// </summary>
        private string basePath;

        /// <summary>
        /// The collection of all selectable classes.
        /// </summary>
        private ObservableCollection<string> classes;

        /// <summary>
        /// The collection of all selectable  numbers.
        /// </summary>
        private ObservableCollection<string> numbers;

        /// <summary>
        /// The currently selected class.
        /// </summary>
        private int classesIndex;

        /// <summary>
        /// The currently selected number.
        /// </summary>
        private int numbersIndex;

        /// <summary>
        /// The type of location which can be currently selected.
        /// </summary>
        private LocationType locationSelector;

        /// <summary>
        /// Gets or sets the base path which this application works from.
        /// </summary>
        public string BasePath
        {
            get => this.basePath;

            set => this.basePath = value;
        }

        /// <summary>
        /// Gets the collection of all selectable classes.
        /// </summary>
        public ObservableCollection<string> Classes => this.classes;

        /// <summary>
        /// Gets or sets the index of the selected class.
        /// </summary>
        public int ClassesIndex
        {
            get => this.classesIndex;

            set
            {
                if (this.classesIndex == value)
                {
                    return;
                }

                this.classesIndex = value;
                this.GetNmbs();
                this.GetImages();
                this.OnPropertyChanged(nameof(this.ClassesIndex));
            }
        }

        /// <summary>
        /// Gets or sets the collection of selectable numbers.
        /// </summary>
        public ObservableCollection<string> Numbers
        {
            get => this.numbers;

            set => this.SetProperty(ref this.numbers, value);
        }

        /// <summary>
        /// Gets or sets the index of the selected number.
        /// </summary>
        public int NumbersIndex
        {
            get => this.numbersIndex;

            set
            {
                if (this.numbersIndex == value)
                {
                    return;

                }
                this.numbersIndex = value;
                this.GetImages();
                this.OnPropertyChanged(nameof(this.NumbersIndex));
            }
        }

        /// <summary>
        /// Gets or sets the type of location being selected.
        /// </summary>
        public LocationType LocationSelector
        {
            get => this.locationSelector;

            set => this.SelectLocationSelector(value);
        }

        /// <summary>
        /// Gets the current location.
        /// </summary>
        public string CurrentLocation
        {
            get
            {
                if (this.LocationsIndex >= 0 && this.LocationsIndex < this.Locations.Count)
                {
                    return this.Locations[this.LocationsIndex];
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the current line.
        /// </summary>
        public string CurrentLine
        {
            get
            {
                if (this.LinesIndex >= 0 && this.LinesIndex < this.Lines.Count)
                {
                    return this.Lines[this.LinesIndex];
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the current county.
        /// </summary>
        public string CurrentCounty
        {
            get
            {
                if (this.CountiesIndex >= 0 && this.CountiesIndex < this.Lines.Count)
                {
                    return this.Counties[this.CountiesIndex];
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the current region.
        /// </summary>
        public string CurrentRegion
        {
            get
            {
                if (this.RegionsIndex >= 0 && this.RegionsIndex < this.Regions.Count)
                {
                    return this.Regions[this.RegionsIndex];
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets the current big region.
        /// </summary>
        public string CurrentBig4
        {
            get
            {
                if (this.Big4RegionsIndex >= 0 && this.Big4RegionsIndex < this.Big4Regions.Count)
                {
                    return this.Big4Regions[this.Big4RegionsIndex];
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Populate the numbers collection.
        /// </summary>
        private void GetNmbs()
        {
            if (this.ClassesIndex >= 0 && this.ClassesIndex < this.Classes.Count)
            {
                this.numbers = this.bLManager.GetNmbs(this.Classes[this.ClassesIndex]);
                this.numbers.Insert(0, string.Empty);
            }
            else
            {
                this.numbers = new ObservableCollection<string> { string.Empty };
            }

            this.numbersIndex = 0;

            this.OnPropertyChanged(nameof(this.NumbersIndex));
            this.OnPropertyChanged(nameof(this.Numbers));
        }

        /// <summary>
        /// Update the location type.
        /// </summary>
        /// <param name="newLocation">The new location type</param>
        /// <param name="forceTrue">Override flag</param>
        private void SelectLocationSelector(
          LocationType newLocation,
          bool forceTrue = false)
        {
            if (forceTrue)
            {
                if (this.LocationSelector != newLocation)
                {
                    this.LocationSelector = newLocation;
                }
            }
            else
            {
                this.locationSelector =
                  this.LocationSelector == newLocation ?
                  LocationType.None :
                  newLocation;
            }

            this.GetImages();
            this.OnPropertyChanged(nameof(this.LocationSelector));
        }
    }
}
