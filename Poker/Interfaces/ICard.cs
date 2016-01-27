namespace Poker.Interfaces
{
    using System.Drawing;

    public interface ICard
    {
        Bitmap CardBackImage { get; }

        int CardPower { get; }

        Image CardImage { get; }
    }
}
