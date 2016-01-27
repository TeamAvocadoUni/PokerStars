namespace Poker.CustomMessages
{
    using System.Windows.Forms;

    using Poker.Interfaces;
    public class Message : IMessage
    {
        public void OutputMessage(string message)
        {
            MessageBox.Show(message);
        }

        public DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons)
        {
            return MessageBox.Show(message, title, buttons);
        }
    }
}
