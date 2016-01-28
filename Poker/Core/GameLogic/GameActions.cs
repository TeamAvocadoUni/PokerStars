namespace Poker.Core.GameLogic
{
    using Interfaces;

    public class GameActions 
    {
        public void FoldAction(ICharacter character)
        {
            character.HasRaised = false;
            character.Fold();
        }

        public void CallAction(ICharacter character, int call, ISingleBet bet)
        {
            character.HasRaised = false;
            character.Call(call);
            bet.AddBet(call);
        }

        public void RaiseAction(ICharacter character, int raise, ISingleBet bet)
        {
            character.HasRaised = true;
            character.Raise(raise);
            bet.AddBet(raise);
            bet.LastBet = raise;
        }

        public void CheckAction(ICharacter character)
        {
            character.HasRaised = false;
            character.Check();
        }
    }
}
