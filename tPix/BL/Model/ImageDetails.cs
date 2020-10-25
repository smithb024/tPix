using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model
{
  using Interfaces;

  public class ImageDetails : IImageDetails
  {
    /// <summary>
    /// Initialise a new instance of the <see cref="ImageDetails"/> class.
    /// </summary>
    public ImageDetails(
      string path,
      string year,
      ILocation stn,
      string multipleNote)
    {
      this.Path = path;
      this.Year = year;
      this.Clss = new List<ICls>();
      this.Stn = stn;
      this.MultipleNote = multipleNote;
    }

    public List<ICls> Clss { get; private set; }

    public List<string> PresentNmbs { get; private set; }

    public string Path { get; private set; }

    public ILocation Stn { get; private set; }

    public string Year { get; private set; }

    public string MultipleNote { get; private set; }

    public void SetClss(
      List<ICls> clss,
      List<string> present)
    {
      this.Clss = clss;
      this.PresentNmbs = present;
    }

    public string Description
    {
      get
      {
        string description = string.Empty;
        foreach (string present in this.PresentNmbs)
        {
          description = $"{description} {present}";
        }

        description = $"{description} {Stn.Name} {this.Year} {this.MultipleNote}";
        return description;
      }
    }

    public bool ContainsCls(string clsName)
    {
      return this.Clss.Exists(c => c.Name == clsName);
    }

    public bool ContainsNmb(string nmb)
    {
      return this.Clss.Exists(c => c.ContainsNms(nmb));
    }
  }
}
