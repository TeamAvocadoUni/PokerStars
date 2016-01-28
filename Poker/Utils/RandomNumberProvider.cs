using System;
using Poker.Interfaces;

namespace Poker.Utils
{
    public class RandomNumberProvider : IRandomNumberProvider
    {
        private readonly Random random;

        public RandomNumberProvider()
        {
            this.random = new Random();
        }

        public int GetRandomNumberInInterval(int min, int max)
        {
            return this.random.Next(min, max);
        }

        public int GetNextRandomNumber(int max)
        {
            return this.random.Next(max);
        }
    }
}
