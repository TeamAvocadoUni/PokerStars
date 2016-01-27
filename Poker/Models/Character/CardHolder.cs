namespace Poker.Models.Character
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    using Interfaces;

    public abstract class CardHolder : ICardHolder
    {
        public ICollection<ICard> Cards { get; set; }

        public IList<PictureBox> PictureBox { get; set; }

        public abstract Task SetAllCards(IList<ICard> cards);

        public void ShowCardAtPosition(int position)
        {
            this.PictureBox[position].Image = this.Cards.ElementAt(position).CardImage;
        }

        protected abstract void SetCardImage(ICard card, PictureBox pictureBox);
    }
}
