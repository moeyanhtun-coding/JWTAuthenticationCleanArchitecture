using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Login
{
    public class LoginUserModel
    {
        [Required, EmailAddress]
        public string? Email { get; set; }
        [Required, StringLength(100, MinimumLength = 6)]
        public string? Password { get; set; }
    }
}
