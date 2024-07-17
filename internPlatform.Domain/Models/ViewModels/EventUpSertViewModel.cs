using internPlatform.Domain.Entities;
using System.Collections.Generic;

namespace internPlatform.Domain.Models.ViewModels
{
    public class EventUpSertViewModel
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<AgeGroup> AgeGroups { get; set; }
        public IEnumerable<EntryType> EntryTypes { get; set; }

        public Event Event { get; set; }



        //Update:

        //event id...
        public List<int> SelectedCategories { get; set; }

        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}
