using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthDTOs
{
    public class LoginRequestDto
    {
        public int UserId { get; set; }
        public string Password { get; set; }
    }
}
