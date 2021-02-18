using System.ComponentModel.DataAnnotations;

namespace JustDo_Web.ViewModels
{
    public class UserRegistration
    {        
        public string Email { get; set; }
        
        public string Password { get; set; }
             
        public string ConfirmPassword { get; set; }
    }
}
