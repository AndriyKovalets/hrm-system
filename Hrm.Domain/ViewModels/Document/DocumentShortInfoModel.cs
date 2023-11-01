using System.ComponentModel.DataAnnotations;

namespace Hrm.Domain.ViewModels.Document
{
    public class DocumentShortInfoModel
    {
        public int Id { get; set; }
        [Required]
        public string? Header { get; set; }
        public DateTime CrerateAt { get; set; }
        [Required]
        public string? Content { get; set; }
    }
}
