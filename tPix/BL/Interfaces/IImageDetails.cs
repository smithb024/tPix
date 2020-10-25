using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces
{
  public interface IImageDetails
  {
    string Path { get; }

    string Description { get; }

    List<ICls> Clss { get; }

    List<string> PresentNmbs { get; }

    ILocation Stn { get; }

    string Year { get; }

    void SetClss(
      List<ICls> clss,
      List<string> present);

    bool ContainsCls(string cls);

    bool ContainsNmb(string nmb);
  }
}
