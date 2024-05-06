using System;
using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Data.Entities
{
    public class LoginHistory
    {
        [Key]
        public int LoginId { get; set; }
        public int ClientId { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
