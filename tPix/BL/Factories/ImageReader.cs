namespace tPix.BL.Factories
{
  using System.Collections.Generic;
  using Interfaces;
  using Interfaces.Factories;
  using Interfaces.ClsNmbConfig;

  public class ImageReader : IImageReader
  {
    /// <summary>
    /// Collection of all images found.
    /// </summary>
    private List<IImageDetails> images;

    /// <summary>
    /// Base path for the collection of images.
    /// </summary>
    private string basePath;

    /// <summary>
    /// Initialise a new instance of the <see cref="ImageReader"/> class.
    /// </summary>
    /// <param name="basePath">base path</param>
    public ImageReader(string basePath)
    {
      this.Initialise();
      this.basePath = basePath;
    }

    /// <summary>
    /// Get all the images in the base path.
    /// </summary>
    /// <returns></returns>
    public List<IImageDetails> ReadImages(
      ILocationManager locationManager,
      IClsNmbManager clsNmbManager,
      IFaultManager faultManager)
    {
      this.Initialise();

      string[] directories = System.IO.Directory.GetDirectories(basePath);
      string[] files = System.IO.Directory.GetFiles(basePath, "*.jpg");

      if (files != null && files.Length > 0)
      {
        this.AnalyseFiles(
          files,
          locationManager,
          clsNmbManager,
          faultManager);
      }

      if (directories != null && directories.Length > 0)
      {
        this.AnalyseDirectory(
          directories,
          locationManager,
          clsNmbManager,
          faultManager);
      }

      return this.images;
    }

    private void AnalyseFiles(
      string[] files,
      ILocationManager locationManager,
      IClsNmbManager clsNmbManager,
      IFaultManager faultManager)
    {
      //List<IImageDetails> images = new List<IImageDetails>();

      for (int index = 0; index < files.Length; ++index)
      {
        IImageDetails image =
          ImageDetailFactory.AnalyseImageDetails(
            files[index],
            locationManager,
            clsNmbManager,
            faultManager);

        this.images.Add(image);
      }

      //return this.images;
    }

    private void AnalyseDirectory(
      string[] directories,
      ILocationManager locationManager,
      IClsNmbManager clsNmbManager,
      IFaultManager faultManager)
    {
      for (int index = 0; index < directories.Length; ++index)
      {
        string[] subDirectories = System.IO.Directory.GetDirectories(directories[index]);
        string[] files = System.IO.Directory.GetFiles(directories[index], "*.jpg");

        if (files != null && files.Length > 0)
        {
          this.AnalyseFiles(
            files,
            locationManager,
            clsNmbManager,
            faultManager);
        }

        if (subDirectories != null && subDirectories.Length > 0)
        {
          this.AnalyseDirectory(
            subDirectories,
            locationManager,
            clsNmbManager,
            faultManager);
        }
      }
    }

    /// <summary>
    /// Initialise the fields in the class.
    /// </summary>
    private void Initialise()
    {
      this.images = new List<IImageDetails>();
    }

    private static char majorTick = '-';
    private static char minorTick = '_';
    private static char alternativeTick = '~';
    private static char clsTick = '^';
    private static char extensionSeparator = '.';

  }
}