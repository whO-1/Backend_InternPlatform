using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.interfaces
{
    public interface ITimeStamp
    {
        DateTime CreatedDate { get; } 
        DateTime UpdateDate { get; }
    }
}
