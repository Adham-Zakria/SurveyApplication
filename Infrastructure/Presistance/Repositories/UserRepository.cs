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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly SurveyAppContext _context;

        public UserRepository(SurveyAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateAsync(int userCode, string password)
        {
            return await _context.Users
                .Include(u => u.UserGroupNavigation)
                .Include(u => u.UserDepartmentNavigation)
                .FirstOrDefaultAsync(u => u.UserId == userCode && u.UserPassword == password);
        }
    }
}
