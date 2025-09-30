using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAuthService
    {
        Task<(User user, Branch branch)> LoginAsync(int userCode, string password);
        Task<User> SignupAsync(User newUser, string plainPassword);
        Task ChangePasswordAsync(int userId, string oldPassword, string newPassword);
    }
}
