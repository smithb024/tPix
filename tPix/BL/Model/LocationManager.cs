using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model
{
  using System;
  using System.IO;
  using System.Collections.Generic;
  using Interfaces;
  using Interfaces.Factories;
  using Factories;

  public class LocationManager : ILocationManager
  {
    /// <summary>
    /// Base path for the collection of images.
    /// </summary>
    private string locationBasePath;

    private bool needsSaving;

    private ILocationFactory locationFactory;

    public LocationManager(
      string path,
      FaultManager faultManager)
    {
      this.needsSaving = false;
      this.locationFactory = new LocationFactory();
      this.Locations = new List<ILocation>();

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

    public List<string> Big4Regions { get; private set; }

    public List<string> Counties { get; private set; }

    public List<string> Lines { get; private set; }

    public List<string> Regions { get; private set; }

    public List<ILocation> Locations { get; private set; }

    public List<string> LocationsByName
    {
      get
      {
        List<string> locations = new List<string>();
        foreach (ILocation location in this.Locations)
        {
          locations.Add(location.Name);
        }

        return locations;
      }
    }


    public ILocation GetStn(string name)
    {
      ILocation returnValue =
        this.Locations.Find(l => l.Name == name);

      if (returnValue != null)
      {
        return returnValue;
      }

      ILocation newLocation = new Location(name);

      this.Locations.Add(newLocation);
      //this.Locations.OrderBy(i => i.Name);
      //this.Locations.Sort();
            this.Locations =
        new List<ILocation>(
          from i in this.Locations orderby i.Name select i);


      this.needsSaving = true;
      return newLocation;
    }

    public List<ILocation> GetLocationsByLetter(string character)
    {
      List<ILocation> locations = new List<ILocation>();

      locations =
        this.Locations.FindAll
        (l => l.Name.Substring(0, 1) == character).ToList();

      return locations;
    }

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

    public void UpdateLocation(ILocation updatedStn)
    {
      for (int index = 0; index < this.Locations.Count; ++index)
      {
        if (string.Compare(this.Locations[index].Name, updatedStn.Name) == 0)
        {
          this.Locations[index] = updatedStn;
          this.needsSaving = true;
          break;
        }
      }

      this.Save();
    }

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
