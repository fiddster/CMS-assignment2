using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inlamning2.Models
{
    public class RegisterViewModel
    {
        [DisplayName("First Name")]
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        [Required(ErrorMessage = "Please enter your last name")]
        public string LastName { get; set; }

        [DisplayName("Username")]
        [Required(ErrorMessage = "Please enter your username")]
        [MinLength(6)]
        public string Username { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "Please enter your email")]
        public string EmailAddress { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Please enter your password")]
        [MinLength(8, ErrorMessage ="Please create a password at least 8 characters long")]
        public string Password { get; set; }

        [DisplayName("Confirm Password")]
        [Required(ErrorMessage = "Please confirmation your password")]
        [EqualTo("Password", ErrorMessage ="Please ensure your password match")]
        public string ConfirmPassword { get; set; }
    }
}