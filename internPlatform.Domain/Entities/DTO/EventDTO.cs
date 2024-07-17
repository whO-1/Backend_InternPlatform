using System;
using System.Collections.Generic;

namespace internPlatform.Domain.Entities.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SpecialGuests { get; set; }
        public virtual int AgeGroupId { get; set; }
        public virtual int EntryTypeId { get; set; }
        public virtual List<string> Categories { get; set; }
        public string AuthorId { get; set; }
        public Location EventLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
