using System.Collections.Generic;

namespace internPlatform.Domain.Models.ApiModels.StatisticsModels
{
    public class StatisiticInteractions
    {
        public List<string> Labels { get; set; }
        public List<int> Likes { get; set; }
        public List<int> Views { get; set; }
    }
}
