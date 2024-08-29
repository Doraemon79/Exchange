using System.Runtime.Serialization;

namespace Exchange.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        // Parameterless constructor
        public CustomException()
            : base("An Exception has occurred.") { }

        // Constructor that accepts a custom message
        public CustomException(string message)
            : base(message) { }

        // Constructor that accepts a custom message and an inner exception
        public CustomException(string message, Exception innerException)
            : base(message, innerException) { }

        // Constructor for serialization
        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

        // Optional: Add any additional custom properties or methods
        public string CustomProperty { get; set; }
    }
}
