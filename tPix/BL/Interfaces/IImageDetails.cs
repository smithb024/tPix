namespace tPix.BL.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface which describe the information about a specific image.
    /// </summary>
    public interface IImageDetails
    {
        /// <summary>
        /// Gets the path of the image.
        /// </summary>
        string Path { get; }

        /// <summary>
        /// Gets the description associated with the image.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the collection of classes featured in the image.
        /// </summary>
        List<ICls> Clss { get; }

        /// <summary>
        /// Gets the collection of numbers featured in the image.
        /// </summary>
        List<string> PresentNmbs { get; }

        /// <summary>
        /// Gets the location featured in the image.
        /// </summary>
        ILocation Location { get; }

        /// <summary>
        /// Gets the location taken from the image name.
        /// </summary>
        string LocationLiteral { get; }

        /// <summary>
        /// Gets the year of the image.
        /// </summary>
        string Year { get; }

        /// <summary>
        /// Gets a value used to diferentiate between different images with the same details.
        /// </summary>
        string MultipleNote { get; }

        /// <summary>
        /// Sets the classes present in the image.
        /// </summary>
        /// <param name="clss">The classes to add</param>
        /// <param name="present"></param>
        void SetClss(
          List<ICls> clss,
          List<string> present);

        /// <summary>
        /// Update the <see cref="Location"/>.
        /// </summary>
        /// <param name="location">The new location</param>
        void SetLocation(
            ILocation location);

        /// <summary>
        /// Indicates whether the image features a class.
        /// </summary>
        /// <param name="cls">The class to check for</param>
        /// <returns>The return flag</returns>
        bool ContainsCls(string cls);

        /// <summary>
        /// Indicates whether the image features a number.
        /// </summary>
        /// <param name="cls">The number to check for</param>
        /// <returns>The return flag</returns>
        bool ContainsNmb(string nmb);
    }
}