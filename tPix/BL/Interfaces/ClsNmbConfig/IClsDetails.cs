using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces.ClsNmbConfig
{
  public interface IClsDetails
  {
    string Name { get; }

    List<INmbDetails> NmbDetails { get; }

    List<string> FindAllNmbs();

    List<string> FindAllNmbs(
      string nmb);
  }
}
