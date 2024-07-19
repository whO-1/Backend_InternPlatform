using internPlatform.Domain.interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace internPlatform.Domain.Entities
{
    [ComplexType]
    public class Location : ILocation
    {
        public string LocationAddress { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
    }
}
