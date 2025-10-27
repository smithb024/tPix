namespace tPix.Common.Messages
{
    /// <summary>
    /// Message class used to request that a new image is displayed.
    /// </summary>
    public class DisplayImageMessage
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DisplayImageMessage"/> class.
        /// </summary>
        /// <param name="path">The image path</param>
        /// <param name="description">the image description</param>
        public DisplayImageMessage(
            string path,
            string description) 
        {        
            this.Path = path;
            this.Description = description;
        }

        /// <summary>
        /// Gets the path for the image.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Gets the image description.
        /// </summary>
        public string Description { get; }
    }
}
