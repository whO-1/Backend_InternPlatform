using System.ComponentModel.DataAnnotations;

namespace internPlatform.Domain.Entities
{
    public class Faq
    {
        [Key]
        public int FaqId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
