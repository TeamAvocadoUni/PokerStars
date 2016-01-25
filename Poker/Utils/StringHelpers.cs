namespace Poker.Utils
{
    public  static class StringHelpers
    {
        public static string StringSlicer(string targetString, params string[] partsToRemove)
        {
            var result = targetString;
            for (int i = 0; i < partsToRemove.Length; i++)
            {
                result = result.Replace(partsToRemove[i], string.Empty);
            }

            return result;
        }
    }
}
