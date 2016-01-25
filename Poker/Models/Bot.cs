namespace Poker.Models
{
    using System.Windows.Forms;

    public class Bot : Character
    {
        public Bot(int chips, bool folded, int call, int raise, double power, double type, bool turn, bool foldTurn) : base(chips, folded, call, raise, power, type, turn, foldTurn)
        {
        }
    }
}
