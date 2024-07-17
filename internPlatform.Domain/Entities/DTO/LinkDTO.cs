using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.Entities.DTO
{
    public class LinkDTO
    {
        public int Id { get; set; }
        public string LinkTitle { get; set; }
        public string LinkUrl { get; set; }
        public int DisplayOrder { get; set; }
        public int? HeadId { get; set; }
    }
}
