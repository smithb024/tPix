using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.Converters
{
  using System.Globalization;
  using System.Windows.Data;

  using tPix.Common.Enum;

  public class LocationToBoolConverter : IValueConverter
  {
    public LocationToBoolConverter()
    {
      Location = LocationType.None;
    }

    public LocationType Location
    {
      get;
      set;
    }

    public object Convert(
      object value, 
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (value == null)
      {
        return false;
      }

      if (value.GetType() != typeof(LocationType))
      {
        return false;
      }

      LocationType converted = (LocationType)value;
      return converted == this.Location;
    }

    public object ConvertBack(
      object value, 
      Type targetType, 
      object parameter,
      CultureInfo culture)
    {
      return this.Location;
    }
  }
}