namespace tPix.BL
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Common.Enum;
    using tPix.BL.Interfaces;
    using tPix.BL.Interfaces.ClsNmbConfig;
    using tPix.BL.Model.ClsNmbConfig;
    using tPix.Common;

    /// <summary>
    /// This is a central class which manages all business logic.
    /// </summary>
    public class BLManager
    {
        private Random randomGenerator;
        private Model.FaultManager faultManager;
        private Model.LocationManager locationManager;
        private IClsNmbManager clsNmbManager;

        /// <summary>
        /// Collection of all the known images.
        /// </summary>
        private List<IImageDetails> allImages;

        private string basePath;

        /// <summary>
        /// Initialises a new instance of the <see cref="BLManager"/> class.
        /// </summary>
        public BLManager()
        {
            this.basePath = "C:\\tp";
            this.randomGenerator = new Random();

            Factories.ClsNmbFactory clsNmbReader = new BL.Factories.ClsNmbFactory(this.basePath);
            Factories.ImageReader reader = new BL.Factories.ImageReader(this.basePath);

            this.faultManager = new BL.Model.FaultManager();

            this.locationManager =
                new BL.Model.LocationManager(
                    this.basePath,
                    this.faultManager);
            this.clsNmbManager =
                new ClsNmbManager(
                    clsNmbReader.ReadClsDetails(
                        this.faultManager));

            this.allImages =
                reader.ReadImages(
                    this.locationManager,
                    this.clsNmbManager,
                    this.faultManager);

            this.locationManager.Save();
        }

        public string BasePath => this.basePath;

        public ObservableCollection<string> GetClasses()
        {
            List<string> classes = new List<string>();

            foreach (IImageDetails image in this.allImages)
            {
                foreach (ICls cls in image.Clss)
                {
                    if (!classes.Contains(cls.Name))
                    {
                        classes.Add(cls.Name);
                    }
                }
            }

            classes.Sort();
            return this.Convert(classes);
        }

        public ObservableCollection<string> GetNmbs(string clsName)
        {
            List<string> nmbs = new List<string>();

            foreach (IImageDetails image in this.allImages)
            {
                foreach (ICls searchCls in image.Clss)
                {
                    if (string.Equals(searchCls.Name, clsName))
                    {
                        foreach (string nmb in searchCls.Nmbs)
                        {
                            if (!nmbs.Contains(nmb))
                            {
                                nmbs.Add(nmb);
                            }
                        }
                    }
                }
            }

            nmbs.Sort();
            return this.Convert(nmbs);
        }

        public ObservableCollection<string> GetLocations()
        {
            return this.Convert(this.locationManager.LocationsByName);
        }

        public ObservableCollection<ILocation> GetLocationsByLetter(string character)
        {
            return this.Convert(this.locationManager.GetLocationsByLetter(character));
        }

        public void SaveLocation(ILocation location)
        {
            this.locationManager.UpdateLocation(location);
        }

        public ObservableCollection<string> GetLines()
        {
            return this.Convert(this.locationManager.Lines);
        }

        public ObservableCollection<string> GetCounties()
        {
            return this.Convert(this.locationManager.Counties);
        }

        public ObservableCollection<string> GetRegions()
        {
            return this.Convert(this.locationManager.Regions);
        }

        public ObservableCollection<string> GetBig4Regions()
        {
            return this.Convert(this.locationManager.Big4Regions);
        }

        public ImageDescription GetImage()
        {
            int r = randomGenerator.Next(this.allImages.Count);
            return new ImageDescription(
              this.allImages[r].Path,
              this.allImages[r].Description);
        }

        public ObservableCollection<ImageDescription> GetImages(
          string clsName)
        {
            return this.Convert(
              this.GetImageDetails(
                clsName));
        }

        public ObservableCollection<ImageDescription> GetImages(
          string clsName,
          string nmb)
        {
            return this.Convert(
              this.GetImageDetails(
                clsName,
                nmb));
        }

        public ObservableCollection<ImageDescription> GetImages(
          LocationType locationType,
          string location)
        {
            List<IImageDetails> imageList =
              this.GetImageDetails(
                this.allImages,
                locationType,
                location);

            return this.Convert(imageList);
        }

        public ObservableCollection<ImageDescription> GetImages(
          string clsName,
          LocationType locationType,
          string location)
        {
            List<IImageDetails> imageList =
              this.GetImageDetails(
                clsName);

            imageList =
              this.GetImageDetails(
                imageList,
                locationType,
                location);

            return this.Convert(imageList);
        }

        public ObservableCollection<ImageDescription> GetImages(
          string clsName,
          string nmb,
          LocationType locationType,
          string location)
        {
            List<IImageDetails> imageList =
              this.GetImageDetails(
                clsName,
                nmb);

            imageList =
              this.GetImageDetails(
                imageList,
                locationType,
                location);

            return this.Convert(imageList);
        }

        /// <summary>
        /// Check the locations to see if there are any new ones. 
        /// </summary>
        public void Check()
        {
            if (this.allImages != null)
            {
                return;
            }

            bool locationAdded = false;

            foreach (IImageDetails imageDetails in this.allImages)
            {
                if (string.Compare(imageDetails.Location.Name, imageDetails.LocationLiteral) != 0)
                {
                    bool results =
                        this.locationManager.Check(
                            imageDetails);

                    if (results)
                    {
                        locationAdded = true;
                    }
                }
            }

            if (locationAdded)
            {
                this.locationManager.OrderLocations();
            }
        }

        private ObservableCollection<string> Convert(List<string> origCollection)
        {
            ObservableCollection<string> outputCollection = new ObservableCollection<string>();

            if (origCollection != null)
            {
                foreach (string item in origCollection)
                {
                    outputCollection.Add(item);
                }
            }

            return outputCollection;
        }

        private ObservableCollection<ImageDescription> Convert(List<IImageDetails> origCollection)
        {
            ObservableCollection<ImageDescription> outputCollection = new ObservableCollection<ImageDescription>();

            if (origCollection != null)
            {
                foreach (IImageDetails item in origCollection)
                {
                    outputCollection.Add(
                      new ImageDescription(
                        item.Path,
                        item.Description));
                }
            }

            return outputCollection;
        }

        private ObservableCollection<ILocation> Convert(List<ILocation> origCollection)
        {
            ObservableCollection<ILocation> outputCollection = new ObservableCollection<ILocation>();

            if (origCollection != null)
            {
                foreach (ILocation item in origCollection)
                {
                    outputCollection.Add(item);
                }
            }

            return outputCollection;
        }

        private int GetLineIndex(string line)
        {
            return this.locationManager.Lines.FindIndex(l => l == line);
        }

        private int GetCountyIndex(string county)
        {
            return this.locationManager.Counties.FindIndex(l => l == county);
        }

        private int GetRegionIndex(string region)
        {
            return this.locationManager.Regions.FindIndex(l => l == region);
        }

        private int GetBig4Index(string big4)
        {
            return this.locationManager.Big4Regions.FindIndex(l => l == big4);
        }

        private List<IImageDetails> GetImageDetails(
          string clsName)
        {
            return this.allImages.FindAll(i => i.ContainsCls(clsName));
        }

        private List<IImageDetails> GetImageDetails(
          string clsName,
          string nmb)
        {
            return this.allImages.FindAll(i => i.ContainsCls(clsName) && i.ContainsNmb(nmb));
        }

        private List<IImageDetails> GetImageDetails(
          List<IImageDetails> images,
          LocationType locationType,
          string location)
        {
            switch (locationType)
            {
                case LocationType.Location:
                    return images.FindAll(i => i.Stn.Name == location);
                case LocationType.Line:
                    return images.FindAll(
                        i => i.Stn.Line == this.GetLineIndex(location));
                case LocationType.County:
                    return images.FindAll(
                        i => i.Stn.County == this.GetCountyIndex(location));
                case LocationType.Region:
                    return images.FindAll(
                        i => i.Stn.Region == this.GetRegionIndex(location));
                case LocationType.Big4Location:
                    return images.FindAll(
                        i => i.Stn.Big4 == this.GetBig4Index(location));
                default:
                    return new List<IImageDetails>();
            }
        }
    }
}