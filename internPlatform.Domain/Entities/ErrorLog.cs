using System.ComponentModel.DataAnnotations;

namespace internPlatform.Domain.Entities
{
    public class ErrorLog
    {
        [Key]
        public int ErrorLogId { get; set; }
        public string Timestamp { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public string CallSite { get; set; }
        public int LineNumber { get; set; }
    }
}
