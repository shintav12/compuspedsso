using System;
using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Data.Entities
{
    public class ErrorLog
    {
        [Key]
        public int ErrorLogId { get; set; }
        public string ErrorType { get; set; }
        public string Location { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string InnerException { get; set; }
        public DateTime RegisteredDate { get; set; }
    }
}
