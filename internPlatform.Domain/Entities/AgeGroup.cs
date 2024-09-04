using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace internPlatform.Domain.Entities
{
    public class AgeGroup
    {
        [Key]
        public int AgeGroupId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        [DisplayName("Age Group Name")]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
    }
}
