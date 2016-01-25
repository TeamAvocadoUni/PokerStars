using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Windows.Forms;

namespace Poker.Models
{
    public abstract class Character : GameObject
    {
        protected Character(Panel panel, int chips, bool folded, int call, int raise, double power, double type, bool turn, bool foldTurn)
        {
            this.Panel = panel;
            this.Chips = chips;
            this.Folded = folded;
            this.Call = call;
            this.Raise = raise;
            this.Power = power;
            this.Type = type;
            this.Turn = turn;
            this.FoldTurn = foldTurn;
        }

        public int Chips { get; set; }

        public double Power { get; set; }

        public bool Turn { get; set; }

        public bool FoldTurn { get; set; }

        public double Type { get; set; }

        public bool Folded { get; set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        void Rules(int c1, int c2, string currentText, double current, double Power, bool foldedTurn, int[] reserve, PictureBox[] holder, List<Type> win, Type sorted)
        {
            if (!foldedTurn || c1 == 0 && c2 == 1 && this.Text.Contains("Fold") == false)
            {
                #region Variables
                bool done = false, vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = reserve[c1];
                Straight[1] = reserve[c2];
                Straight1[0] = Straight[2] = reserve[12];
                Straight1[1] = Straight[3] = reserve[13];
                Straight1[2] = Straight[4] = reserve[14];
                Straight1[3] = Straight[5] = reserve[15];
                Straight1[4] = Straight[6] = reserve[16];
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);
                #endregion
                for (int i = 0; i < 16; i++)
                {
                    if (reserve[i] == int.Parse(holder[c1].Tag.ToString()) && reserve[i + 1] == int.Parse(holder[c2].Tag.ToString()))
                    {
                        rPairFromHand(current, Power, reserve, i, win, sorted);

                        //#region Pair or Two Pair from Table current = 2 || 0
                        //rPairTwoPair(current, Power);
                        //#endregion

                        //#region Two Pair current = 2
                        //rTwoPair(current, Power);
                        //#endregion

                        //#region Three of a kind current = 3
                        //rThreeOfAKind(current, Power, Straight);
                        //#endregion

                        //#region Straight current = 4
                        //rStraight(current, Power, Straight);
                        //#endregion

                        //#region Flush current = 5 || 5.5
                        //rFlush(current, Power, vf, Straight1);
                        //#endregion

                        //#region Full House current = 6
                        //rFullHouse(current, Power, done, Straight);
                        //#endregion

                        //#region Four of a Kind current = 7
                        //rFourOfAKind(current, Power, Straight);
                        //#endregion

                        //#region Straight Flush current = 8 || 9
                        //rStraightFlush(current, Power, st1, st2, st3, st4);
                        //#endregion

                        //#region High Card current = -1
                        //rHighCard(current, Power);
                        //#endregion
                    }
                }
            }
        }

        private void rPairFromHand(double current, double Power, int[] reserve, int i, List<Type> win, Type sorted)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (reserve[i] / 4 == reserve[i + 1] / 4)
                {
                    if (!msgbox)
                    {
                        if (reserve[i] / 4 == 0)
                        {
                            current = 1;
                            Power = 13 * 4 + current * 100;
                            win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (reserve[i + 1] / 4) * 4 + current * 100;
                            win.Add(new Type() { Power = Power, Current = 1 });
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (reserve[i + 1] / 4 == reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i + 1] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + reserve[i] / 4 + current * 100;
                                win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (reserve[i + 1] / 4) * 4 + reserve[i] / 4 + current * 100;
                                win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (reserve[i] / 4 == reserve[tc] / 4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i] / 4 == 0)
                            {
                                current = 1;
                                Power = 13 * 4 + reserve[i + 1] / 4 + current * 100;
                                win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (reserve[tc] / 4) * 4 + reserve[i + 1] / 4 + current * 100;
                                win.Add(new Type() { Power = Power, Current = 1 });
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }
    }
}
