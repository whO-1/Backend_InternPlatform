namespace internPlatform.Domain.Entities
{
    public class Image
    {
        public int ImageId { get; set; }
        public string Name { get; set; }
        public bool IsTemp { get; set; }
        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; }
        public TimeStamp TimeStamp { get; set; }
        public virtual Event Event { get; set; }
    }
}
