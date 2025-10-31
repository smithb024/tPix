namespace tPix.Common.Messages
{
    using tPix.Common.Enum;

    /// <summary>
    /// Message class, used to request that the image list is generated.
    /// </summary>
    public class GenerateImageListMessage
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GenerateImageListMessage"/> class.
        /// </summary>
        /// <param name="cls">The selected class.</param>
        /// <param name="nmb">The selected number</param>
        /// <param name="useFilter">Indicates whether to use the filter</param>
        /// <param name="filter">The filter to use</param>
        public GenerateImageListMessage(
            string cls,
            string nmb,
            bool useFilter,
            LocationType filter)
        {
            this.Cls = cls;
            this.Nmb = nmb;
            this.UseFilter = useFilter;
            this.Filter = filter;
        }

        /// <summary>
        /// Gets the currently selected class. Empty if none selected.
        /// </summary>
        public string Cls { get; }

        /// <summary>
        /// Gets the currently selected number. Empty if none selected.
        /// </summary>
        public string Nmb { get; }

        /// <summary>
        /// Gets a value indicating whether a filter should be used.
        /// </summary>
        public bool UseFilter { get; }

        /// <summary>
        /// Gets the type of filter to use.
        /// </summary>
        public LocationType Filter { get; }
    }
}
