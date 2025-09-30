using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IDepartmentRepository : IGenericRepository<Department>
    {
        Task<IEnumerable<User>> GetAllDepartmentUsersAsync(int departmentId);
    }
}
