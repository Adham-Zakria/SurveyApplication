using Domain.Models;
using Shared.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IDepartmentService
    {
        Task<IEnumerable<Department>> GetAllAsync();
        Task<IEnumerable<UserDto>> GetDepartmentUsersAsync(int departmentId);
    }
}
