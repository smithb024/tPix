using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces
{
  public interface ILocationManager
  {
    List<ILocation> Locations { get; }

    List<string> LocationsByName { get; }

    List<string> Lines { get; }

    List<string> Regions { get; }

    List<string> Counties { get; }

    List<string> Big4Regions { get; }

    void UpdateLocation(ILocation updatedStn);

    List<ILocation> GetLocationsByLetter(string character);

    ILocation GetStn(string name);

    void Save();
  }
}
