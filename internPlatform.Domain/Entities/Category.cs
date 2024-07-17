using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace internPlatform.Domain.Entities
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        [DisplayName ("Category Name")]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
        public virtual ICollection<Event> Events { get; set; } = new List<Event>();
    }
}