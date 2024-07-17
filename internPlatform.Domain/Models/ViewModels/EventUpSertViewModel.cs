using internPlatform.Domain.Entities;
using internPlatform.Domain.Entities.DTO;
using System;
using System.Collections.Generic;

namespace internPlatform.Domain.Models.ViewModels
{
    public class EventUpSertViewModel
    {
        public IEnumerable<CategoryDTO> Categories { get; set; }
        public IEnumerable<AgeGroupDTO> AgeGroups { get; set; }
        public IEnumerable<EntryTypeDTO> EntryTypes { get; set; }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SpecialGuests { get; set; }
        public virtual int AgeGroupId { get; set; }
        public virtual int EntryTypeId { get; set; }
        public string AuthorId { get; set; }
        public Location EventLocation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> SelectedCategories { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}
