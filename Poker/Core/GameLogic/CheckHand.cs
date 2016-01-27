using System.Collections.Generic;
using System.Linq;
using Poker.Enums;
using Poker.Interfaces;
using Poker.Models;

namespace Poker.Core.GameLogic
{
    public class CheckHand
    {
        public void CheckStraightFlush(ICharacter player, int[] spades, int[] hearts, int[] diamonds, int[] clubs, ref List<Type> strongestHands, ref Type winningHand)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                if (spades.Length >= 5)
                {
                    if (spades[0] + 4 == spades[4])
                    {
                        player.CharacterType.Current = PokerHand.StraightFlush;
                        player.CharacterType.Power = spades.Max() / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (spades[0] == 0 && spades[1] == 9 && spades[2] == 10 && spades[3] == 11 && spades[0] + 12 == spades[4])
                    {
                        player.CharacterType.Current = PokerHand.RoyalFlush;
                        player.CharacterType.Power = spades.Max() / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }

                if (hearts.Length >= 5)
                {
                    if (hearts[0] + 4 == hearts[4])
                    {
                        player.CharacterType.Current = PokerHand.StraightFlush;
                        player.CharacterType.Power = hearts.Max() / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (hearts[0] == 0 && hearts[1] == 9 && hearts[2] == 10 && hearts[3] == 11 && hearts[0] + 12 == hearts[4])
                    {
                        player.CharacterType.Current = PokerHand.RoyalFlush;
                        player.CharacterType.Power = (hearts.Max()) / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }

                if (diamonds.Length >= 5)
                {
                    if (diamonds[0] + 4 == diamonds[4])
                    {
                        player.CharacterType.Current = PokerHand.StraightFlush;
                        player.CharacterType.Power = (diamonds.Max()) / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = strongestHands
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (diamonds[0] == 0 && diamonds[1] == 9 && diamonds[2] == 10 && diamonds[3] == 11 && diamonds[0] + 12 == diamonds[4])
                    {
                        player.CharacterType.Current = PokerHand.RoyalFlush;
                        player.CharacterType.Power = (diamonds.Max()) / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }

                if (clubs.Length >= 5)
                {
                    if (clubs[0] + 4 == clubs[4])
                    {
                        player.CharacterType.Current = PokerHand.StraightFlush;
                        player.CharacterType.Power = (clubs.Max()) / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (clubs[0] == 0 && clubs[1] == 9 && clubs[2] == 10 && clubs[3] == 11 && clubs[0] + 12 == clubs[4])
                    {
                        player.CharacterType.Current = PokerHand.RoyalFlush;
                        player.CharacterType.Power = clubs.Max() / 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }
            }
        }

        public void CheckFourOfAKind(ICharacter player, int[] cards, ref List<Type> strongestHands, ref Type winningHand)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (cards[i] / 4 == cards[i + 1] / 4 && cards[i] / 4 == cards[i + 2] / 4 &&
                        cards[i] / 4 == cards[i + 3] / 4)
                    {
                        player.CharacterType.Current = PokerHand.FourOfAKind;
                        player.CharacterType.Power = (cards[i] / 4) * 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (cards[i] / 4 == 0 && cards[i + 1] / 4 == 0 && cards[i + 2] / 4 == 0 && cards[i + 3] / 4 == 0)
                    {
                        player.CharacterType.Current = PokerHand.FourOfAKind;
                        player.CharacterType.Power = 13 * 4 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }
            }
        }

        public void CheckFullHouse(ICharacter player, ref bool done, int[] cards, ref List<Type> strongestHands, ref Type winningHand)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                double type = player.CharacterType.Power;

                for (int i = 0; i <= 12; i++)
                {
                    var fullHouse = cards.Where(c => c / 4 == i).ToArray();

                    if (fullHouse.Length == 3 || done)
                    {
                        if (fullHouse.Length == 2)
                        {
                            if (fullHouse.Max() / 4 == 0)
                            {
                                player.CharacterType.Current = PokerHand.FullHouse;
                                player.CharacterType.Power = 13 * 2 + player.CharacterType.Current * 100;

                                strongestHands.Add(new Type()
                                {
                                    Power = player.CharacterType.Power,
                                    Current = player.CharacterType.Current
                                });

                                winningHand = GetWinningHand(strongestHands);
                                break;
                            }

                            if (fullHouse.Max() / 4 > 0)
                            {
                                player.CharacterType.Current = PokerHand.FullHouse;
                                player.CharacterType.Power = fullHouse.Max() / 4 * 2 + player.CharacterType.Current * 100;

                                strongestHands.Add(new Type()
                                {
                                    Power = player.CharacterType.Power,
                                    Current = player.CharacterType.Current
                                });

                                winningHand = GetWinningHand(strongestHands);
                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fullHouse.Max() / 4 == 0)
                            {
                                player.CharacterType.Power = 13;
                                done = true;
                                i = -1;
                            }
                            else
                            {
                                player.CharacterType.Power = fullHouse.Max() / 4;
                                done = true;
                                i = -1;
                            }
                        }
                    }
                }

                if (player.CharacterType.Current != PokerHand.FullHouse)
                {
                    player.CharacterType.Power = type;
                }
            }
        }

        public void CheckFlush(ICharacter player, ref bool validFlush, int[] cards, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve, int i)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                var spadesFlush = cards.Where(s => s % 4 == 0).ToArray();
                var heartsFlush = cards.Where(s => s % 4 == 1).ToArray();
                var diamondsFlush = cards.Where(s => s % 4 == 2).ToArray();
                var clubsFlush = cards.Where(s => s % 4 == 3).ToArray();

                if (spadesFlush.Length == 3 || spadesFlush.Length == 4)
                {
                    if (reserve[i].CardPower % 4 == reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == spadesFlush[0] % 4)
                    {
                        if (reserve[i].CardPower / 4 > spadesFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i + 1].CardPower / 4 > spadesFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i].CardPower / 4 < spadesFlush.Max() / 4 && reserve[i + 1].CardPower / 4 < spadesFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = spadesFlush.Max() + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                    }
                }

                if (spadesFlush.Length == 4)//different cards in hand
                {
                    if (reserve[i].CardPower % 4 != reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == spadesFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i].CardPower / 4 > spadesFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                        else
                        {
                            player.CharacterType.Power = spadesFlush.Max() + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                    }

                    if (reserve[i + 1].CardPower % 4 != reserve[i].CardPower % 4 && reserve[i + 1].CardPower % 4 == spadesFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i + 1].CardPower / 4 > spadesFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                        else
                        {
                            player.CharacterType.Power = spadesFlush.Max() + player.CharacterType.Current * 100;
                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                    }
                }

                if (spadesFlush.Length == 5)
                {
                    if (reserve[i].CardPower % 4 == spadesFlush[0] % 4 && reserve[i].CardPower / 4 > spadesFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 == spadesFlush[0] % 4 && reserve[i + 1].CardPower / 4 > spadesFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i].CardPower / 4 < spadesFlush.Min() / 4 && reserve[i + 1].CardPower / 4 < spadesFlush.Min())
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = spadesFlush.Max() + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }

                if (heartsFlush.Length == 3 || heartsFlush.Length == 4)
                {
                    if (reserve[i].CardPower % 4 == reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == heartsFlush[0] % 4)
                    {
                        if (reserve[i].CardPower / 4 > heartsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i + 1].CardPower / 4 > heartsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i].CardPower / 4 < heartsFlush.Max() / 4 && reserve[i + 1].CardPower / 4 < heartsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = heartsFlush.Max() + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                    }
                }

                if (heartsFlush.Length == 4)//different cards in hand
                {
                    if (reserve[i].CardPower % 4 != reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == heartsFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i].CardPower / 4 > heartsFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = heartsFlush.Max() + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 != reserve[i].CardPower % 4 && reserve[i + 1].CardPower % 4 == heartsFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i + 1].CardPower / 4 > heartsFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = heartsFlush.Max() + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }

                if (heartsFlush.Length == 5)
                {
                    if (reserve[i].CardPower % 4 == heartsFlush[0] % 4 && reserve[i].CardPower / 4 > heartsFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;
                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 == heartsFlush[0] % 4 && reserve[i + 1].CardPower / 4 > heartsFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;
                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i].CardPower / 4 < heartsFlush.Min() / 4 && reserve[i + 1].CardPower / 4 < heartsFlush.Min())
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = heartsFlush.Max() + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }

                if (diamondsFlush.Length == 3 || diamondsFlush.Length == 4)
                {
                    if (reserve[i].CardPower % 4 == reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == diamondsFlush[0] % 4)
                    {
                        if (reserve[i].CardPower / 4 > diamondsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i + 1].CardPower / 4 > diamondsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i].CardPower / 4 < diamondsFlush.Max() / 4 && reserve[i + 1].CardPower / 4 < diamondsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = diamondsFlush.Max() + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                    }
                }

                if (diamondsFlush.Length == 4)//different cards in hand
                {
                    if (reserve[i].CardPower % 4 != reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == diamondsFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i].CardPower / 4 > diamondsFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = diamondsFlush.Max() + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 != reserve[i].CardPower % 4 && reserve[i + 1].CardPower % 4 == diamondsFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i + 1].CardPower / 4 > diamondsFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = diamondsFlush.Max() + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }

                if (diamondsFlush.Length == 5)
                {
                    if (reserve[i].CardPower % 4 == diamondsFlush[0] % 4 && reserve[i].CardPower / 4 > diamondsFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 == diamondsFlush[0] % 4 && reserve[i + 1].CardPower / 4 > diamondsFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i].CardPower / 4 < diamondsFlush.Min() / 4 && reserve[i + 1].CardPower / 4 < diamondsFlush.Min())
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = diamondsFlush.Max() + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }

                if (clubsFlush.Length == 3 || clubsFlush.Length == 4)
                {
                    if (reserve[i].CardPower % 4 == reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == clubsFlush[0] % 4)
                    {
                        if (reserve[i].CardPower / 4 > clubsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i + 1].CardPower / 4 > clubsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }

                        if (reserve[i].CardPower / 4 < clubsFlush.Max() / 4 && reserve[i + 1].CardPower / 4 < clubsFlush.Max() / 4)
                        {
                            player.CharacterType.Current = PokerHand.Flush;
                            player.CharacterType.Power = clubsFlush.Max() + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                            validFlush = true;
                        }
                    }
                }

                if (clubsFlush.Length == 4)//different cards in hand
                {
                    if (reserve[i].CardPower % 4 != reserve[i + 1].CardPower % 4 && reserve[i].CardPower % 4 == clubsFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i].CardPower / 4 > clubsFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = clubsFlush.Max() + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 != reserve[i].CardPower % 4 && reserve[i + 1].CardPower % 4 == clubsFlush[0] % 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        if (reserve[i + 1].CardPower / 4 > clubsFlush.Max() / 4)
                        {
                            player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = clubsFlush.Max() + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }

                if (clubsFlush.Length == 5)
                {
                    if (reserve[i].CardPower % 4 == clubsFlush[0] % 4 && reserve[i].CardPower / 4 > clubsFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i].CardPower + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i + 1].CardPower % 4 == clubsFlush[0] % 4 && reserve[i + 1].CardPower / 4 > clubsFlush.Min() / 4)
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = reserve[i + 1].CardPower + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }

                    if (reserve[i].CardPower / 4 < clubsFlush.Min() / 4 && reserve[i + 1].CardPower / 4 < clubsFlush.Min())
                    {
                        player.CharacterType.Current = PokerHand.Flush;
                        player.CharacterType.Power = clubsFlush.Max() + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                        validFlush = true;
                    }
                }
                //ace
                if (spadesFlush.Length > 0)
                {
                    if (reserve[i].CardPower / 4 == 0 && reserve[i].CardPower % 4 == spadesFlush[0] % 4 && validFlush && spadesFlush.Length > 0)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (reserve[i + 1].CardPower / 4 == 0 && reserve[i + 1].CardPower % 4 == spadesFlush[0] % 4 && validFlush && spadesFlush.Length > 0)
                    {
                        player.CharacterType.Current = 5.5;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }

                if (heartsFlush.Length > 0)
                {
                    if (reserve[i].CardPower / 4 == 0 && reserve[i].CardPower % 4 == heartsFlush[0] % 4 && validFlush && heartsFlush.Length > 0)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (reserve[i + 1].CardPower / 4 == 0 && reserve[i + 1].CardPower % 4 == heartsFlush[0] % 4 && validFlush && heartsFlush.Length > 0)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }

                if (diamondsFlush.Length > 0)
                {
                    if (reserve[i].CardPower / 4 == 0 && reserve[i].CardPower % 4 == diamondsFlush[0] % 4 && validFlush && diamondsFlush.Length > 0)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (reserve[i + 1].CardPower / 4 == 0 && reserve[i + 1].CardPower % 4 == diamondsFlush[0] % 4 && validFlush && diamondsFlush.Length > 0)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }

                if (clubsFlush.Length > 0)
                {
                    if (reserve[i].CardPower / 4 == 0 && reserve[i].CardPower % 4 == clubsFlush[0] % 4 && validFlush && clubsFlush.Length > 0)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    if (reserve[i + 1].CardPower / 4 == 0 && reserve[i + 1].CardPower % 4 == clubsFlush[0] % 4 && validFlush)
                    {
                        player.CharacterType.Current = PokerHand.FlushWithAce;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }
            }
        }

        public void CheckStraight(ICharacter player, int[] cards, ref List<Type> strongestHands, ref Type winningHand)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                var straight = cards.Select(c => c / 4).Distinct().ToArray();

                for (int i = 0; i < straight.Length - 4; i++)
                {
                    if (straight[i] + 4 == straight[i + 4])
                    {
                        if (straight.Max() - 4 == straight[i])
                        {
                            player.CharacterType.Current = PokerHand.Straigth;
                            player.CharacterType.Power = straight.Max() + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                        }
                        else
                        {
                            player.CharacterType.Current = PokerHand.Straigth;
                            player.CharacterType.Power = straight[i + 4] + player.CharacterType.Current * 100;

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                        }
                    }

                    if (straight[i] == 0 && straight[i + 1] == 9 && straight[i + 2] == 10 && straight[i + 3] == 11 && straight[i + 4] == 12)
                    {
                        player.CharacterType.Current = PokerHand.Straigth;
                        player.CharacterType.Power = 13 + player.CharacterType.Current * 100;

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }
            }
        }

        public void CheckThreeOfAKind(ICharacter player, int[] straight, ref List<Type> strongestHands, ref Type winningHand)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                for (int i = 0; i <= 12; i++)
                {
                    var threeOfAKind = straight.Where(o => o / 4 == i).ToArray();

                    if (threeOfAKind.Length == 3)
                    {
                        player.CharacterType.Current = PokerHand.ThreeOfAKind;

                        if (threeOfAKind.Max() / 4 == 0)
                        {
                            player.CharacterType.Power = 13 * 3 + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = threeOfAKind[0] / 4 + threeOfAKind[1] / 4 + threeOfAKind[2] / 4 + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }
                }
            }
        }

        public void CheckTwoPair(ICharacter player, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve, int i)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                bool msgbox = false;

                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;

                    if (reserve[i].CardPower / 4 != reserve[i + 1].CardPower / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }

                            if (tc - k >= 12)
                            {
                                if (reserve[i].CardPower / 4 == reserve[tc].CardPower / 4 && reserve[i + 1].CardPower / 4 == reserve[tc - k].CardPower / 4 ||
                                    reserve[i + 1].CardPower / 4 == reserve[tc].CardPower / 4 && reserve[i].CardPower / 4 == reserve[tc - k].CardPower / 4)
                                {
                                    if (!msgbox)
                                    {
                                        if (reserve[i].CardPower / 4 == 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = 13 * 4 + (reserve[i + 1].CardPower / 4) * 2 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }

                                        if (reserve[i + 1].CardPower / 4 == 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = 13 * 4 + (reserve[i].CardPower / 4) * 2 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }

                                        if (reserve[i + 1].CardPower / 4 != 0 && reserve[i].CardPower / 4 != 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = (reserve[i].CardPower / 4) * 2 + (reserve[i + 1].CardPower / 4) * 2 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }
                                    }

                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckPairTwoPair(ICharacter player, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve, int i)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                bool msgbox = false;
                bool msgbox1 = false;

                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;

                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }

                        if (tc - k >= 12)
                        {
                            if (reserve[tc].CardPower / 4 == reserve[tc - k].CardPower / 4)
                            {
                                if (reserve[tc].CardPower / 4 != reserve[i].CardPower / 4 && reserve[tc].CardPower / 4 != reserve[i + 1].CardPower / 4 && player.CharacterType.Current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (reserve[i + 1].CardPower / 4 == 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = (reserve[i].CardPower / 4) * 2 + 13 * 4 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }

                                        if (reserve[i].CardPower / 4 == 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = (reserve[i + 1].CardPower / 4) * 2 + 13 * 4 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }

                                        if (reserve[i + 1].CardPower / 4 != 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = (reserve[tc].CardPower / 4) * 2 + (reserve[i + 1].CardPower / 4) * 2 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }

                                        if (reserve[i].CardPower / 4 != 0)
                                        {
                                            player.CharacterType.Current = PokerHand.TwoPair;
                                            player.CharacterType.Power = (reserve[tc].CardPower / 4) * 2 + (reserve[i].CardPower / 4) * 2 + player.CharacterType.Current * 100;

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = player.CharacterType.Current
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }
                                    }

                                    msgbox = true;
                                }

                                if (player.CharacterType.Current == PokerHand.HighCard)
                                {
                                    if (!msgbox1)
                                    {
                                        if (reserve[i].CardPower / 4 > reserve[i + 1].CardPower / 4)
                                        {
                                            player.CharacterType.Current = PokerHand.PairTable;
                                            if (reserve[tc].CardPower / 4 == 0)
                                            {
                                                player.CharacterType.Power = 13 + reserve[i].CardPower / 4 + player.CharacterType.Current * 100;
                                            }
                                            else
                                            {
                                                player.CharacterType.Power = reserve[tc].CardPower / 4 + reserve[i].CardPower / 4 + player.CharacterType.Current * 100;
                                            }

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = 1 //??
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }
                                        else
                                        {
                                            player.CharacterType.Current = PokerHand.PairTable;

                                            if (reserve[tc].CardPower / 4 == 0)
                                            {
                                                player.CharacterType.Power = 13 + reserve[i + 1].CardPower + player.CharacterType.Current * 100;
                                            }
                                            else
                                            {
                                                player.CharacterType.Power = reserve[tc].CardPower / 4 + reserve[i + 1].CardPower / 4 + player.CharacterType.Current * 100;
                                            }

                                            strongestHands.Add(new Type()
                                            {
                                                Power = player.CharacterType.Power,
                                                Current = 1 //??
                                            });

                                            winningHand = GetWinningHand(strongestHands);
                                        }
                                    }

                                    msgbox1 = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void CheckPairFromHand(ICharacter player, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve, int i)
        {
            if (player.CharacterType.Current >= PokerHand.HighCard)
            {
                bool msgbox = false;

                if (reserve[i].CardPower / 4 == reserve[i + 1].CardPower / 4)
                {
                    if (!msgbox)
                    {
                        player.CharacterType.Current = PokerHand.PairFromHand;

                        if (reserve[i].CardPower / 4 == 0)
                        {
                            player.CharacterType.Power = 13 * 4 + player.CharacterType.Current * 100;
                        }
                        else
                        {
                            player.CharacterType.Power = (reserve[i + 1].CardPower / 4) * 4 + player.CharacterType.Current * 100;
                        }

                        strongestHands.Add(new Type()
                        {
                            Power = player.CharacterType.Power,
                            Current = player.CharacterType.Current
                        });

                        winningHand = GetWinningHand(strongestHands);
                    }

                    msgbox = true;
                }

                for (int tc = 16; tc >= 12; tc--)
                {
                    if (reserve[i + 1].CardPower / 4 == reserve[tc].CardPower / 4)
                    {
                        if (!msgbox)
                        {
                            player.CharacterType.Current = PokerHand.PairFromHand;
                            if (reserve[i + 1].CardPower / 4 == 0)
                            {
                                player.CharacterType.Power = 13 * 4 + reserve[i].CardPower / 4 + player.CharacterType.Current * 100;
                            }
                            else
                            {
                                player.CharacterType.Power = (reserve[i + 1].CardPower / 4) * 4 + reserve[i].CardPower / 4 + player.CharacterType.Current * 100;
                            }

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                        }

                        msgbox = true;
                    }

                    if (reserve[i].CardPower / 4 == reserve[tc].CardPower / 4)
                    {
                        if (!msgbox)
                        {
                            player.CharacterType.Current = PokerHand.PairFromHand;

                            if (reserve[i].CardPower / 4 == 0)
                            {
                                player.CharacterType.Power = 13 * 4 + reserve[i + 1].CardPower / 4 + player.CharacterType.Current * 100;
                            }
                            else
                            {
                                player.CharacterType.Power = (reserve[tc].CardPower / 4) * 4 + reserve[i + 1].CardPower / 4 + player.CharacterType.Current * 100;
                            }

                            strongestHands.Add(new Type()
                            {
                                Power = player.CharacterType.Power,
                                Current = player.CharacterType.Current
                            });

                            winningHand = GetWinningHand(strongestHands);
                        }

                        msgbox = true;
                    }
                }
            }
        }

        public void CheckHighCard(ICharacter player, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve, int i)
        {
            if (player.CharacterType.Current == PokerHand.HighCard)
            {
                if (reserve[i].CardPower / 4 == 0 || reserve[i + 1].CardPower / 4 == 0)
                {
                    player.CharacterType.Power = 13;
                }
                else if (reserve[i].CardPower / 4 > reserve[i + 1].CardPower / 4)
                {
                    player.CharacterType.Power = reserve[i].CardPower / 4;
                }
                else
                {
                    player.CharacterType.Power = reserve[i + 1].CardPower / 4;
                }

                strongestHands.Add(new Type()
                {
                    Power = player.CharacterType.Power,
                    Current = player.CharacterType.Current
                });

                winningHand = GetWinningHand(strongestHands);
            }
        }

        private static Type GetWinningHand(List<Type> strongestHands)
        {
            Type winningHand = strongestHands
                .OrderByDescending(h => h.Current)
                .ThenByDescending(h => h.Power)
                .First();

            return winningHand;
        }
    }
}
