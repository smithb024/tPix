namespace tPix.BL.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    /// Interface which contains a collection of locations.
    /// </summary>
    public interface ILocationCollection
    {
        /// <summary>
        /// Gets a collection of locations.
        /// </summary>
        List<ILocation> Locations { get; }
    }
}
