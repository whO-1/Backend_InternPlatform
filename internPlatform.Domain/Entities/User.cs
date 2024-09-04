using System.Collections.Generic;

namespace internPlatform.Domain.Entities
{
    public class User
    {
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public virtual Image Image { get; set; }
        public TimeStamp TimeStamp { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}
