using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class BadConnectionStringExeption: Exception
    {
        public BadConnectionStringExeption() : base("Bad connection string")
        {
        }

        public BadConnectionStringExeption(Exception innerException) : base("Bad connection string", innerException)
        {
        }

        public BadConnectionStringExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
