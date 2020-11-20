using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Inlamning2.Models
{
    public class ContactModel
    {
        [Required(ErrorMessage = "Please enter you first name")]
        [DisplayName("First name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter you last name")]
        [DisplayName("Last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please enter you email")]
        [EmailAddress]
        [DisplayName("Email")]
        public string EmailAddress{ get; set; }
        [Required(ErrorMessage = "Please enter you message")]
        [DisplayName("Message")]
        public string Message { get; set; }

        public int ContactFormId { get; set; }
    }
}