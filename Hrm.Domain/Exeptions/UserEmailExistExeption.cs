using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class UserEmailExistExeption: Exception
    {
        public UserEmailExistExeption() : base("User with this email already exist")
        {
        }

        public UserEmailExistExeption(Exception innerException) : base("User with this email already exist", innerException)
        {
        }

        public UserEmailExistExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
