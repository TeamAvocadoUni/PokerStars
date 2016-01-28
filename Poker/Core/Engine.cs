using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Poker.Core.GameLogic;
using Poker.Enums;
using Poker.Utils;

namespace Poker.Core
{
    using System.Threading.Tasks;

    using Poker.Events;
    using Poker.Interfaces;

    public class Engine : IGameEngine
    {
        public event EngineStateEvent EngineEvent;

        private HandPower handType = new HandPower();
        private CheckHand checkHandType = new CheckHand(); 
        private ICharacter player;
        private IPokerManager pokerManager;
        private IDeck deck;
        private int raise;
        private IList<ICharacter> bots;
        private bool changed;
        private int raisedTurn = 1;
        private List<Type> strongestHands = new List<Type>(); 
        private bool hasRaisedPlayers;
        private Type winningHand;
        private int turnCount = 0;

        public bool HasRaisedPlayers
        {
            get { return this.hasRaisedPlayers; }
            set { this.hasRaisedPlayers = value; }
        }

        public int BigBlind { get; set; }

        public int SmallBlind { get; set; }

        public ISingleBet Bet{ get; private set; }

        public int Call { get; set; }

        public int Raise { get; set; }

        public IMessage Message { get; set; }

        public Engine(ICharacter player, ICollection<ICharacter> bots, ISingleBet bet, IPokerManager pokerManager, IDeck deck, IMessage message)
        {
            this.player = player;
            this.bots = new List<ICharacter>(bots);
            this.Bet = bet;
            this.pokerManager = pokerManager;
            this.deck = deck;
            this.Message = message;
            this.winningHand = new Type();
            this.BigBlind = GameConstants.DefautBigBlind;
            this.SmallBlind = GameConstants.DefautSmallBlind;
            this.SetDefaultCall();
            this.Raise = 0;
            this.hasRaisedPlayers = false;
        }

        public ICharacter GetHumanPlayer()
        {
            return this.player;
        }

        public async void Run()
        {
            await this.Shuffle();
        }

        Task IGameEngine.Shuffle()
        {
            return Shuffle();
        }

        private void InvokeEngineStateEvent(EngineEventArgs args)
        {
            if (this.EngineEvent != null)
            {
                this.EngineEvent(this, args);
            }
        }

        public IList<ICharacter> GetAllPlayers()
        {
            IList<ICharacter> allPlayers = new List<ICharacter>();
            allPlayers.Add(this.player);
            foreach (ICharacter enemy in this.bots)
            {
                allPlayers.Add(enemy);
            }

            return allPlayers;
        }

        async Task Shuffle()
        {
            this.InvokeEngineStateEvent(new EngineEventArgs(EngineStateType.Shuflling));

            await this.deck.SetAllCards(this.GetAllPlayers(), this.pokerManager);


            if (this.bots.Count(e => !e.IsInGame) == 5)
            {
                DialogResult dialogResult = this.Message.ShowMessageBox(GameConstants.PlayAgainMessage, GameConstants.WinGameMessage, MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Application.Restart();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            this.InvokeEngineStateEvent(new EngineEventArgs(EngineStateType.EndShuffling));

        }

        private void SetDefaultCall()
        {
            this.Call = this.BigBlind;
        }

        private async Task HandleAITurn(ICharacter currentAI, ICharacter nextAI)
        {
            if (!currentAI.FoldTurn)
            {
                if (currentAI.IsInTurn)
                {
                    FixCall(currentAI, 1);
                    FixCall(currentAI, 2);
                    Rules(currentAI);
                    this.Message.OutputMessage(currentAI.Name + GameConstants.PlayerTurnMessage);
                    AI(currentAI);
                    turnCount++;
                    currentAI.IsInTurn = false;
                    nextAI.IsInTurn = true;
                }
            }
            if (currentAI.FoldTurn && !currentAI.HasFolded)
            {
                currentAI.HasFolded = true;
            }
            if (currentAI.FoldTurn || !currentAI.IsInTurn)
            {
                await CheckRaise(currentAI.Id + 1);
                nextAI.IsInTurn = true;
            }
        }

        public async Task Turns()
        {
            #region Rotating
            if (!player.FoldTurn)
            {
                if (player.IsInTurn)
                {
                    FixCall(player, 1);
                    this.InvokeEngineStateEvent(new EngineEventArgs(EngineStateType.PlayerTurn));
                    turnCount++;
                    FixCall(player, 2);
                }
            }
            if (player.FoldTurn || !player.IsInTurn)
            {
                await AllIn();
                await CheckRaise(0);
                this.InvokeEngineStateEvent(new EngineEventArgs(EngineStateType.BotTurn));
                this.bots.First().IsInTurn = true;
                for (int i = 0; i < this.bots.Count; i++)
                {
                    if (this.bots.Count - 1 == i)
                    {
                        await this.HandleAITurn(this.bots[i], this.player);
                    }
                    else
                    {
                        await this.HandleAITurn(this.bots[i], this.bots[i + 1]);
                    }
                }
                #endregion

                await AllIn();
                await Turns();
            }
        }

        private int GetFoldedPlayersCount(ICollection<ICharacter> players)
        {
            return players.Count(p => p.HasFolded == true);
        }

        public void Rules(ICharacter player)
        {
            if (!player.FoldTurn)
            {
                #region Variables

                bool done = false;
                bool vf = false;
                int[] Straight1 = new int[5];
                int[] Straight = new int[7];
                Straight[0] = player.Cards.First().CardPower;
                Straight[1] = player.Cards.Last().CardPower;
                Straight1[0] = Straight[2] = this.pokerManager.Cards.ElementAt(0).CardPower;
                Straight1[1] = Straight[3] = this.pokerManager.Cards.ElementAt(1).CardPower;
                Straight1[2] = Straight[4] = this.pokerManager.Cards.ElementAt(2).CardPower;
                Straight1[3] = Straight[5] = this.pokerManager.Cards.ElementAt(3).CardPower;
                Straight1[4] = Straight[6] = this.pokerManager.Cards.ElementAt(4).CardPower;
                var a = Straight.Where(o => o % 4 == 0).ToArray();
                var b = Straight.Where(o => o % 4 == 1).ToArray();
                var c = Straight.Where(o => o % 4 == 2).ToArray();
                var d = Straight.Where(o => o % 4 == 3).ToArray();
                var st1 = a.Select(o => o / 4).Distinct().ToArray();
                var st2 = b.Select(o => o / 4).Distinct().ToArray();
                var st3 = c.Select(o => o / 4).Distinct().ToArray();
                var st4 = d.Select(o => o / 4).Distinct().ToArray();
                Array.Sort(Straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);

                #endregion

                for (int i = 0; i < 16; i++)
                {
                    if (this.deck.GetCardAtPosition(i).CardPower == player.Cards.First().CardPower && this.deck.GetCardAtPosition(i + 1).CardPower == player.Cards.Last().CardPower)
                    {
                        

                        this.checkHandType.CheckPairFromHand(player, ref this.strongestHands, ref this.winningHand, this.deck.GetAllCards(), i);

                        #region TwoPair or Two TwoPair from Table current = 2 || 0
                        this.checkHandType.CheckPairTwoPair(player, ref this.strongestHands, ref this.winningHand, this.deck.GetAllCards(), i);

                        #endregion

                        #region Two TwoPair current = 2
                        #endregion

                        #region Three of a kind current = 3
                        this.checkHandType.CheckThreeOfAKind(player, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Straight current = 4
                        this.checkHandType.CheckStraight(player, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Flush current = 5 || 5.5
                        this.checkHandType.CheckFlush(player, ref vf, Straight1, ref this.strongestHands, ref this.winningHand, this.deck.GetAllCards(), i);
                        #endregion

                        #region Full House current = 6
                        this.checkHandType.CheckFullHouse(player, ref done, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Four of a Kind current = 7
                        this.checkHandType.CheckFourOfAKind(player, Straight, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region Straight Flush current = 8 || 9
                        this.checkHandType.CheckStraightFlush(player, st1, st2, st3, st4, ref this.strongestHands, ref this.winningHand);
                        #endregion

                        #region High Card current = -1
                        this.checkHandType.CheckHighCard(player, ref this.strongestHands, ref this.winningHand, this.deck.GetAllCards(), i);

                        #endregion
                    }
                }
            }
        }

        private ICollection<ICharacter> GetWinners(ICollection<ICharacter> players)
        {
            ICollection<ICharacter> winners = new List<ICharacter>();
            foreach (var player in players)
            {
                if (!player.HasFolded)
                {
                    if ((player.CharacterType.Current == this.winningHand.Current && player.CharacterType.Power == this.winningHand.Power) || this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 1)
                    {
                        winners.Add(player);
                    }
                }
            }

            return winners;
        }

        private void ShowWinnersMessages(ICollection<ICharacter> winners)
        {
            foreach (var player in winners)
            {
                if (player.CharacterType.Current == -1)
                {
                    this.Message.OutputMessage(player.Name + " High Card ");
                }
                if (player.CharacterType.Current == 1 || player.CharacterType.Current == 0)
                {
                    this.Message.OutputMessage(player.Name + " TwoPair ");
                }
                if (player.CharacterType.Current == 2)
                {
                    this.Message.OutputMessage(player.Name + " Two TwoPair ");
                }
                if (player.CharacterType.Current == 3)
                {
                    this.Message.OutputMessage(player.Name + " Three of a Kind ");
                }
                if (player.CharacterType.Current == 4)
                {
                    this.Message.OutputMessage(player.Name + " Straight ");
                }
                if (player.CharacterType.Current == 5 || player.CharacterType.Current == 5.5)
                {
                    this.Message.OutputMessage(player.Name + " Flush ");
                }
                if (player.CharacterType.Current == 6)
                {
                    this.Message.OutputMessage(player.Name + " Full House ");
                }
                if (player.CharacterType.Current == 7)
                {
                    this.Message.OutputMessage(player.Name + " Four of a Kind ");
                }
                if (player.CharacterType.Current == 8)
                {
                    this.Message.OutputMessage(player.Name + " Straight Flush ");
                }
                if (player.CharacterType.Current == 9)
                {
                    this.Message.OutputMessage(player.Name + " Royal Flush ! ");
                }
            }
        }

        private void SetWinnersChips(ICollection<ICharacter> players)
        {
            foreach (var player in players)
            {
                player.Chips += this.Bet.BetValue / players.Count;
                player.TextBoxChips.Text = player.Chips.ToString();
            }
        }

        private void CheckWinners(ICollection<ICharacter> players, IPokerManager dealer)
        {
            for (int i = 0; i < dealer.Cards.Count; i++)
            {
                dealer.ShowCardAtPosition(i);
            }

            foreach (var player in players)
            {
                for (int i = 0; i < player.Cards.Count; i++)
                {
                    player.ShowCardAtPosition(i);
                }
            }

            var winners = this.GetWinners(players);
            if (this.GetNotFoldedPlayersCount(this.GetAllPlayers()) != 1)
            {
                this.ShowWinnersMessages(winners);
            }

            this.SetWinnersChips(winners);
        }

        private int GetNotFoldedPlayersCount(ICollection<ICharacter> players)
        {
            return this.GetAllPlayers().Count - this.GetFoldedPlayersCount(players);
        }

        private void ResetCall(ICollection<ICharacter> players)
        {
            foreach (var player in players)
            {
                player.CallValue = 0;
            }
        }

        private void ResetRaise(ICollection<ICharacter> players)
        {
            foreach (var player in players)
            {
                player.RaiseValue = 0;
            }
        }

        async Task CheckRaise(int currentTurn)
        {
            if (hasRaisedPlayers)
            {
                turnCount = 0;
                hasRaisedPlayers = false;
                raisedTurn = currentTurn;
                changed = true;
            }
            else
            {
                if (turnCount >= this.GetNotFoldedPlayersCount(this.GetAllPlayers()) - 1 || !changed && turnCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()))
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == this.GetNotFoldedPlayersCount(this.GetAllPlayers()) || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        Raise = 0;
                        Call = 0;
                        raisedTurn = 123;
                        this.pokerManager.CurrentGameState++;
                        foreach (var player in this.GetAllPlayers())
                        {
                            if (!player.FoldTurn)
                            {
                                player.CharacterStatus.Text = string.Empty;
                            }
                        }
                    }
                }
            }

            if (this.pokerManager.CurrentGameState == GameStateType.Flop)
            {
                for (int j = 0; j <= 2; j++)
                {
                    if (this.pokerManager.PictureBox[j].Image != this.pokerManager.Cards.ElementAt(j).CardImage)
                    {
                        this.pokerManager.ShowCardAtPosition(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (this.pokerManager.CurrentGameState == GameStateType.Turn)
            {
                for (int j = 2; j <= 3; j++)
                {
                    if (this.pokerManager.PictureBox[j].Image != this.pokerManager.Cards.ElementAt(j).CardImage)
                    {
                        this.pokerManager.ShowCardAtPosition(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (this.pokerManager.CurrentGameState == GameStateType.River)
            {
                for (int j = 3; j <= 4; j++)
                {
                    if (this.pokerManager.PictureBox[j].Image != this.pokerManager.Cards.ElementAt(j).CardImage)
                    {
                        this.pokerManager.ShowCardAtPosition(j);
                        this.ResetCall(this.GetAllPlayers());
                        this.ResetRaise(this.GetAllPlayers());
                    }
                }
            }
            if (this.pokerManager.CurrentGameState == GameStateType.End && this.GetNotFoldedPlayersCount(this.GetAllPlayers()) == 6)
            {
                await this.Finish(2);
                await Turns(); 
            }
        }

        public void AddChips(ICollection<ICharacter> players, int amount)
        {
            foreach (var player in players)
            {
                player.Chips += amount;
            }
        }

        private void ClearCards(ICollection<ICardHolder> cardHolders)
        {
            foreach (var cardHolder in cardHolders)
            {
                foreach (var pictureBox in cardHolder.PictureBox)
                {
                    pictureBox.Image = null;
                    pictureBox.Invalidate();
                    pictureBox.Visible = false;
                }

                cardHolder.Cards.Clear();
            }
        }

        void FixCall(ICharacter player, int options)
        {
            if (this.pokerManager.CurrentGameState != GameStateType.End)
            {
                if (options == 1)
                {
                    if (player.CharacterStatus.Text.Contains("Raise"))
                    {
                        var changeRaise = player.CharacterStatus.Text.Substring(6);
                        player.RaiseValue = int.Parse(changeRaise);
                    }
                    if (player.CharacterStatus.Text.Contains("Call"))
                    {
                        var changeCall = player.CharacterStatus.Text.Substring(5);
                        player.CallValue = int.Parse(changeCall);
                    }
                    if (player.CharacterStatus.Text.Contains("Check"))
                    {
                        this.ResetCall(new List<ICharacter>() { player });
                        this.ResetRaise(new List<ICharacter>() { player });
                    }
                }

                if (options == 2)
                {
                    if (player.RaiseValue < this.Raise)
                    {
                       Call = Convert.ToInt32(Raise) - player.RaiseValue;
                    }

                    if (player.CallValue < Call)
                    {
                        Call = Call - player.CallValue;
                    }

                    if (player.RaiseValue == Raise && Raise > 0)
                    {
                        Call = 0;
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in
            int allInPlayersCount = 0;
            if (player.Chips <= 0)
            {
                if (this.player.CharacterStatus.Text.Contains("Raise") || this.player.CharacterStatus.Text.Contains("Call"))
                {
                    allInPlayersCount++;
                }
            }

            foreach (var enemy in this.bots)
            {
                if (enemy.Chips <= 0 && !enemy.FoldTurn)
                {
                    allInPlayersCount++;
                }
            }
            #endregion

            var notFoldedPlayersCount = this.GetNotFoldedPlayersCount(this.GetAllPlayers());

            #region LastManStanding
            if (notFoldedPlayersCount == 1)
            {
                ICharacter notFoldedPlayer = this.GetAllPlayers().FirstOrDefault(p => p.HasFolded == false);
                this.Message.OutputMessage(notFoldedPlayer.Name + " Wins");
                
                this.HideCardsPictureBoxes(this.GetCardHolders());
                await Finish(1);
            }

            #endregion

            #region FiveOrLessLeft
            if (notFoldedPlayersCount < 6 && notFoldedPlayersCount > 1 && this.pokerManager.CurrentGameState >= GameStateType.End)
            {
                await Finish(2);
            }
            #endregion
        }

        private void HideCardsPictureBoxes(ICollection<ICardHolder> cardHolders)
        {
            foreach (var cardHolder in cardHolders)
            {
                foreach (var pictureBox in cardHolder.PictureBox)
                {
                    pictureBox.Visible = false;
                }
            }
        }

        private void ResetForNextGame(ICharacter player, ICollection<ICharacter> bots)
        {
            IList<ICharacter> allPlayers = new List<ICharacter>(bots);
            allPlayers.Add(player);

            foreach (var currentPlayer in allPlayers)
            {
                currentPlayer.CharacterPanel.Visible = false;
                currentPlayer.CharacterType.Power = 0;
                currentPlayer.CharacterType.Current = -1;
                currentPlayer.FoldTurn = false;
                currentPlayer.HasFolded = false;
                currentPlayer.IsInTurn = false;
                currentPlayer.CharacterStatus.Text = string.Empty;
            }

            this.ResetCall(allPlayers);
            this.ResetRaise(allPlayers);

            player.IsInTurn = true;
        }

        async Task Finish(int n)
        {
            FixWinners();

            this.ResetForNextGame(this.player, this.bots);
            
            this.CheckPlayerChipsAmount(this.player);
            this.ResetGameVariables();
            this.ClearCards(this.GetCardHolders());
            await Shuffle();
        }

        private ICollection<ICardHolder> GetCardHolders()
        {
            ICollection<ICardHolder> cardHolders = new List<ICardHolder>(this.GetAllPlayers());
            cardHolders.Add(this.pokerManager);

            return cardHolders;
        }

        private void CheckPlayerChipsAmount(ICharacter player)
        {
            if (player.Chips <= 0)
            {
                AddChips addChipsDialog = new AddChips();
                addChipsDialog.ShowDialog();
                if (addChipsDialog.ChipsValue != 0)
                {
                    this.AddChips(this.GetAllPlayers(), addChipsDialog.ChipsValue);
                    player.FoldTurn = false;
                    player.IsInTurn = true;
                }
            }
        }

        private void ResetGameVariables()
        {
            this.strongestHands.Clear(); 
            this.winningHand.Current = 0;
            this.winningHand.Power = 0;
            this.Bet.Clear();
            this.SetDefaultCall();
            this.pokerManager.CurrentGameState = 0;
            Raise = 0;
            hasRaisedPlayers = false;
            raisedTurn = 1;
            turnCount = 0;
        }

        void FixWinners()
        {
            foreach (var player in this.GetAllPlayers())
            {
                if (!player.HasFolded)
                {
                    Rules(player);
                }
            }

            this.CheckWinners(this.GetAllPlayers(), this.pokerManager);
        }

        void AI(ICharacter player)
        {
            if (!player.FoldTurn)
            {
                if (player.CharacterType.Current == GameConstants.HighCard)
                {
                    handType.HighCard(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.PairTable)
                {
                   handType.PairTable(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.PairFromHand)
                {
                    handType.PairHand(player, Call, this.Bet, raise, this.pokerManager.CurrentGameState);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.TwoPair)
                {
                    handType.TwoPair(player, Call, this.Bet, raise, this.pokerManager.CurrentGameState);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.ThreeOfAKind)
                {
                    handType.ThreeOfAKind(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.Straigth)
                {
                    handType.Straight(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.Flush || player.CharacterType.Current == GameConstants.FlushWithAce)
                {
                    handType.Flush(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.FullHouse)
                {
                   handType.FullHouse(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.FourOfAKind)
                {
                    handType.FourOfAKind(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }

                if (player.CharacterType.Current == GameConstants.StraightFlush || player.CharacterType.Current == GameConstants.RoyalFlush)
                {
                    handType.StraightFlush(player, Call, this.Bet, raise);
                    CheckForRaisedPlayers(player);
                }
            }

            if (player.FoldTurn)
            {
                foreach (var pictureBox in player.PictureBox)
                {
                    pictureBox.Visible = false;
                }
            }
        }

        private void CheckForRaisedPlayers(ICharacter player)
        {
            var allPlayers = this.bots;
            allPlayers.Add(this.player);
            foreach (var character in allPlayers)
            {
                if (character.HasRaised)
                {
                    this.hasRaisedPlayers = true;
                    break;
                }
            }
        }
    }
}
