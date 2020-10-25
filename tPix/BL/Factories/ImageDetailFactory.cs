using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tPix.BL.Factories
{
  using Interfaces;
  using Interfaces.ClsNmbConfig;
  using Model;

  public static class ImageDetailFactory
  {
    private static char majorTick = '-';
    private static char minorTick = '_';
    private static char alternativeTick = '~';
    private static char clsTick = '^';
    private static char extensionSeparator = '.';

    private class ClsClass
    {
      public ClsClass(
        List<ICls> clss,
        List<string> presentNmbs)
      {
        this.Clss = clss;
        this.PresentNmbs = presentNmbs;
      }

      public List<ICls> Clss { get; }
      public List<string> PresentNmbs { get; }
    }

    public static IImageDetails AnalyseImageDetails(
      string path,
      ILocationManager locationManager,
      IClsNmbManager clsNmbManager,
      IFaultManager faultManager)
    {
      IImageDetails image;

      string imageName = System.IO.Path.GetFileName(path);

      string[] filenameArray = imageName.Split(extensionSeparator);

      string[] inputArray    = filenameArray[0].Split(majorTick);

      if (inputArray.Length < 2 || inputArray.Length > 4)
      {
        faultManager.AddFault(
          "Can't split into nmb and stn",
          imageName);
      }

      string[] nmbsArray   = inputArray[0].Split(minorTick);

      ILocation stn =
        locationManager.GetStn(
          inputArray[1]);

      string year =
        inputArray.Length > 2 ?
        inputArray[2] :
        string.Empty;

      string multipleNote =
        inputArray.Length > 3 ?
        inputArray[3] :
        string.Empty;

      image =
        new ImageDetails(
          path,
          year,
          stn,
          multipleNote);

      ClsClass clss =
        ImageDetailFactory.GetCls(
          nmbsArray.ToList(),
          path,
          faultManager,
          clsNmbManager);
      image.SetClss(
        clss.Clss,
        clss.PresentNmbs);

      return image;
    }

    private static ClsClass GetCls(
      List<string> nmbs,
      string imageName,
      IFaultManager faultManager,
      IClsNmbManager clsNmbManager)
    {
      List<ICls> clss = new List<ICls>();
      List<string> present = new List<string>();

      foreach (string nmb in nmbs)
      {
        string[] individualNmbArray   = nmb.Split(clsTick);
        if (individualNmbArray.Length != 2)
        {
          faultManager.AddFault(
            "Numbers Incorrect",
            imageName);
          continue;
        }

        present.Add(individualNmbArray[1]);

        // Go to nmb manager and get all associated nmbs
        //List<string> foundNmbs = new List<string>() { individualNmbArray[1] };
        List<string> foundNmbs =
          clsNmbManager.FindAllNmbs(
            individualNmbArray[0],
            individualNmbArray[1]);
        bool foundCls = false;

        foreach (ICls cls in clss)
        {
          if (cls.Name == individualNmbArray[0])
          {
            foreach (string foundNmb in foundNmbs)
            {
              cls.AddNmb(foundNmb);
            }

            foundCls = true;
            break;
          }
        }

        if (!foundCls)
        {
          ICls newCls =
            new Cls(
              individualNmbArray[0]);
          foreach (string foundNmb in foundNmbs)
          {
            newCls.AddNmb(foundNmb);
          }

          clss.Add(newCls);
        }
      }

      return 
        new ClsClass(
          clss,
          present);
    }
  }
}