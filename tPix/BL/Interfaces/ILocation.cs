namespace tPix.BL.Interfaces
{
    /// <summary>
    /// An interface which describes a location object.
    /// </summary>
    public interface ILocation
    {
        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets or sets the location's line.
        /// </summary>
        string Line { get; set; }

        /// <summary>
        /// Gets or sets the location's region.
        /// </summary>
        string Region { get; set; }

        /// <summary>
        /// Gets or sets the location's county.
        /// </summary>
        string County { get; set; }

        /// <summary>
        /// Gets or sets the location's big region.
        /// </summary>
        string Big4 { get; set; }
    }
}