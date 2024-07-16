using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.interfaces
{
    public interface ILocation
    {
        string LocationAddress { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
