using Domain.Contracts;
using Domain.Models;
using ServicesAbstraction;
using Shared.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService(/*IGenericRepository<Department> _departmentRepository*/IDepartmentRepository _departmentRepository) : IDepartmentService
    {
        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<IEnumerable<UserDto>> GetDepartmentUsersAsync(int departmentId)
        {
            var users = await _departmentRepository.GetAllDepartmentUsersAsync(departmentId);

            return users.Select(u => new UserDto
            {
                UserId = u.UserId,
                UserName = u.UserName
            }).ToList();
        }
    }
}
