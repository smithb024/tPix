using tPix.Common.Enum;

namespace tPix.Common.Messages
{
    /// <summary>
    /// Message class, used to indicate that a new filter has been selected.
    /// </summary>
    public class NewFiltersMessage
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NewFiltersMessage"/> class.
        /// </summary>
        /// <param name="type">The last filter type to be selected</param>
        /// <param name="location">the location</param>
        /// <param name="line">the line</param>
        /// <param name="county">the county</param>
        /// <param name="region">the region</param>
        /// <param name="big4Region">the big region</param>
        public NewFiltersMessage(
            LocationType type,
            string location,
            string line,
            string county,
            string region,
            string big4Region) 
        {
            this.NewType = type;
            this.Location = location;
            this.Line = line;
            this.County = county;
            this.Region = region;
            this.Big4Region = big4Region;
        }

        /// <summary>
        /// Gets the last filter to be selected.
        /// </summary>
        private LocationType NewType { get; }

        /// <summary>
        /// Gets the name of the currently selected region.
        /// </summary>
        private string Location { get; }

        /// <summary>
        /// Gets the name of the currently line region.
        /// </summary>
        private string Line { get; }

        /// <summary>
        /// Gets the name of the currently county region.
        /// </summary>
        private string County { get; }

        /// <summary>
        /// Gets the name of the currently selected region.
        /// </summary>
        private string Region { get; }

        /// <summary>
        /// Gets the name of the currently selected big region.
        /// </summary>
        private string Big4Region { get; }
    }
}
