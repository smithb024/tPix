using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Interfaces.ClsNmbConfig
{
  public interface IClsNmbManager
  {
    List<IClsDetails> ClsDetails { get; }

    List<string> FindAllNmbs(
      string cls,
      string nmb);

    List<string> GetAllCls();

    List<string> GetNms(string clsName);
  }
}
