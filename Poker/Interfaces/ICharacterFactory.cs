namespace Poker.Interfaces
{
    using System.Windows.Forms;

    using Poker.Models.Character;


    public interface ICharacterFactory
    {
        Character CreateCharacter(
            string characterType,
            string name,
            int chips,
            Label status, 
            TextBox textBox,
            AnchorStyles cardHolderAnchorStyles,
            int pictureBoxX,
            int pictureBoxY);
    }
}
