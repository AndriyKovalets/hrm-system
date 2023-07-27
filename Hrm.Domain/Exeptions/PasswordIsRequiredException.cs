using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class PasswordIsRequiredException: Exception
    {
        public PasswordIsRequiredException() : base("Password is required")
        {
        }

        public PasswordIsRequiredException(Exception innerException) : base("Password is required", innerException)
        {
        }

        public PasswordIsRequiredException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
