using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces.Factories
{
  using ClsNmbConfig;

  public interface IClsNmbFactory
  {
    List<IClsDetails> ReadClsDetails(
      IFaultManager faultManager);
  }
}
