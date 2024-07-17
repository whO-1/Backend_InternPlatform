using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.Entities
{
    public class EntryType
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        [DisplayName("Entry Type Name")]
        public string Name { get; set; }
        [Required]
        public int DisplayOrder { get; set; }
    }
}

