namespace tPix.BL.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Interfaces.ClsNmbConfig;
    using Model;

    /// <summary>
    /// Factory class which is used to analyse an image.
    /// </summary>
    public static class ImageDetailFactory
    {
        /// <summary>
        /// Character which separates the sections in the name.
        /// </summary>
        private static char majorTick = '-';

        /// <summary>
        /// Character which separates a section in the name.
        /// </summary>
        private static char minorTick = '_';

        /// <summary>
        /// Not currently used.
        /// </summary>
        private static char alternativeTick = '~';

        /// <summary>
        /// Character which separates a class name.
        /// </summary>
        private static char clsTick = '^';

        /// <summary>
        /// The extention marker.
        /// </summary>
        private static char extensionSeparator = '.';

        /// <summary>
        /// Class which describes a class.
        /// </summary>
        private class ClsClass
        {
            /// <summary>
            /// Initialises a new instance of the <see cref="ClsClass"/> class.
            /// </summary>
            /// <param name="clss">List of classes</param>
            /// <param name="presentNmbs">list of numbers</param>
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

        /// <summary>
        /// Analyse an image and return the details.
        /// </summary>
        /// <param name="path">Image path</param>
        /// <param name="locationManager">The instance of the location manager</param>
        /// <param name="clsNmbManager">The instance of the class number manager</param>
        /// <param name="faultManager">The instance of the fault manager</param>
        /// <returns>The image details.</returns>
        public static IImageDetails AnalyseImageDetails(
          string path,
          ILocationManager locationManager,
          IClsNmbManager clsNmbManager,
          IFaultManager faultManager)
        {
            string imageName = string.Empty;

            try
            {
                IImageDetails image;

                imageName = System.IO.Path.GetFileName(path);

                string[] filenameArray = imageName.Split(extensionSeparator);

                string[] inputArray = filenameArray[0].Split(majorTick);

                if (inputArray.Length < 2 || inputArray.Length > 4)
                {
                    faultManager.AddFault(
                      "Can't split into nmb and stn",
                      imageName);
                    return null;
                }

                string[] nmbsArray = inputArray[0].Split(minorTick);

                Location location =
                    locationManager.GetLocation(
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
                        location,
                        inputArray[1],
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                faultManager.AddFault(
                    "Can't split into nmb and stn",
                    imageName);

                return null;
            }
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
                string[] individualNmbArray = nmb.Split(clsTick);
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