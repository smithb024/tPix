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
        /// The type of location which can be currently selected.
        /// </summary>
        private LocationType locationSelector;

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
            this.SelectLocationSelector(message.NewType, true);

        }
    }
}
