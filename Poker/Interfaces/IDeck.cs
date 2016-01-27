using Poker.Models;

namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDeck
    {
        IList<Card> GetAllCards();

        ICard GetCardAtPosition(int position);

        Task SetAllCards(IList<ICharacter> players, IPokerManager pokerManager);
    }
}
