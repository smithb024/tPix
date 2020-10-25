using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model.ClsNmbConfig
{
  using Interfaces.ClsNmbConfig;
  using BL.Factories;

  public class ClsNmbManager : IClsNmbManager
  {
    public ClsNmbManager(List<IClsDetails> clsDetails)
    {
      this.ClsDetails = clsDetails;
    }

    public List<IClsDetails> ClsDetails { get; private set; }

    public List<string> FindAllNmbs(string cls, string nmb)
    {
      foreach (IClsDetails clsdetails in this.ClsDetails)
      {
        if (clsdetails.Name == cls)
        {
          return clsdetails.FindAllNmbs(
            nmb);
        }
      }

      return new List<string> { nmb };
    }

    public List<string> GetAllCls()
    {
      List<string> classes = new List<string>();

      if (this.ClsDetails != null)
      {
        foreach (IClsDetails cls in this.ClsDetails)
        {
          classes.Add(cls.Name);
        }
      }

      return classes;
    }

    public List<string> GetNms(string clsName)
    {
      if (this.ClsDetails == null)
      {
        return new List<string>();
      }

      return this.ClsDetails.Find(cd => cd.Name == clsName).FindAllNmbs();
    }
  }
}
