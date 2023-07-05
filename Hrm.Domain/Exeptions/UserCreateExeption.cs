using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class UserCreateExeption: Exception
    {

        public UserCreateExeption(string message) : base(message)
        {
        }

        public UserCreateExeption(string message, Exception innerException) : base(message, innerException)
        {
        }

        public UserCreateExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
