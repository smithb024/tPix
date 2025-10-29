namespace tPix.ViewModel
{
    using CommunityToolkit.Mvvm.Messaging;
    using NynaeveLib.ViewModel;
    using System.Collections.ObjectModel;
    using System.Windows.Media;
    using tPix.BL;
    using tPix.Common;
    using tPix.Common.Enum;
    using tPix.Common.Messages;

    /// <summary>
    /// View model which supports the list pane which is present on the main window.
    /// </summary>
    public class ListPaneViewModel : ViewModelBase
    {
        /// <summary>
        /// The instance of the <see cref="BLManager"/>.
        /// </summary>
        private readonly BLManager blManager;

        /// <summary>
        /// The currently selected class.
        /// </summary>
        private string selectedClass;

        /// <summary>
        /// The currently selected number.
        /// </summary>
        private string selectedNumber;

        /// <summary>
        /// Indicates whether to use the filter.
        /// </summary>
        private bool useFilter;

        /// <summary>
        /// The type of filter to use.
        /// </summary>
        private LocationType locationType;

        /// <summary>
        /// The current location filter.
        /// </summary>
        private string location;

        /// <summary>
        /// The current line filter.
        /// </summary>
        private string line;

        /// <summary>
        /// The current county filter.
        /// </summary>
        private string county;

        /// <summary>
        /// The current region filter.
        /// </summary>
        private string region;

        /// <summary>
        /// The current big region filter.
        /// </summary>
        private string big4Region;

        /// <summary>
        /// Initialsies a new instance of the <see cref="ListPaneViewModel"/> class.
        /// </summary>
        /// <param name="blManager">The instance of the <see cref="BLManager"/>.</param>
        public ListPaneViewModel(BLManager blManager)
        {
            this.blManager = blManager;
            this.selectedClass = string.Empty;
            this.selectedNumber = string.Empty;
            this.useFilter = false;
            this.locationType = LocationType.None;
            this.location = string.Empty;
            this.line = string.Empty;
            this.county = string.Empty;
            this.region = string.Empty;
            this.big4Region = string.Empty;

            this.images =
                new ObservableCollection<ImageDescription>
                {
                    this.blManager.GetImage()
                };


            this.Messenger.Register<NewFiltersMessage>(
                this,
                (r, message) => this.FilterMessage(message));

            this.Messenger.Register<GenerateImageListMessage>(
                this,
                (r, message) => this.GenerateImageListMessage(message));
        }

        /// <summary>
        /// The collection of images being offered for selection.
        /// </summary>
        private ObservableCollection<ImageDescription> images;

        /// <summary>
        /// The index of the currently selected index.
        /// </summary>
        private int imagesIndex;

        /// <summary>
        /// Gets or sets the collection of images which are being offered for selection.
        /// </summary>
        public ObservableCollection<ImageDescription> Images
        {
            get => this.images;

            set => this.SetProperty(ref this.images, value);
        }

        /// <summary>
        /// Gets or sets the index of the currently selected index.
        /// </summary>
        public int ImagesIndex
        {
            get => this.imagesIndex;

            set
            {
                if (this.imagesIndex == value)
                {
                    return;
                }

                this.imagesIndex = value;
                this.OnPropertyChanged(nameof(this.ImagesIndex));

                this.SendDisplayStringMessage();
            }
        }

        private void GetImages()
        {
            if (!string.IsNullOrEmpty(this.selectedNumber))
            {
                if (this.useFilter)
                {
                    this.images =
                        this.blManager.GetImages(
                            this.selectedClass,
                            this.selectedNumber,
                            this.locationType,
                            this.GetFilterString());
                }
                else
                {
                    this.images =
                        this.blManager.GetImages(
                            this.selectedClass,
                            this.selectedNumber);
                }
            }
            else if (!string.IsNullOrEmpty(this.selectedClass))
            {
                if (this.useFilter)
                {
                    this.images =
                        this.blManager.GetImages(
                            this.selectedClass,
                            this.locationType,
                            this.GetFilterString());
                }
                else
                {
                    this.images =
                        this.blManager.GetImages(
                            this.selectedClass);
                }
            }
            else if (this.useFilter)
            {
                this.images =
                    this.blManager.GetImages(
                        this.locationType,
                        this.GetFilterString());
            }
            else
            {
                this.images =
                    new ObservableCollection<ImageDescription>
                    {
                        this.blManager.GetImage()
                    };
            }

            this.OnPropertyChanged(nameof(this.Images));

            this.imagesIndex = 0;
            this.OnPropertyChanged(nameof(this.ImagesIndex));

            this.SendDisplayStringMessage();
        }

        /// <summary>
        /// A new <see cref="NewFiltersMessage"/> has been received.
        /// </summary>
        /// <param name="message">The <see cref="NewFiltersMessage"/> message</param>
        private void FilterMessage(NewFiltersMessage message)
        {
            this.location = message.Location;
            this.line = message.Line;
            this.county = message.County;
            this.region = message.Region;
            this.big4Region = message.Big4Region;

            this.GetImages();
        }

        /// <summary>
        /// A new <see cref="GenerateImageListMessage"/> has been received.
        /// </summary>
        /// <param name="message">The <see cref="GenerateImageListMessage"/> message</param>
        private void GenerateImageListMessage(GenerateImageListMessage message)
        {
            this.selectedClass = message.Cls;
            this.selectedNumber = message.Nmb;
            this.useFilter = message.UseFilter;
            this.locationType = message.Filter;

            this.GetImages();
        }

        /// <summary>
        /// Determine which string to use for the filter.
        /// </summary>
        /// <returns>The selected string</returns>
        private string GetFilterString()
        {
            switch (this.locationType)
            {
                case LocationType.Location:
                    return this.location;

                case LocationType.Line:
                    return this.line;

                case LocationType.County:
                    return this.county;

                case LocationType.Region:
                    return this.region;

                case LocationType.Big4Location:
                    return this.big4Region;

                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// Create and send a <see cref="DisplayImageMessage"/> via the messenger.
        /// </summary>
        private void SendDisplayStringMessage()
        {
            string path =
                this.ImagesIndex >= 0 && this.ImagesIndex < this.Images.Count ?
                this.Images[this.ImagesIndex].Path :
                string.Empty;

            string description =
                this.ImagesIndex >= 0 && this.ImagesIndex < this.Images.Count ?
                this.Images[this.ImagesIndex].Description :
                string.Empty;
            DisplayImageMessage message =
                           new DisplayImageMessage(
                               path,
                               description);
            this.Messenger.Send(message);
        }
    }
}
