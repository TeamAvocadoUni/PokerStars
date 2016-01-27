

namespace Poker.Interfaces
{
    using System.Threading.Tasks;

    public interface IGameEngine
    {
        void Run();

        Task Shuffle();

        void Rules(ICharacter character);
    }
}