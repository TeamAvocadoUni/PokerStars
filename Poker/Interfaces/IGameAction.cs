namespace Poker.Interfaces
{
    public interface IGameAction
    {
        void Raise(int chips);

        void Call(int chips);

        void Fold();

        void Check();

        void AllIn();
    }
}
