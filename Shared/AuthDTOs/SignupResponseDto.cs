using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthDTOs
{
    public class SignupResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPhone { get; set; }
        public int UserGroup { get; set; }
        public int UserDepartment { get; set; }
    }
}
