using internPlatform.Domain.Entities;
using System;
using System.Collections.Generic;

namespace internPlatform.Domain.Models.ViewModels
{
    public class ApiEventViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string SpecialGuests { get; set; }
        public int? AgeGroupId { get; set; }
        public string AgeGroupName { get; set; }
        public int? EntryTypeId { get; set; }
        public string EntryTypeName { get; set; }
        public string Author { get; set; }
        public List<string> SelectedCategories { get; set; } = new List<string>();
        public Location EventLocation { get; set; }
        public TimeStamp TimeStamp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
