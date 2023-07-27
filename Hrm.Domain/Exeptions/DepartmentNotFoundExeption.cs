using System.Runtime.Serialization;

namespace Hrm.Domain.Exeptions
{
    public class DepartmentNotFoundExeption: Exception
    {
        public DepartmentNotFoundExeption() : base("Department not found")
        {
        }

        public DepartmentNotFoundExeption(Exception innerException) : base("Department not found", innerException)
        {
        }

        public DepartmentNotFoundExeption(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
