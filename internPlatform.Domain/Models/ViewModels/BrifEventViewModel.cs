using System;
using System.Collections.Generic;

namespace internPlatform.Domain.Models.ViewModels
{
    public class BrifEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SpecialGuests { get; set; }
        public string AgeGroup { get; set; }
        public string EntryType { get; set; }
        public string Author { get; set; }
        public List<string> SelectedCategories { get; set; } = new List<string>();
        public DateTime LastUpdate { get; set; }
    }
}
