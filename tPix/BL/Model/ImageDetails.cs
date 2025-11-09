namespace tPix.BL.Model
{
    using System.Collections.Generic;
    using Interfaces;

    /// <summary>
    /// Class which describes a single image file.
    /// </summary>
    public class ImageDetails : IImageDetails
    {
        /// <summary>
        /// Initialise a new instance of the <see cref="ImageDetails"/> class.
        /// </summary>
        /// <param name="path">The image path</param>
        /// <param name="year">The image year</param>
        /// <param name="loc">The image location</param>
        /// <param name="locationLiteral">The image location taken from the name</param>
        /// <param name="multipleNote">
        /// Unique identifier for different images with the same details.
        /// </param>
        public ImageDetails(
          string path,
          string year,
          Location loc,
          string locationLiteral,
          string multipleNote)
        {
            this.Path = path;
            this.Year = year;
            this.Clss = new List<ICls>();
            this.Location = loc;
            this.LocationLiteral = locationLiteral;
            this.MultipleNote = multipleNote;
        }

        /// <summary>
        /// Gets the classes featured in the image.
        /// </summary>
        public List<ICls> Clss { get; private set; }

        /// <summary>
        /// Gets the numbers featured in the image.
        /// </summary>
        public List<string> PresentNmbs { get; private set; }

        /// <summary>
        /// Gets the path image.
        /// </summary>
        public string Path { get; private set; }

        /// <summary>
        /// Gets the location of the image.
        /// </summary>
        public Location Location { get; private set; }

        /// <summary>
        /// Gets the location taken from the image name.
        /// </summary>
        public string LocationLiteral { get; private set; }

        /// <summary>
        /// Gets the year of the image.
        /// </summary>
        public string Year { get; private set; }

        /// <summary>
        /// Gets a value used to diferentiate between different images with the same details.
        /// </summary>
        public string MultipleNote { get; private set; }

        /// <summary>
        /// Sets the classes featured in the image
        /// </summary>
        /// <param name="clss">collection of classes</param>
        /// <param name="present">collection of numbers</param>
        public void SetClss(
          List<ICls> clss,
          List<string> present)
        {
            this.Clss = clss;
            this.PresentNmbs = present;
        }

        /// <summary>
        /// Update the <see cref="Location"/>.
        /// </summary>
        /// <param name="location">The new location</param>
        public void SetLocation(
            Location location)
        {
            this.Location = location;
        }

        /// <summary>
        /// Gets the description of the image.
        /// </summary>
        public string Description
        {
            get
            {
                string description = string.Empty;
                foreach (string present in this.PresentNmbs)
                {
                    description = $"{description} {present}";
                }

                description = $"{description} {this.Location.Name} {this.Year} {this.MultipleNote}";
                return description;
            }
        }

        /// <summary>
        /// Indicates whether the image features a class.
        /// </summary>
        /// <param name="cls">The class to check for</param>
        /// <returns>The return flag</returns>
        public bool ContainsCls(string clsName)
        {
            return this.Clss.Exists(c => c.Name == clsName);
        }

        /// <summary>
        /// Indicates whether the image features a number.
        /// </summary>
        /// <param name="nmb">The number to check for</param>
        /// <returns>The return flag</returns>
        public bool ContainsNmb(string nmb)
        {
            return this.Clss.Exists(c => c.ContainsNms(nmb));
        }
    }
}