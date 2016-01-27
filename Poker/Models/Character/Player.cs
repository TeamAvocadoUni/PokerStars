namespace Poker.Models.Character
{
    using System.Collections.Generic;
    using System.Windows.Forms;

    using Poker.Interfaces;

    public class Player : Character
    {
        public Player(int id, string name, Label status, TextBox chipsTextBox, int chips, IList<PictureBox> pictureBoxHolder, Panel panel)
            : base(id, name, status, chipsTextBox, chips, pictureBoxHolder, panel)
        {
        }

        protected override void SetCardImage(ICard card, PictureBox pictureBox)
        {
            pictureBox.Image = card.CardImage;
        }
    }
}
