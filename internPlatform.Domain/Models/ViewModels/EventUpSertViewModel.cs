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
        public int? AgeGroupId { get; set; }
        public int? EntryTypeId { get; set; }
        public string AuthorId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Today { get; set; }
        public List<int> SelectedCategories { get; set; } = new List<int>();
        public string Longitude { get; set; } = string.Empty;
        public string Latitude { get; set; } = string.Empty;
        public string InputImages { get; set; }
        public List<FileViewModel> StoredImages { get; set; }
        public string StoredImagesIds { get; set; }
        public string MainImageId { get; set; }
        public EventUpSertViewModel()
        {
            Today = DateTime.Now.ToString("yyyy-MM-ddTHH:mm");
        }
    }
}
