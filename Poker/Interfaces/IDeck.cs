namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDeck
    {
        IList<ICard> GetAllCards();

        ICard GetCardAtPosition(int position);

        Task SetAllCards(IList<ICharacter> players, IPokerManager pokerManager);
    }
}
