namespace tPix.ViewModel
{
    using NynaeveLib.ViewModel;
    using System.Collections.ObjectModel;
    using tPix.BL;
    using tPix.Common;

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
        /// Initialsies a new instance of the <see cref="ListPaneViewModel"/> class.
        /// </summary>
        /// <param name="blManager">The instance of the <see cref="BLManager"/>.</param>
        public ListPaneViewModel(BLManager blManager)
        {
            this.blManager = blManager;
            this.images =
                new ObservableCollection<ImageDescription>
                {
                    this.blManager.GetImage()
                };
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
                this.OnPropertyChanged(nameof(this.ImagePath));
                this.OnPropertyChanged(nameof(this.ImageDescription));
            }
        }
    }
}
