using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace internPlatform.Domain.Entities
{
    public class AgeGroup
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        [DisplayName("Age Group Name")]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
    }
}
