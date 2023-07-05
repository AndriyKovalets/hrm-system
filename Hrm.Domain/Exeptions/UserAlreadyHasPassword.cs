using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class UserAlreadyHasPassword: Exception
    {
        public UserAlreadyHasPassword() : base("User already has password")
        {
        }

        public UserAlreadyHasPassword(Exception innerException) : base("User already has password", innerException)
        {
        }

        public UserAlreadyHasPassword(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
