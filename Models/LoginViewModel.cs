using System.ComponentModel.DataAnnotations;

namespace Inlamning2.Models
{
    public class LoginViewModel
    {
        [Display(Name ="Username")]
        [Required]
        public string Username { get; set; }
        
        [Display(Name = "Password")]
        [Required]
        public string Password{ get; set; }
        
        public string RedirectUrl{ get; set; }

    }
}