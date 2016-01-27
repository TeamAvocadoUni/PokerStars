namespace Poker.Core.Factory
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;

    using Interfaces;
    using Models.Character;


    public class CharacterFactory : ICharacterFactory
    {
        private static int characterId = 0;
        private static int charactersCount = 0;

        public Character CreateCharacter(
            string characterType,
            string name,
            int chips,
            Label status,
            TextBox textBox,
            AnchorStyles cardHolderAnchorStyles,
            int pictureBoxX,
            int pictureBoxY)
        {
            IList<PictureBox> cardHolders = new List<PictureBox>();
            cardHolders.Add(CreatePictureBox(cardHolderAnchorStyles, pictureBoxX, pictureBoxY));
            pictureBoxX += cardHolders.First().Width;
            cardHolders.Add(CreatePictureBox(cardHolderAnchorStyles, pictureBoxX, pictureBoxY));

            Panel panel = new Panel();
            panel.Location = new Point(cardHolders.First().Left - 10, cardHolders.Last().Top - 10);
            panel.BackColor = Color.DarkBlue;
            panel.Height = 150;
            panel.Width = 180;
            panel.Visible = false;

            textBox.Enabled = false;
            switch (characterType)
            {
                    
                case "Player":
                    return new Player(
                        characterId,
                        name,
                        status,
                        textBox,
                        chips,
                        cardHolders,
                        panel);
                case "Bot":
                    return new Bot(
                        characterId,
                        name,
                        status,
                        textBox,
                        chips,
                        cardHolders,
                        panel);
                default:
                    throw new NotImplementedException("This character type is not implemented.");
            }
        }

        private static PictureBox CreatePictureBox(
            AnchorStyles cardHoldersPictureBoxesAnchorStyles,
            int cardHoldersPictureBoxesX,
            int cardHoldersPictureBoxesY)
        {
            PictureBox cardHolder = new PictureBox();
            cardHolder.SizeMode = PictureBoxSizeMode.StretchImage;
            cardHolder.Height = 130;
            cardHolder.Width = 80;
            cardHolder.Visible = false;
            cardHolder.Name = "pb" + charactersCount++;
            cardHolder.Anchor = cardHoldersPictureBoxesAnchorStyles;
            cardHolder.Location = new Point(cardHoldersPictureBoxesX, cardHoldersPictureBoxesY);

            return cardHolder;
        }


    }
}
