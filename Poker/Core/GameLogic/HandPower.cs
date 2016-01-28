using System;
using Poker.Enums;
using Poker.Interfaces;

namespace Poker.Core.GameLogic
{
    public class HandPower
    {
        private readonly HandClassificator type;
        private readonly Random rnd;

        public HandPower()
        {
            this.type = new HandClassificator();
            this.rnd = new Random();
        }

        public void HighCard(ICharacter character, int call, ISingleBet bet, int raise)
        {
            this.type.HP(character, 20, 25, call, bet, raise);
        }

        public void PairTable(ICharacter character, int call, ISingleBet bet, int raise)
        {
            this.type.HP(character, 16, 25, call, bet, raise);
        }

        public void PairHand(ICharacter character, int call, ISingleBet bet, int raise, GameStateType state)
        {
            int randomCall = this.rnd.Next(10, 16);
            int randomRaise = this.rnd.Next(10, 13);

            if (character.CharacterType.Power <= 199 && character.CharacterType.Power >= 140)
            {
                this.type.PH(character, randomCall, 6, randomRaise, call, bet, raise, state);
            }

            if (character.CharacterType.Power <= 139 && character.CharacterType.Power >= 128)
            {
                this.type.PH(character, randomCall, 7, randomRaise, call, bet, raise, state);
            }

            if (character.CharacterType.Power < 128 && character.CharacterType.Power >= 101)
            {
                this.type.PH(character, randomCall, 9, randomRaise, call, bet, raise, state);
            }
        }

        public void TwoPair(ICharacter character, int call, ISingleBet bet, int raise, GameStateType state)
        {
            int randomCall = this.rnd.Next(6, 11);
            int randomRaise = this.rnd.Next(6, 11);

            if (character.CharacterType.Power <= 290 && character.CharacterType.Power >= 246)
            {
                this.type.PH(character, randomCall, 3, randomRaise, call, bet, raise, state);
            }

            if (character.CharacterType.Power <= 244 && character.CharacterType.Power >= 234)
            {
                this.type.PH(character, randomCall, 4, randomRaise, call, bet, raise, state);
            }

            if (character.CharacterType.Power < 234 && character.CharacterType.Power >= 201)
            {
                this.type.PH(character, randomCall, 4, randomRaise, call, bet, raise, state);
            }
        }

        public void ThreeOfAKind(ICharacter character, int call, ISingleBet bet, int raise)
        {
            int randomCall = this.rnd.Next(3, 7);
            int randomRaise = this.rnd.Next(4, 8);

            if (character.CharacterType.Power <= 390 && character.CharacterType.Power >= 330)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }

            if (character.CharacterType.Power <= 327 && character.CharacterType.Power >= 321)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }

            if (character.CharacterType.Power < 321 && character.CharacterType.Power >= 303)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }
        }

        public void Straight(ICharacter character, int call, ISingleBet bet, int raise)
        {
            int randomCall = this.rnd.Next(3, 6);
            int randomRaise = this.rnd.Next(3, 8);

            if (character.CharacterType.Power <= 480 && character.CharacterType.Power >= 410)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }

            if (character.CharacterType.Power <= 409 && character.CharacterType.Power >= 407)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }

            if (character.CharacterType.Power < 407 && character.CharacterType.Power >= 404)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }
        }

        public void Flush(ICharacter character, int call, ISingleBet bet, int raise)
        {
            int randomCall = this.rnd.Next(2, 6);
            int randomRaise = this.rnd.Next(3, 7);

            this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
        }

        public void FullHouse(ICharacter character, int call, ISingleBet bet, int raise)
        {
            int randomhCall = this.rnd.Next(1, 5);
            int randomRaise = this.rnd.Next(2, 6);

            if (character.CharacterType.Power <= 626 && character.CharacterType.Power >= 620)
            {
                this.type.Smooth(character, randomhCall, randomRaise, call, bet, raise);
            }

            if (character.CharacterType.Power < 620 && character.CharacterType.Power >= 602)
            {
                this.type.Smooth(character, randomhCall, randomRaise, call, bet, raise);
            }
        }

        public void FourOfAKind(ICharacter character, int call, ISingleBet bet, int raise)
        {
            int randomCall = this.rnd.Next(1, 4);
            int randomRaise = this.rnd.Next(2, 5);

            if (character.CharacterType.Power <= 752 && character.CharacterType.Power >= 704)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }
        }

        public void StraightFlush(ICharacter character, int call, ISingleBet bet, int raise)
        {
            int randomCall = this.rnd.Next(1, 3);
            int randomRaise = this.rnd.Next(1, 3);

            if (character.CharacterType.Power <= 913 && character.CharacterType.Power >= 804)
            {
                this.type.Smooth(character, randomCall, randomRaise, call, bet, raise);
            }
        }
    }
}
