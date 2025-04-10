using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Register
{
    public record RegisterResponseModel(bool Flag, string Message = null!);
}
