namespace Poker.Interfaces
{
    public interface IRandomNumberProvider
    {
        int GetRandomNumberInInterval(int min, int max);

        int GetNextRandomNumber(int max);
    }
}
