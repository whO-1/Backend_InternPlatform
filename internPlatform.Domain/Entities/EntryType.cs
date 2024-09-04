using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace internPlatform.Domain.Entities
{
    public class EntryType
    {
        [Key]
        public int EntryTypeId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        [DisplayName("Entry Type Name")]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
    }
}

