using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Factories
{
  using System.IO;
  using Interfaces.ClsNmbConfig;
  using Interfaces.Factories;
  using Interfaces;
  using Model.ClsNmbConfig;

  public class ClsNmbFactory : IClsNmbFactory
  {
    /// <summary>
    /// Base path for the collection of images.
    /// </summary>
    private string basePath;

    private const char NmbSeparator = '|';

    public ClsNmbFactory(string basePath)
    {
      this.basePath = basePath;
    }

    public List<IClsDetails> ReadClsDetails(
      IFaultManager faultManager)
    {
      List<IClsDetails> clsCollection = new List<IClsDetails>();
      string path = this.basePath + Path.DirectorySeparatorChar + "ClsNmb";

      string[] files =
        Directory.GetFiles(
          path,
          "*.txt");

      for (int filesIndex = 0; filesIndex < files.Length; ++filesIndex)
      {
        IClsDetails cls = this.AnalyseFile(files[filesIndex]);
        clsCollection.Add(cls);
      }

      return clsCollection;
    }

    private IClsDetails AnalyseFile(string path)
    {
      List<INmbDetails> nmbDetails = new List<INmbDetails>();

      using (StreamReader reader = new StreamReader(path, false))
      {
        string currentLine = string.Empty;
        currentLine = reader.ReadLine();
        while (currentLine != null)
        {
          INmbDetails nmbDetail = this.AnalyseLine(currentLine);
          nmbDetails.Add(nmbDetail);
          currentLine = reader.ReadLine();
        }
      }

      return
        new ClsDetails(
          Path.GetFileNameWithoutExtension(path),
          nmbDetails);
    }

    private INmbDetails AnalyseLine(string fileLine)
    {
      return new NmbDetails(fileLine.Split(NmbSeparator).ToList());
    }
  }
}
