using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces
{
  public interface ILocation
  {
    string Name { get; }

    int? Line { get; set; }

    int? Region { get; set; }

    int? County { get; set; }

    int? Big4 { get; set; }
  }
}
