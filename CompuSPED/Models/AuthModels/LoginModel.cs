using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Models.AuthModels
{
    public class LoginModel
    {
        [Email]
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string client_id { get; set; }
        [Required]
        public string redirect_uri { get; set; }
    }
}