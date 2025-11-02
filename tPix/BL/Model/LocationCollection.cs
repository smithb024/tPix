namespace tPix.BL.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using tPix.BL.Interfaces;

    /// <summary>
    /// Contains a collection of <see cref="Location"/> objects.
    /// </summary>
    public class LocationCollection : ILocationCollection
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LocationCollection"/> class.
        /// </summary>
        [JsonConstructor]
        public LocationCollection() 
        {
            this.Locations = new List<ILocation>();
        }

        /// <summary>
        /// Gets a collection of locations.
        /// </summary>
        public List<ILocation> Locations { get; }
    }
}
