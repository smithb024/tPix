using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model
{
  using Interfaces;

  public class Cls : ICls
  {
    public Cls(
      string name)
    {
      this.Name = name;
      this.Nmbs = new List<string>();
    }

    public string Name { get; private set; }

    public List<string> Nmbs { get; private set; }

    public void AddNmb(string newNmb)
    {
      this.Nmbs.Add(newNmb);
    }

    public bool ContainsNms(string nmb)
    {
      return this.Nmbs.Contains(nmb);
    }
  }
}
