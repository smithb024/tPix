using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces
{
  public interface IFaultManager
  {
    void ClearFaults();

    void AddFault(
      string description,
      string fileName);
  }
}
