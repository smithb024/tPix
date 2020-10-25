using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces
{
  public interface ICls
  {
    string Name { get; }

    List<string> Nmbs { get; }

    bool ContainsNms(string nmb);

    void AddNmb(string newNmb);
  }
}
