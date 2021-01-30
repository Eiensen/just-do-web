using System.ComponentModel.DataAnnotations;

namespace JustDo_Web.ViewModels
{
    public class UserRegistration
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
