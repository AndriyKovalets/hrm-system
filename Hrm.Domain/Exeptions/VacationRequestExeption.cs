using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class VacationRequestExeption : Exception
    {
        public VacationRequestExeption(string message) : base(message)
        {
        }

        public VacationRequestExeption(string message, Exception innerException): base(message, innerException)
        {
        }

        public VacationRequestExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
