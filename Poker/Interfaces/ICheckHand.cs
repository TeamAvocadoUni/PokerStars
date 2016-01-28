namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using Models;

    public interface ICheckHand
    {
        void CheckStraightFlush(ICharacter player, int[] spades, int[] hearts, int[] diamonds, int[] clubs,
            ref List<Type> strongestHands, ref Type winningHand);

        void CheckFourOfAKind(ICharacter player, int[] cards, ref List<Type> strongestHands, ref Type winningHand);

        void CheckFullHouse(ICharacter player, ref bool done, int[] cards, ref List<Type> strongestHands,
            ref Type winningHand);

        void CheckFlush(ICharacter player, ref bool validFlush, int[] cards, ref List<Type> strongestHands,
            ref Type winningHand, IList<Card> reserve, int i);

        void CheckStraight(ICharacter player, int[] cards, ref List<Type> strongestHands, ref Type winningHand);

        void CheckThreeOfAKind(ICharacter player, int[] straight, ref List<Type> strongestHands, ref Type winningHand);

        void CheckTwoPair(ICharacter player, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve,
            int i);

        void CheckPairTwoPair(ICharacter player, ref List<Type> strongestHands, ref Type winningHand,
            IList<Card> reserve, int i);

        void CheckPairFromHand(ICharacter player, ref List<Type> strongestHands, ref Type winningHand,
            IList<Card> reserve, int i);

        void CheckHighCard(ICharacter player, ref List<Type> strongestHands, ref Type winningHand, IList<Card> reserve,
            int i);
    }
}
