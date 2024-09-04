using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internPlatform.Domain.Entities
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        [Required]
        [MinLength(1)]
        [MaxLength(30)]
        [DisplayName("Event Title")]
        public string Title { get; set; }

        [DisplayName("Event Description")]
        public string Description { get; set; }
        public string SpecialGuests { get; set; }
        public int? AgeGroupId { get; set; }
        [ForeignKey("Id")]
        public virtual AgeGroup Age { get; set; }
        public int? EntryTypeId { get; set; }
        [ForeignKey("Id")]
        public virtual EntryType Entry { get; set; }
        [DisplayName("Author")]
        [Required]
        public string Author { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Start date:")]
        [Required]
        public DateTime StartDate { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("End date:")]
        [Required]
        public DateTime EndDate { get; set; }

        public TimeStamp TimeStamp { get; set; }
        public Location EventLocation { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBlocked { get; set; }
        public int Likes { get; set; }
        public int Views { get; set; }
        public virtual ICollection<Category> Categories { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public virtual User User { get; set; }


        public Event()
        {
            EventLocation = new Location();
            TimeStamp = new TimeStamp();
        }

    }
}
