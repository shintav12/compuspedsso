using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace CompuSPED.Models.AuthModels
{
    public class LoginSAMLModel
    {
        [Email]
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}