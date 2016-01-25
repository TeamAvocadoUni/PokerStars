namespace Poker.Models
{
    using System.Windows.Forms;

    public class Bot : Character
    {
        public Bot(Panel panel, int chips, bool folded, int call, int raise, double power, double type, bool turn, bool foldTurn) : base(panel, chips, folded, call, raise, power, type, turn, foldTurn)
        {
        }
    }
}
