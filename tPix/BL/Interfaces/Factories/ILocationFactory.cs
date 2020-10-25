using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces.Factories
{
  using System.Collections.Generic;

  public interface ILocationFactory
  {
    List<ILocation> ReadLocations(
      string fileName,
      IFaultManager faultManager,
      List<string> lines,
      List<string> counties,
      List<string> regions,
      List<string> big4regions);

    void WriteLocations(
      string fileName,
      List<ILocation> locations,
      List<string> lines,
      List<string> counties,
      List<string> regions,
      List<string> big4regions);
  }
}
