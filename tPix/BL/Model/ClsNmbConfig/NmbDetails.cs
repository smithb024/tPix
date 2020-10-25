using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model.ClsNmbConfig
{
  using Interfaces.ClsNmbConfig;

  public class NmbDetails : INmbDetails
  {
    public NmbDetails(List<string> nmbs)
    {
      this.Nmbs = new List<string>();

      foreach (string nmb in nmbs)
      {
        if (!string.IsNullOrEmpty(nmb))
        {
          this.Nmbs.Add(nmb);
        }
      }
    }

    public List<string> Nmbs { get; private set; }
  }
}
