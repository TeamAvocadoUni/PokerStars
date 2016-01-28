namespace Poker.Models
{
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Enums;
    using Interfaces;
    using Character;
    using Utils;

    public class PokerManager : CardHolder, IPokerManager
    {
        public PokerManager(int x, int y)
        {
            this.PictureBox = new List<PictureBox>();
            this.Cards = new List<ICard>();
            this.SetCardDetails(x, y);
        }

        public GameStateType CurrentGameState { get; set; }

        public override async Task SetAllCards(IList<ICard> cards)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                await Task.Delay(200);
                this.Cards.Add(cards[i]);
                this.PictureBox[i].Tag = cards[i].CardPower;
                this.SetCardImage(cards[i], this.PictureBox[i]);
                this.PictureBox[i].Visible = true;
            }
        }

        protected override void SetCardImage(ICard card, PictureBox pictureBox)
        {
            pictureBox.Image = card.CardBackImage;
        }

        private void SetCardDetails(int x, int y)
        {
            for (int i = 0; i < GameConstants.DrowedCards; i++)
            {
                PictureBox cardHolder = new PictureBox();
                cardHolder.Anchor = AnchorStyles.None;
                cardHolder.SizeMode = PictureBoxSizeMode.StretchImage;
                cardHolder.Height = 130;
                cardHolder.Width = 80;
                cardHolder.Visible = false;
                cardHolder.Location = new Point(x, y);
                x += 110;
                this.PictureBox.Add(cardHolder);
            }
        }
    }
}
