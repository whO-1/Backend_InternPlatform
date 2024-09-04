namespace internPlatform.Domain.Models.ViewModels
{
    public class FileViewModel
    {
        public int FileId { get; set; }
        public string FileName { get; set; }
        public int DisplayOrder { get; set; }
        public byte[] FileContent { get; set; }
    }
}
