using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class InvalidEmailOrPasswordExeption: Exception
    {
        public InvalidEmailOrPasswordExeption() : base("Invalid email or password")
        {
        }

        public InvalidEmailOrPasswordExeption(Exception innerException) : base("Invalid email or password", innerException)
        {
        }

        public InvalidEmailOrPasswordExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
