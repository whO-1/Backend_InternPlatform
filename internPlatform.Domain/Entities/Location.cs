using internPlatform.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.Entities
{
    [ComplexType]
    public class Location : ILocation
    {
        public string LocationAddress { get ; set ; }
        public double Latitude { get ; set ; }
        public double Longitude { get ; set ; }
    }
}
    