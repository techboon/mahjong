namespace Mahjong.Exception
{
    public class InvalidArgumentException : System.Exception
    {
        public InvalidArgumentException() { }
        public InvalidArgumentException(string message) : base(message) { }
        public InvalidArgumentException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidArgumentException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
