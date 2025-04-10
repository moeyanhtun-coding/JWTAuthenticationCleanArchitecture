using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Register
{
    public class RegisterUserModel
    {
        [Required]
        public string? Name { get; set; }
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
        [Required, Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]  
        public string? ConfirmPassword { get; set; }
    }
}
