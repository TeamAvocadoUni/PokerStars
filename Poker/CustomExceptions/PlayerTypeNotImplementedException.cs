namespace Poker.CustomExceptions
{
    using System;

    public class PlayerTypeNotImplementedException : Exception
    {
        public PlayerTypeNotImplementedException(string message)
            :base(message)
        {
            
        }
    }
}
