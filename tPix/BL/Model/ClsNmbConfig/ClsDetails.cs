using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model.ClsNmbConfig
{
  using Interfaces.ClsNmbConfig;

  public class ClsDetails : IClsDetails
  {
    public ClsDetails(
      string name,
      List<INmbDetails> nmbs)
    {
      this.Name = name;
      this.NmbDetails = nmbs;
    }

    public string Name { get; private set; }

    public List<INmbDetails> NmbDetails { get; private set; }

    public List<string> FindAllNmbs()
    {
      List<string> nmbs = new List<string>();

      foreach (INmbDetails nmbDetail in this.NmbDetails)
      {
        foreach (string nmb in nmbDetail.Nmbs)
        {
          nmbs.Add(nmb);
        }
      }

      return nmbs;
    }

    public List<string> FindAllNmbs(string nmb)
    {
      foreach (INmbDetails nmbDetail in this.NmbDetails)
      {
        if (nmbDetail.Nmbs.Any(n => n == nmb))
        {
          return nmbDetail.Nmbs;
        }
      }

      return new List<string> { nmb };
    }
  }
}