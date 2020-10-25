using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.Converters
{
  using System;
  using System.IO;
  using System.Globalization;
  using System.Windows.Data;

  using tPix.Common.Enum;

  public class PathConverter : IValueConverter
  {
    public object Convert(
      object value, 
      Type targetType,
      object parameter,
      CultureInfo culture)
    {
      if (value == null)
      {
        return string.Empty;
      }

      if (value.GetType() != typeof(string))
      {
        return value;
      }

      return Path.GetFileNameWithoutExtension((string)value);
    }

    public object ConvertBack(
      object value, 
      Type targetType, 
      object parameter,
      CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}