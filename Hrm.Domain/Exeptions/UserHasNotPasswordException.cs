using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class UserHasNotPasswordException: Exception
    {
        public UserHasNotPasswordException() : base("User has not password")
        {
        }

        public UserHasNotPasswordException(Exception innerException) : base("User has not password", innerException)
        {
        }

        public UserHasNotPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
