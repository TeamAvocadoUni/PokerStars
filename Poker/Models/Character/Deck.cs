namespace Poker.Models.Character
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Threading.Tasks;

    using Poker.Interfaces;

    public class Deck : IDeck
    {
        private static Deck instance;
        private readonly IList<ICard> cards;

        private Deck()
        {
            this.cards = new List<ICard>();
            string[] imagesLocations = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < imagesLocations.Length; i++)
            {
                var charsToRemove = new string[] { "Assets\\Cards\\", ".png" };
                Image cardImage = Image.FromFile(imagesLocations[i]);
                foreach (var c in charsToRemove)
                {
                    imagesLocations[i] = imagesLocations[i].Replace(c, string.Empty);
                }

                int cardNumber = int.Parse(imagesLocations[i]) - 1;
                Card currentCard = new Card(cardNumber, cardImage);
                this.cards.Add(currentCard);
            }
        }

        public static Deck Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Deck();
                }

                return instance;
            }
        }

        public IList<ICard> GetAllCards()
        {
            return this.cards;
        }

        public ICard GetCardAtPosition(int position)
        {
            return this.cards[position];
        }

        public async Task SetAllCards(IList<ICharacter> characters, IPokerManager pokerManager)
        {
            this.ShuffleDeck();
            int charactersCount;
            for (charactersCount = 0; charactersCount < characters.Count; charactersCount++)
            {
                await this.SetCardToCharacters(characters[charactersCount], charactersCount, 2);
            }

            await this.SetCardToPokerManager(pokerManager, 5, charactersCount * 2);
        }

        private async Task SetCardToCharacters(ICardHolder cardHandler, int cardHandlerIndex, int cardsCountToSet)
        {
            var cards = new List<ICard>();
            for (int i = 0; i < cardsCountToSet; i++)
            {
                cards.Add(this.cards[cardHandlerIndex * cardsCountToSet + i]);
            }

            await cardHandler.SetAllCards(cards);
        }

        private async Task SetCardToPokerManager(ICardHolder cardHandler, int cardsCountToSet, int allPlayersCardsCount)
        {
            var cards = new List<ICard>();

            for (int i = 0; i < cardsCountToSet; i++)
            {
                cards.Add(this.cards[allPlayersCardsCount + i]);
            }

            await cardHandler.SetAllCards(cards);
        }

        private void ShuffleDeck()
        {
            Random random = new Random();
            for (int i = this.cards.Count; i > 0; i--)
            {
                int j = random.Next(i);
                var k = this.cards[j];
                this.cards[j] = this.cards[i - 1];
                this.cards[i - 1] = k;
            }
        }
    }
}
