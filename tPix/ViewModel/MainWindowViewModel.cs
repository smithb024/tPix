namespace tPix.ViewModel
{
    using Common.Enum;
    using CommunityToolkit.Mvvm.Messaging;
    using NynaeveLib.ViewModel;
    using tPix.Common.Messages;

    public class MainWindowViewModel : ViewModelBase
    {
        /// <summary>
        /// The description of the currently selected image.
        /// </summary>
        private string imageDescription;

        /// <summary>
        /// Initialises a new instance of the <see cref="MainWindowViewModel"/> class.
        /// </summary>
        public MainWindowViewModel()
        {
            this.imageDescription = string.Empty;

            this.Messenger.Register<DisplayImageMessage>(
                this,
                (r, message) => this.DisplayImage(message));

            GenerateImageListMessage imageListMessage =
                new GenerateImageListMessage(
                    string.Empty,
                    string.Empty,
                    false,
                    LocationType.None);

            this.Messenger.Send(imageListMessage);
        }

        /// <summary>
        /// Gets or sets the description of the currently selected image.
        /// </summary>
        public string ImageDescription
        {
            get => this.imageDescription;
            set => this.SetProperty(ref this.imageDescription, value);
        }

        /// <summary>
        /// Handle a display image message. Update the properties.
        /// </summary>
        /// <param name="message">
        /// The message which has been received via the messenger service.
        /// </param>
        private void DisplayImage(DisplayImageMessage message)
        {
            this.ImageDescription = message.Description;
        }
    }
}