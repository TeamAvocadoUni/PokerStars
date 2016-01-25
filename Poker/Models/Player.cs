using System.Windows.Forms;

namespace Poker.Models
{
    public class Player : Character
    {
        public const int FirstCardNumber = 0;
        public const int SecondCardNumber = 1;
        public const string PlayerName = "Player";

        public Player(Panel panel, int chips, bool folded, int call, int raise, double power, double type, bool turn, bool foldTurn) : base(panel, chips, folded, call, raise, power, type, turn, foldTurn)
        {
            this.Turn = true;
        }

        //public override void Rules(int card1Number, int card2Number, string playerName, double type, double power, bool foldedTurn)
        //{
        //    if (!foldedTurn || card1Number == 0 && card2Number == 1 && this.StplayerStatus.Text.Contains("Fold") == false)
        //    {
        //        #region Variables
        //        bool done = false, vf = false;
        //        int[] Straight1 = new int[5];
        //        int[] Straight = new int[7];
        //        Straight[0] = Reserve[c1];
        //        Straight[1] = Reserve[c2];
        //        Straight1[0] = Straight[2] = Reserve[12];
        //        Straight1[1] = Straight[3] = Reserve[13];
        //        Straight1[2] = Straight[4] = Reserve[14];
        //        Straight1[3] = Straight[5] = Reserve[15];
        //        Straight1[4] = Straight[6] = Reserve[16];
        //        var a = Straight.Where(o => o % 4 == 0).ToArray();
        //        var b = Straight.Where(o => o % 4 == 1).ToArray();
        //        var c = Straight.Where(o => o % 4 == 2).ToArray();
        //        var d = Straight.Where(o => o % 4 == 3).ToArray();
        //        var st1 = a.Select(o => o / 4).Distinct().ToArray();
        //        var st2 = b.Select(o => o / 4).Distinct().ToArray();
        //        var st3 = c.Select(o => o / 4).Distinct().ToArray();
        //        var st4 = d.Select(o => o / 4).Distinct().ToArray();
        //        Array.Sort(Straight); Array.Sort(st1); Array.Sort(st2); Array.Sort(st3); Array.Sort(st4);
        //        #endregion
        //        for (i = 0; i < 16; i++)
        //        {
        //            if (Reserve[i] == int.Parse(Holder[c1].Tag.ToString()) && Reserve[i + 1] == int.Parse(Holder[c2].Tag.ToString()))
        //            {
        //                //Pair from Hand current = 1

        //                rPairFromHand(ref current, ref Power);

        //                #region Pair or Two Pair from Table current = 2 || 0
        //                rPairTwoPair(ref current, ref Power);
        //                #endregion

        //                #region Two Pair current = 2
        //                rTwoPair(ref current, ref Power);
        //                #endregion

        //                #region Three of a kind current = 3
        //                rThreeOfAKind(ref current, ref Power, Straight);
        //                #endregion

        //                #region Straight current = 4
        //                rStraight(ref current, ref Power, Straight);
        //                #endregion

        //                #region Flush current = 5 || 5.5
        //                rFlush(ref current, ref Power, ref vf, Straight1);
        //                #endregion

        //                #region Full House current = 6
        //                rFullHouse(ref current, ref Power, ref done, Straight);
        //                #endregion

        //                #region Four of a Kind current = 7
        //                rFourOfAKind(ref current, ref Power, Straight);
        //                #endregion

        //                #region Straight Flush current = 8 || 9
        //                rStraightFlush(ref current, ref Power, st1, st2, st3, st4);
        //                #endregion

        //                #region High Card current = -1
        //                rHighCard(ref current, ref Power);
        //                #endregion
        //            }
        //        }
        //    }
        //}
    }
}
