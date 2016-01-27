namespace Poker.Events
{
    using Poker.Enums;

    public delegate void EngineStateEvent(object sender, EngineEvent args);

    public class EngineEvent
    {
        public EngineEvent(EngineStateType gameState)
        {
            this.GameState = gameState;
        }

        public EngineStateType GameState { get; set; }
    }
}
