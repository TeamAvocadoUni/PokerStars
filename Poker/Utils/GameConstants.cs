namespace Poker.Utils
{
    public static class GameConstants
    {
        public const string ImagePath = "..\\..\\Resources\\Assets\\Cards";
        public const string BackImagePath = "..\\..\\Resources\\Assets\\Back\\Back.png";
        public const string ImagePathInFolder = "..\\..\\Resources\\Assets\\Cards\\";
        public const string ImageFileExtension = ".png";
        public const string AllImageFileExtension = "*.png";
        public const string WinGameMessage = "You Won, Congratulations!";
        public const string PlayAgainMessage = "Would You Like To Play Again?";
        public const string PlayerTurnMessage = "'s Turn";

        public const int DrowedCards = 5;
        public const int DefautBigBlind = 500;
        public const int DefautSmallBlind = 250;
        public const int ChipsMaxValue = 100000000;

        public const double HighCard = -1;
        public const double PairTable = 0;
        public const double PairFromHand = 1;
        public const double TwoPair = 2;
        public const double ThreeOfAKind = 3;
        public const double Straigth = 4;
        public const double Flush = 5;
        public const double FlushWithAce = 5.5;
        public const double FullHouse = 6;
        public const double FourOfAKind = 7;
        public const double StraightFlush = 8;
        public const double RoyalFlush = 9;
    }
}
