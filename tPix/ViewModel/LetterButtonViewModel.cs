namespace tPix.ViewModel
{
    using System;
    using System.Windows.Input;
    using NynaeveLib.ViewModel;
    using tPix.ViewModel.Cmd;

    /// <summary>
    /// View model which supports the letter buttons. 
    /// </summary>
    public class LetterButtonViewModel : ViewModelBase
    {
        /// <summary>
        /// The letter to display on the view.
        /// </summary>
        private readonly string letter;

        /// <summary>
        /// The action to run when the button is pressed.
        /// </summary>
        private readonly Action<string> runCommand;

        /// <summary>
        /// Indicates whether this is the lasted selected button.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// Initialises a new instance of the <see cref="LetterButtonViewModel"/> class.
        /// </summary>
        /// <param name="letter">The letter to display</param>
        /// <param name="runCommand">The command to run when the button is pressed.</param>
        public LetterButtonViewModel(
          string letter,
          Action<string> runCommand)
        {
            this.letter = letter;
            this.LetterCommand = new LetterCmd(this, this.SelectLetter);
            this.runCommand = runCommand;
            this.isSelected = false;
        }

        /// <summary>
        /// Gets the letter to display on the view.
        /// </summary>
        public string Letter => this.letter;

        /// <summary>
        /// Gets the command to run when the button is pressed.
        /// </summary>
        public ICommand LetterCommand { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether this button is selected.
        /// </summary>
        public bool IsSelected
        {
            get => this.isSelected; 
            private set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.OnPropertyChanged(nameof(this.isSelected));
                }
            }
        }

        /// <summary>
        /// The the letter of the button which is selected. 
        /// </summary>
        /// <remarks>
        /// It is the responsibility of the button to decide if it is the one which is selected.
        /// </remarks>
        /// <param name="selectedLetter">Letter of the selected button</param>
        public void SelectedButton(string selectedLetter)
        {
            this.IsSelected =
                string.Compare(
                    this.Letter,
                    selectedLetter) == 0;
        }

        /// <summary>
        /// Run the command.
        /// </summary>
        private void SelectLetter()
        {
            this.runCommand.Invoke(this.Letter);
        }
    }
}