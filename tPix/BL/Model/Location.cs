using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Model
{
  using Interfaces;

  public class Location : ILocation
  {
    public Location(
      string name,
      int? line,
      int? county,
      int? region,
      int? big4)
    {
      this.Name = name;
      this.Line = line;
      this.County = county;
      this.Region = region;
      this.Big4 = big4;
    }

    public Location(
      string name)
      : this (name, null, null, null, null)
    {
    }

    public int? Big4 { get; set; }

    public int? County { get; set; }

    public int? Line { get; set; }

    public string Name { get; set; }

    public int? Region { get; set; }
  }
}
