using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace internPlatform.Domain.Entities
{
    public class Link
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(20)]
        [DisplayName("Sublink Name")]
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        [Required]
        public int DisplayOrder { get; set; }



        public int? HeadId { get; set; }
        [ForeignKey("HeadId")]
        public virtual Link HeadLink { get; set; }
    }

    
}