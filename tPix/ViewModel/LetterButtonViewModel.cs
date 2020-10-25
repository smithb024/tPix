namespace tPix.ViewModel
{
  using System;
  using System.Windows.Input;
  using NynaeveLib.ViewModel;
  using tPix.ViewModel.Cmd;

  public class LetterButtonViewModel : ViewModelBase
  {
    private string letter;
    private Action<string> runCommand;

    public LetterButtonViewModel(
      string letter,
      Action<string> runCommand)
    {
      this.letter = letter;
      this.LetterCommand = new LetterCmd(this, SelectLetter);
      this.runCommand = runCommand;
    }

    public string Letter => letter;

    public ICommand LetterCommand
    {
      get;
      private set;
    }

    public void SelectLetter()
    {
      this.runCommand.Invoke(this.Letter);
    }
  }
}
