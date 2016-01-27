namespace Poker.Utils
{
    using System;

    public static class CalculationsHelper
    {
        public static double RoundNumber(int number, int deviser)
        {
            var result = Math.Round(number / (double)deviser, 0);

            return result;
        }
    }
}
