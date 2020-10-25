using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.Common
{
  using NynaeveLib.ViewModel;

  public class ImageDescription : ViewModelBase
  {
    private string path;
    private string description;

    public ImageDescription(
      string path,
      string description)
    {
      this.path = path;
      this.description = description;
    }

    public string Path => this.path;
    public string Description => this.description;
  }
}
