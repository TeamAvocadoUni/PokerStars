namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface IMessage
    {
        void OutputMessage(string message);

        DialogResult ShowMessageBox(string message, string title, MessageBoxButtons buttons);
    }
}
