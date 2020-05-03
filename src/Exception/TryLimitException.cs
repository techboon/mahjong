namespace Mahjong.Exception
{
    [System.Serializable]
    public class TryLimitException : System.Exception
    {
        public TryLimitException() { }
        public TryLimitException(string message) : base(message) { }
        public TryLimitException(string message, System.Exception inner) : base(message, inner) { }
        protected TryLimitException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

}