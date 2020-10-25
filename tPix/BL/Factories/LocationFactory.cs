using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Factories
{
  using System;
  using System.Collections.Generic;
  using System.IO;
  using Interfaces.Factories;
  using Interfaces;
  using Model;

  public class LocationFactory : ILocationFactory
  {
    public const char Separator = '|';
    public const int NameIndex = 0;
    public const int LineIndex = 1;
    public const int CountyIndex = 2;
    public const int RegionIndex = 3;
    public const int Big4Index = 4;

    public List<ILocation> ReadLocations(
      string fileName,
      IFaultManager faultManager,
      List<string> lines,
      List<string> counties,
      List<string> regions,
      List<string> big4regions)
    {
      List<ILocation> locations = new List<ILocation>();

      if (!File.Exists(fileName))
      {
        return locations;
      }

      using (StreamReader reader = new StreamReader(fileName))
      {
        string currentLine = string.Empty;
        currentLine = reader.ReadLine();

        while (currentLine != null)
        {
          string[] currentLineArray = currentLine.Split(Separator);

          if (currentLineArray.Length != 5)
          {
            faultManager.AddFault(
              "Invalid line in locations file",
              fileName);
            continue;
          }

          int? line =
            this.GetIndex(
              lines,
              currentLineArray[LineIndex]);
          int? county =
            this.GetIndex(
              counties,
              currentLineArray[CountyIndex]);
          int? region =
            this.GetIndex(
              regions,
              currentLineArray[RegionIndex]);
          int? big4 =
            this.GetIndex(
              big4regions,
              currentLineArray[Big4Index]);

          locations.Add(
            new Location(
              currentLineArray[NameIndex],
              line,
              county,
              region,
              big4));

          currentLine = reader.ReadLine();
        }
      }

      return locations;
    }

    public void WriteLocations(
      string fileName,
      List<ILocation> locations,
      List<string> lines,
      List<string> counties,
      List<string> regions,
      List<string> big4regions)
    {
      using (StreamWriter writer = new StreamWriter(fileName))
      {
        foreach (ILocation location in locations)
        {
          writer.WriteLine(
            $"{location.Name}{Separator}{this.GetValue(lines, location.Line)}{Separator}{this.GetValue(counties, location.County)}{Separator}{this.GetValue(regions, location.Region)}{Separator}{this.GetValue(big4regions, location.Region)}");
        }
      }
    }

    private int? GetIndex(
      List<string> stringList,
      string comparisonString)
    {
      for (int index = 0; index < stringList.Count; ++index)
      {
        if (string.Compare(stringList[index], comparisonString, true) == 0)
        {
          return index;
        }
      }

      return null;
    }

    private string GetValue(
      List<string> stringList,
      int? index)
    {
      if (index == null || index < 0 || index >= stringList.Count)
      {
        return string.Empty;
      }

      return stringList[(int)index];
    }
  }
}
