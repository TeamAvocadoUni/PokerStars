namespace Poker.Models
{
    using System.Drawing;

    using Poker.Interfaces;
    using Poker.Utils;

    public class Card : ICard
    {
        public Card(int cardPower, Image cardImage)
        {
            this.CardPower = cardPower;
            this.CardImage = cardImage;
        }

        public Bitmap CardBackImage
        {
            get
            {
                return new Bitmap(GameConstants.BackImagePath);
            }
        }

        public int CardPower { get; set; }

        public Image CardImage { get; set; }
    }
}
