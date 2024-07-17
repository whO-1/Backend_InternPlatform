using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace internPlatform.Domain.Entities.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string SpecialGuests { get; set; }

        public virtual AgeGroup Age { get; set; }
        public virtual EntryType Entry { get; set; }
        public virtual List<string> Categories{ get; set; }

        [Required]
        public string AuthorId { get; set; }

        public TimeStamp TimeStamp { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
