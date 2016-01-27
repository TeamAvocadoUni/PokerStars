namespace Poker.Core
{
    using System.Threading.Tasks;

    using Poker.Events;
    using Poker.Interfaces;

    public class Engine : IGameEngine
    {
        public event EngineStateEvent GameEventState;

        public void Run()
        {
            throw new System.NotImplementedException();
        }

        public Task Shuffle()
        {
            throw new System.NotImplementedException();
        }

        public void Rules(ICharacter character)
        {
            throw new System.NotImplementedException();
        }
    }
}
