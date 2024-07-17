using internPlatform.Domain.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.Entities
{
    public class TimeStamp : ITimeStamp
    {
        public DateTime CreatedDate { get ; private set ; }
        public DateTime UpdateDate { get; private set ; }

        public TimeStamp() 
        {
            CreatedDate = DateTime.Now;
            UpdateDate = DateTime.Now;
        }
        public void PerformUpdate()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
