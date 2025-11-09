namespace tPix.BL.Interfaces
{
    using System.Collections.Generic;
    using tPix.BL.Model;

    /// <summary>
    /// Interface for a class which manages the locations.
    /// </summary>
    public interface ILocationManager
    {
        /// <summary>
        /// Gets an object which contains the collection of all location objects.
        /// </summary>
        LocationCollection Locations { get; }

        /// <summary>
        /// Gets a collection of the names of all locations.
        /// </summary>
        List<string> LocationsByName { get; }

        /// <summary>
        /// Gets a collection of all known lines.
        /// </summary>
        List<string> Lines { get; }

        /// <summary>
        /// Gets a collection of all known regions.
        /// </summary>
        List<string> Regions { get; }

        /// <summary>
        /// Gets  a collection of all known counties.
        /// </summary>
        List<string> Counties { get; }

        /// <summary>
        /// Gets a collection of all known big regions.
        /// </summary>
        List<string> Big4Regions { get; }

        /// <summary>
        /// Determine and return a collection of all locations starting with the 
        /// <paramref name="character"/>.
        /// </summary>
        /// <param name="character">The search parameter</param>
        /// <returns>The collection of locations</returns>
        List<Location> GetLocationsByLetter(string character);

        /// <summary>
        /// Find a location with a given name.
        /// </summary>
        /// <param name="name">The search parameter</param>
        /// <returns>The found location.</returns>
        Location GetLocation(string name);

        /// <summary>
        /// Save the locations.
        /// </summary>
        void Save();

        /// <summary>
        /// Use the details in the <paramref name="imageDetails"/> to see if it contains location
        /// which is not in the list.
        /// </summary>
        /// <param name="imageDetails">The image to use.</param>
        /// <returns>
        /// A value indicating if there have been any changes.
        /// </returns>
        bool Check(IImageDetails imageDetails);

        /// <summary>
        /// Order the locations in the model.
        /// </summary>
        /// <remarks>
        /// This would be done after a check has completed.
        /// </remarks>
        void OrderLocations();
    }
}