namespace Poker.Interfaces
{
    using Poker.Enums;

    public interface IPokerManager : ICardHolder
    {
        GameStateType CurrentGameState { get; set; }
    }
}
