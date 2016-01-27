namespace Poker.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    public interface ICardHolder
    {
        ICollection<ICard> Cards { get; set; }

        IList<PictureBox> PictureBox { get; set; }

        Task SetAllCards(IList<ICard> cards);


        void ShowCardAtPosition(int position);
    }
}
