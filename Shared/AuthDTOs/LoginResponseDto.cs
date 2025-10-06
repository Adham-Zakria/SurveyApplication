using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AuthDTOs
{
    public class LoginResponseDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public bool IsManager { get; set; }
        public int? DepartmentId { get; set; }
        public int UserGroup { get; set; }
    }
}
 