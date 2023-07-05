using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class UserNotFoundExeption: Exception
    {
        public UserNotFoundExeption() : base("User not found")
        {
        }

        public UserNotFoundExeption(Exception innerException) : base("User not found", innerException)
        {
        }

        public UserNotFoundExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
