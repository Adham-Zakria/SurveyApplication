using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthDTOs
{
    public class SignupRequestDto
    {
        public int UserId { get; set; }   
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserPhone { get; set; }

        public int UserGroup = 2; // 1 = Manager, 2 = Employee ...
        public int UserDepartment { get; set; }
    }
}
