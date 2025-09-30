using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        private readonly SurveyAppContext _context;
        public DepartmentRepository(SurveyAppContext context) : base(context) 
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllDepartmentUsersAsync(int departmentId)
        {
            var department = await _context.Departments
                   .Include(d => d.Users)
                   .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");

            if (department.Users == null || !department.Users.Any())
                throw new InvalidOperationException($"No users found for Department ID {departmentId}.");

            return department.Users;
        }
    }
}
