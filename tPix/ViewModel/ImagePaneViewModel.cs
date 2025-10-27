namespace tPix.ViewModel
{
    using CommunityToolkit.Mvvm.Messaging;
    using NynaeveLib.ViewModel;
    using tPix.Common.Messages;

    /// <summary>
    /// View model which supports the image pane which is present on the main window.
    /// </summary>
    public class ImagePaneViewModel : ViewModelBase
    {
        /// <summary>
        /// The path to the image to be display.
        /// </summary>
        private string path;

        /// <summary>
        /// The description of the displayed image.
        /// </summary>
        private string description;

        /// <summary>
        /// Initialises a new instance of the <see cref="ImagePaneViewModel"/> class.
        /// </summary>
        public ImagePaneViewModel() 
        {
            this.path = string.Empty;
            this.description = string.Empty;

            this.Messenger.Register<DisplayImageMessage>(
                this,
                (r, message) => this.DisplayImage(message));

        }

        /// <summary>
        /// Gets or sets the path to the image to be displayed
        /// </summary>
        public string ImagePath 
        {
            get => this.path;
            set => this.SetProperty(ref this.path, value);
        }

        /// <summary>
        /// Gets or sets the image description.
        /// </summary>
        public string ImageDescription
        {
            get => this.description;
            set => this.SetProperty(ref this.description, value);
        }

        /// <summary>
        /// Handle a display image message. Update the properties.
        /// </summary>
        /// <param name="message">
        /// The message which has been received via the messenger service.
        /// </param>
        private void DisplayImage(DisplayImageMessage message)
        {
            this.ImagePath = message.Path;
            this.ImageDescription = message.Description;
        }
    }
}
