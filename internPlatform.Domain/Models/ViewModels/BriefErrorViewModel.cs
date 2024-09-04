namespace internPlatform.Domain.Models.ViewModels
{
    public class BriefErrorViewModel
    {
        public int ErrorLogId { get; set; }
        public string Timestamp { get; set; }
        public string Message { get; set; }
        public string CallSite { get; set; }
        public int LineNumber { get; set; }
    }
}
