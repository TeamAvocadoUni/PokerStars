namespace Poker.Interfaces
{
    using System.Windows.Forms;

    public interface ISingleBet
    {
        TextBox BetTextBox { get; }

        int BetValue { get; }

        int LastBet { get; set; }

        void SetBet(int chips);

        void AddBet(int chips);

        void Clear();
    }
}
