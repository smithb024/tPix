namespace tPix.BL.Model
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using tPix.BL.Interfaces;
    using System.Linq;

    /// <summary>
    /// Contains a collection of <see cref="Location"/> objects.
    /// </summary>
    public class LocationCollection
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LocationCollection"/> class.
        /// </summary>
        [JsonConstructor]
        public LocationCollection() 
        {
            this.Locations = new List<Location>();
        }

        /// <summary>
        /// Gets a collection of locations.
        /// </summary>
        public List<Location> Locations { get; private set; }

        /// <summary>
        /// Order the collection.
        /// </summary>
        public void Order()
        {
            this.Locations =
                new List<Location>(
                    from i in this.Locations orderby i.Name select i);
        }
    }
}
