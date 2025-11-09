namespace tPix.BL.Model
{
    using Interfaces;
    using Newtonsoft.Json;

    /// <summary>
    /// A location object. This is JSON serialisable.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="line">The line</param>
        /// <param name="county">The county</param>
        /// <param name="region">The region</param>
        /// <param name="big4">The big region</param>
        [JsonConstructor]
        public Location(
            string name,
            string line,
            string county,
            string region,
            string big4)
        {
            this.Name = name;
            this.Line = line;
            this.County = county;
            this.Region = region;
            this.Big4 = big4;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Location"/> class
        /// </summary>
        /// <param name="name">The name</param>
        public Location(
          string name)
          : this(name, string.Empty, string.Empty, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Gets or sets the name of the location.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the location's line.
        /// </summary>
        public string Line { get; set; }

        /// <summary>
        /// Gets or sets the location's region.
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Gets or sets the location's county.
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// Gets or sets the location's big region.
        /// </summary>
        public string Big4 { get; set; }

    }
}
