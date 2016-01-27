using System;
using Poker.Enums;
using Poker.Interfaces;
using Poker.Utils;

namespace Poker.Core.GameLogic
{
    public class HandClassificator
    {
        private readonly GameActions actions;
        private readonly Random random;

        public HandClassificator()
        {
            this.actions = new GameActions();
            this.random = new Random();
        }

        public void HP(ICharacter character, int n, int n1, int call, ISingleBet pot,ref int raise,ref bool raising)
        {
            int randomInteger = this.random.Next(1, 4);

            if (call <= 0)
            {
                this.actions.CheckAction(character);
            }
            else
            { 
                if (randomInteger == 1)
                {
                    if (call <= CalculationsHelper.RoundNumber(character.Chips, n))
                    {
                        this.actions.CallAction(character, call, pot);
                    }
                    else
                    {
                        this.actions.FoldAction(character);
                    }
                }

                if (randomInteger == 2)
                {
                    if (call <= CalculationsHelper.RoundNumber(character.Chips, n1))
                    {
                        this.actions.CallAction(character, call, pot);
                    }
                    else
                    {
                        this.actions.FoldAction(character);
                    }
                }
            }

            if (randomInteger == 3)
            {
                if (raise == 0)
                {
                    raise = call * 2;
                    this.actions.RaiseAction(character, raise, pot);
                }
                else
                {
                    if (raise <= CalculationsHelper.RoundNumber(character.Chips, n))
                    {
                        raise = call * 2;
                        this.actions.RaiseAction(character, raise, pot);
                    }
                    else
                    {
                        this.actions.FoldAction(character);
                    }
                }
            }

            if (character.Chips <= 0)
            {
                character.FoldTurn = true;
            }
        }

        public void PH(ICharacter character, int n, int n1, int r, int call, ISingleBet bet, ref int raise,ref bool raising, GameStateType state)
        {
            int rnd = this.random.Next(1, 3);

            if (state < GameStateType.Turn)
            {
                if (call <= 0)
                {
                    this.actions.CheckAction(character);
                }

                if (call > 0)
                {
                    if (call >= CalculationsHelper.RoundNumber(character.Chips, n1))
                    {
                        this.actions.FoldAction(character);
                    }

                    if (raise > CalculationsHelper.RoundNumber(character.Chips, n))
                    {
                        this.actions.FoldAction(character);
                    }

                    if (!character.FoldTurn)
                    {
                        if (call >= CalculationsHelper.RoundNumber(character.Chips, n) && call <= CalculationsHelper.RoundNumber(character.Chips, n1))
                        {
                            this.actions.CallAction(character, call, bet);
                        }

                        if (raise <= CalculationsHelper.RoundNumber(character.Chips, n) && raise >= CalculationsHelper.RoundNumber(character.Chips, n) / 2)
                        {
                            this.actions.CallAction(character, call, bet);
                        }

                        if (raise <= CalculationsHelper.RoundNumber(character.Chips, n) / 2)
                        {
                            if (raise > 0)
                            {
                                raise = (int)CalculationsHelper.RoundNumber(character.Chips, n);
                                this.actions.CallAction(character, call, bet);
                            }
                            else
                            {
                                raise = call * 2;
                                this.actions.CallAction(character, call, bet);
                            }
                        }
                    }
                }
            }

            if (state >= GameStateType.Turn)
            {
                if (call > 0)
                {
                    if (call >= CalculationsHelper.RoundNumber(character.Chips, n1 - rnd))
                    {
                        this.actions.FoldAction(character);
                    }

                    if (raise > CalculationsHelper.RoundNumber(character.Chips, n - rnd))
                    {
                        this.actions.FoldAction(character);
                    }

                    if (!character.FoldTurn)
                    {
                        if (call >= CalculationsHelper.RoundNumber(character.Chips, n - rnd) && call <= CalculationsHelper.RoundNumber(character.Chips, n1 - rnd))
                        {
                            this.actions.CallAction(character, call, bet);
                        }

                        if (raise <= CalculationsHelper.RoundNumber(character.Chips, n - rnd) && raise >= CalculationsHelper.RoundNumber(character.Chips, n - rnd) / 2)
                        {
                            this.actions.CallAction(character, call, bet);
                        }

                        if (raise <= CalculationsHelper.RoundNumber(character.Chips, n - rnd) / 2)
                        {
                            if (raise > 0)
                            {
                                raise = (int)CalculationsHelper.RoundNumber(character.Chips, n - rnd);
                                this.actions.RaiseAction(character, raise, bet);
                            }
                            else
                            {
                                raise = call * 2;
                                this.actions.RaiseAction(character, raise, bet);
                            }
                        }
                    }
                }

                if (call <= 0)
                {
                    raise = (int)CalculationsHelper.RoundNumber(character.Chips, r - rnd);
                    this.actions.RaiseAction(character, raise, bet);
                }
            }

            if (character.Chips <= 0)
            {
                character.FoldTurn = true;
            }
        }

        public void Smooth(ICharacter character, int n, int r, int call, ISingleBet pot, int raise, ref bool raising)
        {
            if (call <= 0)
            {
                this.actions.CheckAction(character);
            }
            else
            {
                if (call >= CalculationsHelper.RoundNumber(character.Chips, n))
                {
                    if (character.Chips > call)
                    {
                        this.actions.CallAction(character, call, pot);
                    }
                    else if (character.Chips <= call)
                    {
                        character.HasRaised = false;
                        character.IsInTurn = false;
                        character.Chips = 0;
                        character.CharacterStatus.Text = "Call " + character.Chips;
                        pot.AddBet(character.Chips);
                    }
                }
                else
                {
                    if (raise > 0)
                    {
                        if (character.Chips >= raise * 2)
                        {
                            raise *= 2;
                            this.actions.RaiseAction(character, raise, pot);
                        }
                        else
                        {
                            this.actions.CallAction(character, call, pot);
                        }
                    }
                    else
                    {
                        raise = call * 2;
                        this.actions.RaiseAction(character, raise, pot);
                    }
                }
            }

            if (character.Chips <= 0)
            {
                character.FoldTurn = true;
            }
        }
    }
}
