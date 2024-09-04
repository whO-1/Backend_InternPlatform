using System.Collections.Generic;

namespace internPlatform.Domain.Models.ApiModels.StatisticsModels
{
    public class StatisticBar
    {
        public List<string> Labels { get; set; }
        public List<int> Posted { get; set; }
        public List<int> Deleted { get; set; }
    }
}
