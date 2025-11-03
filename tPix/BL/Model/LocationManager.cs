namespace tPix.BL.Model
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Interfaces;
    using Interfaces.Factories;
    using Factories;

    /// <summary>
    /// A class which manages the model locations.
    /// </summary>
    public class LocationManager : ILocationManager
    {
        /// <summary>
        /// Base path for the collection of images.
        /// </summary>
        private readonly string locationBasePath;

        /// <summary>
        /// The instance of the location factory utility.
        /// </summary>
        private readonly ILocationFactory locationFactory;

        /// <summary>
        /// Indicates whether the model needs to be saved.
        /// </summary>
        private bool needsSaving;

        /// <summary>
        /// Initialises a new instance of the <see cref="LocationManager"/> class.
        /// </summary>
        /// <param name="path">The base path.</param>
        /// <param name="faultManager">The instance of the <see cref="FaultManager"/></param>
        public LocationManager(
          string path,
          FaultManager faultManager)
        {
            this.needsSaving = false;
            this.locationFactory = new LocationFactory();
            this.Locations = new LocationCollection();

            this.locationBasePath = path + Path.DirectorySeparatorChar + "Locations";

            this.Big4Regions =
              this.ReadListFileContents(
                this.locationBasePath + Path.DirectorySeparatorChar + "Big4.txt");
            this.Counties =
              this.ReadListFileContents(
                this.locationBasePath + Path.DirectorySeparatorChar + "County.txt");
            this.Lines =
              this.ReadListFileContents(
                this.locationBasePath + Path.DirectorySeparatorChar + "Line.txt");
            this.Regions =
              this.ReadListFileContents(
                this.locationBasePath + Path.DirectorySeparatorChar + "Region.txt");

            this.Locations = this.locationFactory.ReadLocations(
              this.locationBasePath + Path.DirectorySeparatorChar + "Location.txt",
              faultManager,
              this.Lines,
              this.Counties,
              this.Regions,
              this.Big4Regions);
        }

        /// <summary>
        /// Gets a collection of all known big regions.
        /// </summary>
        public List<string> Big4Regions { get; private set; }

        /// <summary>
        /// Gets a collection of all known counties.
        /// </summary>
        public List<string> Counties { get; private set; }

        /// <summary>
        /// Gets a collection of all known lines.
        /// </summary>
        public List<string> Lines { get; private set; }

        /// <summary>
        /// Gets a collection of all known regions.
        /// </summary>
        public List<string> Regions { get; private set; }

        /// <summary>
        /// Gets an object which contains the collection of all location objects.
        /// </summary>
        public ILocationCollection Locations { get; private set; }

        /// <summary>
        /// Gets a collection of the names of all locations. 
        /// </summary>
        public List<string> LocationsByName
        {
            get
            {
                List<string> locations = new List<string>();
                foreach (ILocation location in this.Locations.Locations)
                {
                    locations.Add(location.Name);
                }

                return locations;
            }
        }

        /// <summary>
        /// Find a location with a given name.
        /// </summary>
        /// <remarks>
        /// If a location can't be found a new one is created and that one is returned.
        /// </remarks>
        /// <param name="name">The search parameter</param>
        /// <returns>The found location.</returns>
        public ILocation GetLocation(string name)
        {
            ILocation returnValue =
              this.Locations.Locations.Find(l => l.Name == name);

            if (returnValue != null)
            {
                return returnValue;
            }

            ILocation newLocation = new Location(name);

            this.Locations.Locations.Add(newLocation);
            //this.Locations.OrderBy(i => i.Name);
            //this.Locations.Sort();
            this.Locations.Locations =
                new List<ILocation>(
                    from i in this.Locations.Locations orderby i.Name select i);


            this.needsSaving = true;
            return newLocation;
        }

        /// <summary>
        /// Determine and return a collection of all locations starting with the 
        /// <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The search parameter</param>
        /// <returns>The collection of locations</returns>
        public List<ILocation> GetLocationsByLetter(string character)
        {
            List<ILocation> locations = new List<ILocation>();

            locations =
              this.Locations.Locations.FindAll
              (l => l.Name.Substring(0, 1) == character).ToList();

            return locations;
        }

        /// <summary>
        /// Save the locations.
        /// </summary>
        public void Save()
        {
            if (this.needsSaving)
            {
                this.locationFactory.WriteLocations(
                  this.locationBasePath + Path.DirectorySeparatorChar + "Location.txt",
                  this.Locations,
                  this.Lines,
                  this.Counties,
                  this.Regions,
                  this.Big4Regions);

                this.needsSaving = false;
            }
        }

        /// <summary>
        /// Update a location.
        /// </summary>
        /// <param name="updatedLocation">The location to update</param>
        public void UpdateLocation(ILocation updatedStn)
        {
            for (int index = 0; index < this.Locations.Locations.Count; ++index)
            {
                if (string.Compare(this.Locations.Locations[index].Name, updatedStn.Name) == 0)
                {
                    this.Locations.Locations[index] = updatedStn;
                    this.needsSaving = true;
                    break;
                }
            }

            this.Save();
        }

        /// <summary>
        /// Read a given file and return each line as a collection of strings.
        /// </summary>
        /// <param name="path">The file to read</param>
        /// <returns>The contents of the file.</returns>
        private List<string> ReadListFileContents(string path)
        {
            List<string> contents = new List<string>();

            using (StreamReader reader = new StreamReader(path, false))
            {
                string currentLine = string.Empty;
                currentLine = reader.ReadLine();

                while (currentLine != null)
                {
                    contents.Add(currentLine);
                    currentLine = reader.ReadLine();
                }
            }

            return contents;
        }
    }
}