namespace Poker.Interfaces
{
    using Enums;

    public interface IHandPower
    {
        void HighCard(ICharacter character, int call, ISingleBet bet, int raise);

        void PairTable(ICharacter character, int call, ISingleBet bet, int raise);

        void PairHand(ICharacter character, int call, ISingleBet bet, int raise, GameStateType state);

        void TwoPair(ICharacter character, int call, ISingleBet bet, int raise, GameStateType state);

        void ThreeOfAKind(ICharacter character, int call, ISingleBet bet, int raise);

        void Straight(ICharacter character, int call, ISingleBet bet, int raise);

        void Flush(ICharacter character, int call, ISingleBet bet, int raise);

        void FullHouse(ICharacter character, int call, ISingleBet bet, int raise);

        void FourOfAKind(ICharacter character, int call, ISingleBet bet, int raise);

        void StraightFlush(ICharacter character, int call, ISingleBet bet, int raise);
    }
}
