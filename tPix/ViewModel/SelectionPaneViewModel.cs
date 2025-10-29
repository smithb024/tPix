namespace tPix.ViewModel
{
    using CommunityToolkit.Mvvm.Messaging;
    using NynaeveLib.ViewModel;
    using System.Collections.ObjectModel;
    using tPix.BL;
    using tPix.Common.Enum;
    using tPix.Common.Messages;

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
        /// The current location filter.
        /// </summary>
        private string currentLocation;

        /// <summary>
        /// The current line filter.
        /// </summary>
        private string currentLine;

        /// <summary>
        /// The current county filter.
        /// </summary>
        private string currentCounty;

        /// <summary>
        /// The current region filter.
        /// </summary>
        private string currentRegion;

        /// <summary>
        /// The current big region filter.
        /// </summary>
        private string currentBig4Region;

        /// <summary>
        /// Indicates whether to use the location filter.
        /// </summary>
        private bool currentLocationChecked;

        /// <summary>
        /// Indicates whether to use the line filter.
        /// </summary>
        private bool currentLineChecked;

        /// <summary>
        /// Indicates whether to use the county filter.
        /// </summary>
        private bool currentCountyChecked;

        /// <summary>
        /// Indicates whether to use the region filter.
        /// </summary>
        private bool currentRegionChecked;

        /// <summary>
        /// Indicates whether to use the big region filter.
        /// </summary>
        private bool currentBig4RegionChecked;

        /// <summary>
        /// Initialises a new instance of the <see cref="SelectionPaneViewModel"/> class.
        /// </summary>
        /// <param name="bLManager">The BL Manager</param>
        private SelectionPaneViewModel(BLManager bLManager)
        {
            this.bLManager = bLManager;

            this.currentLocation = string.Empty; 
            this.currentLine = string.Empty;
            this.currentCounty = string.Empty;
            this.currentRegion = string.Empty;
            this.currentBig4Region = string.Empty;

            this.Messenger.Register<NewFiltersMessage>(
                this,
                (r, message) => this.FilterMessage(message));
        }

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
                this.OnPropertyChanged(nameof(this.ClassesIndex));
                this.SendGenerateImageRequest();
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
                this.OnPropertyChanged(nameof(this.NumbersIndex));
                this.SendGenerateImageRequest();
            }
        }

        /// <summary>
        /// Gets or sets the current location.
        /// </summary>
        public string CurrentLocation
        {
            get => this.currentLocation;
            set => this.SetProperty(ref this.currentLocation, value);
        }

        /// <summary>
        /// Gets or sets the current line.
        /// </summary>
        public string CurrentLine
        {
            get => this.currentLine;
            set => this.SetProperty(ref this.currentLine, value);
        }

        /// <summary>
        /// Gets or sets the current county.
        /// </summary>
        public string CurrentCounty
        {
            get => this.currentCounty;
            set => this.SetProperty(ref this.currentCounty, value);
        }

        /// <summary>
        /// Gets or sets the current region.
        /// </summary>
        public string CurrentRegion
        {
            get => this.currentRegion;
            set => this.SetProperty(ref this.currentRegion, value);
        }

        /// <summary>
        /// Gets or sets the current big region.
        /// </summary>
        public string CurrentBig4
        {
            get => this.currentBig4Region;
            set => this.SetProperty(ref this.currentBig4Region, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the current location filter.
        /// </summary>
        public bool CurrentLocationChecked
        {
            get => this.currentLocationChecked;
            set
            {
                if (this.currentLocationChecked == value)
                {
                    return;
                }

                this.currentLocationChecked = value;
                this.currentLineChecked = false;
                this.currentCountyChecked = false;
                this.currentRegionChecked = false;
                this.currentBig4RegionChecked = false;
                this.RefreshCheckboxes();
                this.SendGenerateImageRequest();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the current line filter.
        /// </summary>
        public bool CurrentLineChecked
        {
            get => this.currentLineChecked;
            set
            {
                if (this.currentLineChecked == value)
                {
                    return;
                }

                this.currentLocationChecked = false;
                this.currentLineChecked = value;
                this.currentCountyChecked = false;
                this.currentRegionChecked = false;
                this.currentBig4RegionChecked = false;
                this.RefreshCheckboxes();
                this.SendGenerateImageRequest();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the current county filter.
        /// </summary>
        public bool CurrentCountyChecked
        {
            get => this.currentCountyChecked;
            set
            {
                if (this.currentCountyChecked == value)
                {
                    return;
                }

                this.currentLocationChecked = false;
                this.currentLineChecked = false;
                this.currentCountyChecked = value;
                this.currentRegionChecked = false;
                this.currentBig4RegionChecked = false;
                this.RefreshCheckboxes();
                this.SendGenerateImageRequest();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the current region filter.
        /// </summary>
        public bool CurrentRegionChecked
        {
            get => this.currentRegionChecked;
            set
            {
                if (this.currentRegionChecked == value)
                {
                    return;
                }

                this.currentLocationChecked = false;
                this.currentLineChecked = false;
                this.currentCountyChecked = false;
                this.currentRegionChecked = value;
                this.currentBig4RegionChecked = false;
                this.RefreshCheckboxes();
                this.SendGenerateImageRequest();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use the current big region filter.
        /// </summary>
        public bool CurrentBig4RegionChecked
        {
            get => this.currentBig4RegionChecked;
            set
            {
                if (this.currentBig4RegionChecked == value)
                {
                    return;
                }

                this.currentLocationChecked = false;
                this.currentLineChecked = false;
                this.currentCountyChecked = false;
                this.currentRegionChecked = false;
                this.currentBig4RegionChecked = value;
                this.RefreshCheckboxes();
                this.SendGenerateImageRequest();
            }
        }

        /// <summary>
        /// Populate the numbers collection.
        /// </summary>
        private void GetNmbs()
        {
            if (this.IsClassValid())
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
        /// A new <see cref="NewFiltersMessage"/> has been received.
        /// </summary>
        /// <param name="message">The <see cref="NewFiltersMessage"/> message</param>
        private void FilterMessage(NewFiltersMessage message)
        {
            this.CurrentLocation = message.Location;
            this.CurrentLine = message.Line;
            this.CurrentCounty = message.County;
            this.CurrentRegion = message.Region;
            this.CurrentBig4 = message.Big4Region;
        }

        /// <summary>
        /// Send a Generate Image Message on the messenger.
        /// </summary>
        private void SendGenerateImageRequest()
        {
            string className = 
                this.IsClassValid()
                ? this.Classes[this.ClassesIndex]
                : string.Empty;
            string number =
                this.IsNmbValid()
                ? this.Numbers[this.NumbersIndex]
                : string.Empty;
            bool useFilter =
                this.CurrentLocationChecked ||
                this.CurrentLineChecked ||
                this.CurrentCountyChecked ||
                this.CurrentRegionChecked ||
                this.CurrentBig4RegionChecked;

            GenerateImageListMessage message =
                new GenerateImageListMessage(
                    className,
                    number,
                    useFilter,
                    this.GetLocationType());

            this.Messenger.Send(message);
        }

        /// <summary>
        /// Indicates whether there is a valid class selected.
        /// </summary>
        /// <returns>Validity flag</returns>
        private bool IsClassValid()
        {
            return this.ClassesIndex >= 0 && this.ClassesIndex < this.Classes.Count;
        }

        /// <summary>
        /// Indicates whether there is a valid number selected.
        /// </summary>
        /// <returns>Validity flag</returns>
        private bool IsNmbValid()
        {
            return this.NumbersIndex >= 0 && this.NumbersIndex < this.Numbers.Count;
        }

        /// <summary>
        /// Raise property changed events for the check boxes.
        /// </summary>
        private void RefreshCheckboxes()
        {
            this.OnPropertyChanged(nameof(this.CurrentLocationChecked));
            this.OnPropertyChanged(nameof(this.CurrentLineChecked));
            this.OnPropertyChanged(nameof(this.CurrentCountyChecked));
            this.OnPropertyChanged(nameof(this.CurrentRegionChecked));
            this.OnPropertyChanged(nameof(this.CurrentBig4RegionChecked));
        }

        /// <summary>
        /// Determine the <see cref="LocationType"/> which is currently selected.
        /// </summary>
        /// <returns>The selected <see cref="LocationType"/></returns>
        private LocationType GetLocationType()
        {
            if (this.CurrentLocationChecked)
            {
                return LocationType.Location;
            }

            if (this.CurrentLineChecked)
            {
                return LocationType.Line;
            }

            if (this.CurrentCountyChecked)
            {
                return LocationType.County;
            }

            if (this.CurrentRegionChecked)
            {
                return LocationType.Region;
            }

            if (!this.CurrentBig4RegionChecked)
            {
                return LocationType.Big4Location;
            }

            return LocationType.None;
        }
    }
}
