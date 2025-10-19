namespace tPix.ViewModel
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Enum;
    using NynaeveLib.ViewModel;
    using NynaeveLib.DialogService;
    using tPix.BL;
    using tPix.Common;
    using ViewModel.Cmd;

    public class MainWindowViewModel : ViewModelBase
    {
        BLManager BLL;

        private string basePath;

        private ObservableCollection<ImageDescription> images;
        private ObservableCollection<string> classes;
        private ObservableCollection<string> numbers;
        private ObservableCollection<string> locations;
        private ObservableCollection<string> lines;
        private ObservableCollection<string> counties;
        private ObservableCollection<string> regions;
        private ObservableCollection<string> big4Regions;

        private int imagesIndex;
        private int classesIndex;
        private int numbersIndex;
        private int locationsIndex;
        private int linesIndex;
        private int countiesIndex;
        private int regionsIndex;
        private int big4RegionsIndex;

        private LocationType locationSelector;

        public MainWindowViewModel()
        {
            this.BLL = new BLManager();

            this.basePath = this.BLL.BasePath;
            this.locationSelector = LocationType.None;

            this.ConfigLocationsCommand = new UpdateLocationCmd(this, ShowConfigLocationsWindow);

            this.classes = this.BLL.GetClasses();
            this.locations = this.BLL.GetLocations();
            this.lines = this.BLL.GetLines();
            this.counties = this.BLL.GetCounties();
            this.regions = this.BLL.GetRegions();
            this.big4Regions = this.BLL.GetBig4Regions();

            this.classes.Insert(0, string.Empty);
            this.locations.Insert(0, string.Empty);
            this.lines.Insert(0, string.Empty);
            this.counties.Insert(0, string.Empty);
            this.regions.Insert(0, string.Empty);
            this.big4Regions.Insert(0, string.Empty);

            this.images =
              new ObservableCollection<ImageDescription>
              {
          this.BLL.GetImage()
              };
            //string randomImage = this.BLL.GetImage();
            ObservableCollection<ImageDescription> cls83Path = this.BLL.GetImages("83");
            ObservableCollection<ImageDescription> cls85Path = this.BLL.GetImages("85");
            ObservableCollection<ImageDescription> cls308Path = this.BLL.GetImages("308");
            ObservableCollection<ImageDescription> cls309Path = this.BLL.GetImages("309");
            ObservableCollection<ImageDescription> cls308149Path = this.BLL.GetImages("308", "149");
            ObservableCollection<ImageDescription> ColchesterPath = this.BLL.GetImages(LocationType.Location, "Colchester");
            ObservableCollection<ImageDescription> RubbishLocationPath = this.BLL.GetImages(LocationType.Location, "Rubbish");
            ObservableCollection<ImageDescription> FlydePath = this.BLL.GetImages(LocationType.Line, "Fylde");
            ObservableCollection<ImageDescription> RubbishLinePath = this.BLL.GetImages(LocationType.Line, "Rubbish");
            ObservableCollection<ImageDescription> EssexPath = this.BLL.GetImages(LocationType.County, "Essex");
            ObservableCollection<ImageDescription> AngliaPath = this.BLL.GetImages(LocationType.Region, "Anglia");
            ObservableCollection<ImageDescription> LnerPath = this.BLL.GetImages(LocationType.Big4Location, "LNER");

            ObservableCollection<ImageDescription> c308ColchesterPath = this.BLL.GetImages("308", LocationType.Location, "Colchester");
            ObservableCollection<ImageDescription> c308FlydePath = this.BLL.GetImages("308", LocationType.Line, "Fylde");
            ObservableCollection<ImageDescription> c308EssexPath = this.BLL.GetImages("308", LocationType.County, "Essex");
            ObservableCollection<ImageDescription> c308AngliaPath = this.BLL.GetImages("308", LocationType.Region, "Anglia");
            ObservableCollection<ImageDescription> c308LnerPath = this.BLL.GetImages("308", LocationType.Big4Location, "LNER");
            ObservableCollection<ImageDescription> c308993ColchesterPath = this.BLL.GetImages("308", "993", LocationType.Location, "Colchester");
            ObservableCollection<ImageDescription> c308993FlydePath = this.BLL.GetImages("308", "993", LocationType.Line, "Fylde");
            ObservableCollection<ImageDescription> c308993EssexPath = this.BLL.GetImages("308", "993", LocationType.County, "Essex");
            ObservableCollection<ImageDescription> c308993AngliaPath = this.BLL.GetImages("308", "993", LocationType.Region, "Anglia");
            ObservableCollection<ImageDescription> c308993LnerPath = this.BLL.GetImages("308", "993", LocationType.Big4Location, "LNER");
        }

        public string BasePath
        {
            get => this.basePath;

            set => this.basePath = value;
        }

        public ObservableCollection<ImageDescription> Images
        {
            get => this.images;

            set => this.SetProperty(ref this.images, value);
        }

        public ObservableCollection<string> Numbers
        {
            get => this.numbers;

            set => this.SetProperty(ref this.numbers, value);
        }

        public ObservableCollection<string> Classes => this.classes;
        public ObservableCollection<string> Locations => this.locations;
        public ObservableCollection<string> Lines => this.lines;
        public ObservableCollection<string> Counties => this.counties;
        public ObservableCollection<string> Regions => this.regions;
        public ObservableCollection<string> Big4Regions => this.big4Regions;

        public int ImagesIndex
        {
            get
            {
                return this.imagesIndex;
            }

            set
            {
                if (this.imagesIndex == value)
                {
                    return;
                }

                this.imagesIndex = value;
                this.OnPropertyChanged(nameof(this.ImagesIndex));
                this.OnPropertyChanged(nameof(this.ImagePath));
                this.OnPropertyChanged(nameof(this.ImageDescription));
            }
        }

        public string ImagePath =>
          this.ImagesIndex >= 0 && this.ImagesIndex < this.Images.Count ?
          this.Images[this.ImagesIndex].Path :
          string.Empty;

        public string ImageDescription =>
          this.ImagesIndex >= 0 && this.ImagesIndex < this.Images.Count ?
          this.Images[this.ImagesIndex].Description :
          string.Empty;

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

        public LocationType LocationSelector
        {
            get => this.locationSelector;

            set => this.SelectLocationSelector(value);
        }

        public string CurrentLocationSelector
        {
            get
            {
                switch (this.LocationSelector)
                {
                    case LocationType.Big4Location:
                        return (this.Big4RegionsIndex >= 0 && this.Big4RegionsIndex < this.Big4Regions.Count) ?
                          this.Big4Regions[this.Big4RegionsIndex] :
                          string.Empty;
                    case LocationType.County:
                        return (this.CountiesIndex >= 0 && this.CountiesIndex < this.Counties.Count) ?
                          this.Counties[this.CountiesIndex] :
                          string.Empty;
                    case LocationType.Line:
                        return (this.LinesIndex >= 0 && this.LinesIndex < this.Lines.Count) ?
                          this.Lines[this.LinesIndex] :
                          string.Empty;
                    case LocationType.Location:
                        return (this.LocationsIndex >= 0 && this.LocationsIndex < this.Locations.Count) ?
                          this.Locations[this.LocationsIndex] :
                          string.Empty;
                    case LocationType.Region:
                        return (this.RegionsIndex >= 0 && this.RegionsIndex < this.Regions.Count) ?
                          this.Regions[this.RegionsIndex] :
                          string.Empty;
                    default:
                        return string.Empty;
                }
            }
        }

        public ICommand ConfigLocationsCommand { get; private set; }

        private void GetNmbs()
        {
            if (this.ClassesIndex >= 0 && this.ClassesIndex < this.Classes.Count)
            {
                this.numbers = this.BLL.GetNmbs(this.Classes[this.ClassesIndex]);
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

        private void GetImages()
        {
            if (this.ClassesIndex < 1)
            {
                this.images =
                  this.BLL.GetImages(
                    this.LocationSelector,
                    this.CurrentLocationSelector);
            }
            else
            {
                string clsName =
                  this.ClassesIndex >= 0 && this.ClassesIndex < this.Classes.Count ?
                      this.Classes[this.ClassesIndex] :
                      string.Empty;

                if (this.NumbersIndex < 1)
                {
                    if (this.LocationSelector == LocationType.None)
                    {
                        this.images =
                          this.BLL.GetImages(
                            clsName);
                    }
                    else
                    {
                        this.images =
                          this.BLL.GetImages(
                            clsName,
                            this.LocationSelector,
                            this.CurrentLocationSelector);
                    }
                }
                else
                {
                    string nmb =
                      this.NumbersIndex >= 0 && this.NumbersIndex < this.Numbers.Count ?
                          this.Numbers[this.NumbersIndex] :
                          string.Empty;

                    if (this.LocationSelector == LocationType.None)
                    {
                        this.images =
                          this.BLL.GetImages(
                            clsName,
                            nmb);
                    }
                    else
                    {
                        this.images =
                          this.BLL.GetImages(
                            clsName,
                            nmb,
                            this.LocationSelector,
                            this.CurrentLocationSelector);
                    }
                }
            }

            if (this.Images == null || this.Images.Count == 0)
            {
                this.images =
                new ObservableCollection<ImageDescription>
                {
                    this.BLL.GetImage()
                };
            }


            this.OnPropertyChanged(nameof(this.Images));

            this.imagesIndex = 0;

            this.OnPropertyChanged(nameof(this.ImagesIndex));
            this.OnPropertyChanged(nameof(this.ImagePath));
            this.OnPropertyChanged(nameof(this.ImageDescription));
        }

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

        private void ShowConfigLocationsWindow()
        {
            //if (this.editMileageWindow == null)
            //{
            //  SetupWindow(
            //    this.editMileageWindow = new EditMileageWindow(),
            //    new EditMileageViewModel(),
            //    this.CloseEditJnyDetailsWindow,
            //    this.EditJnyDetailsWindowClosed);
            //}

            //this.editMileageWindow.Focus();
            LocationUpdateWindowViewModel locationUpdateViewModel =
              new LocationUpdateWindowViewModel(
                this.BLL.GetLocationsByLetter,
                this.BLL.SaveLocation,
                this.Lines,
                this.Counties,
                this.Regions,
                this.Big4Regions);

            DialogService service = new DialogService();

            service.ShowDialog(
              new LocationUpdateWindow()
              {
                  DataContext = locationUpdateViewModel
              });
        }
    }
}