namespace internPlatform.Domain.Entities.DTO
{
    public class ImageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsTemp { get; set; }
        public bool IsMain { get; set; }
        public int DisplayOrder { get; set; }
        public TimeStamp TimeStamp { get; set; }
    }
}
