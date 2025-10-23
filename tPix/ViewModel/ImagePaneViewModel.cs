namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;

    /// <summary>
    /// View model which supports the image pane which is present on the main window.
    /// </summary>
    public class ImagePaneViewModel : ViewModelBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ImagePaneViewModel"/> class.
        /// </summary>
        public ImagePaneViewModel() 
        {
        }

        /// <summary>
        /// Gets the path to the image to be displayed
        /// </summary>
        public string ImagePath =>
            this.ImagesIndex >= 0 && this.ImagesIndex < this.Images.Count ?
            this.Images[this.ImagesIndex].Path :
            string.Empty;
    }
}
