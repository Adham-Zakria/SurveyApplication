using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> AuthenticateAsync(int userCode, string password);
    }
}
