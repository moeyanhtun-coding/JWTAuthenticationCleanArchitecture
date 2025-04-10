using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Login
{
    public record LoginResponseModel (bool Flag, string Message = null!, string Token = null!);
}
