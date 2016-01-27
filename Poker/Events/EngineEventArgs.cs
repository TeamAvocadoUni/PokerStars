namespace Poker.Events
{
    using Poker.Enums;

    public delegate void EngineStateEvent(object sender, EngineEventArgs args);

    public class EngineEventArgs
    {
        public EngineEventArgs(EngineStateType gameState)
        {
            this.GameState = gameState;
        }

        public EngineStateType GameState { get; set; }
    }
}
