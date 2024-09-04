using internPlatform.Domain.interfaces;
using System;

namespace internPlatform.Domain.Entities
{
    public class TimeStamp : ITimeStamp
    {
        public DateTime CreatedDate { get; private set; }
        public DateTime UpdateDate { get; private set; }

        public TimeStamp()
        {
            CreatedDate = DateTime.Now;
            UpdateDate = CreatedDate;
        }
        public void PerformUpdate()
        {
            UpdateDate = DateTime.Now;
        }
    }
}
