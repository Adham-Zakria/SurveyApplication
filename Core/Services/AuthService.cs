using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ServicesAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Services
{
    public class AuthService(
    IUserRepository _userRepository,
    IGenericRepository<Branch> _branchRepository,
    IHttpContextAccessor _httpContextAccessor) : IAuthService
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        public async Task<(User user, Branch branch)> LoginAsync(int userCode, string password)
        {
            var user = await _userRepository.GetByIdAsync(userCode);
            if (user == null) return (null, null);

            // Verify password
            var result = _passwordHasher.VerifyHashedPassword(user, user.UserPassword, password);
            if (result != PasswordVerificationResult.Success)
                return (null, null);

            // Detect branch from IP
            var clientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var branch = await FindBranchByIp(clientIp);

            return (user, branch);
        }

        private async Task<Branch> FindBranchByIp(string clientIp)
        {
            if (string.IsNullOrEmpty(clientIp)) return null;

            var branches = await _branchRepository.GetAllAsync();
            return branches.FirstOrDefault(b => b.BranchIp == clientIp);
        }

        public async Task<User> SignupAsync(User newUser, string plainPassword)
        {
            // check لو الـ UserId موجود
            var exists = await _userRepository.GetByIdAsync(newUser.UserId);
            if (exists is not null)
                throw new Exception("User already exists");

            // Hash password
            newUser.UserPassword = _passwordHasher.HashPassword(newUser, plainPassword);

            await _userRepository.AddAsync(newUser);
            await _userRepository.SaveAsync();

            return newUser;
        }

        public async Task ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new Exception("User not found");

            // تحقق من الباسورد القديم
            var result = _passwordHasher.VerifyHashedPassword(user, user.UserPassword, oldPassword);
            if (result != PasswordVerificationResult.Success)
                throw new Exception("Old password is incorrect");

            // Hash الباسورد الجديد
            user.UserPassword = _passwordHasher.HashPassword(user, newPassword);

            _userRepository.Update(user);
            await _userRepository.SaveAsync();
        }


        //public async Task<(User user, Branch branch)> LoginAsync(int userCode, string password)
        //{
        //    var user = await _userRepository.AuthenticateAsync(userCode, password);
        //    if (user == null) return (null, null);

        //    var clientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        //    var branch = await FindBranchByIp(clientIp);

        //    return (user, branch);
        //}


        //private async Task<Branch> FindBranchByIp(string clientIp)
        //{
        //    if (string.IsNullOrEmpty(clientIp)) return null;

        //    var branches = await _branchRepository.GetAllAsync();
        //    foreach (var branch in branches)
        //    {
        //        if (branch.BranchIp == clientIp)
        //            return branch;
        //    }

        //    return null;
        //}


        ////
        //public async Task<User> SignupAsync(User newUser)
        //{
        //    // check لو الـ UserId موجود
        //    var exists = await _userRepository.GetByIdAsync(newUser.UserId);
        //    if (exists is not null)
        //        throw new Exception("User already exists");

        //    await _userRepository.AddAsync(newUser);
        //    await _userRepository.SaveAsync();

        //    return newUser;
        //}






        //--------------------------------------------------

        //public async Task<(User user, Branch branch)> LoginAsync(string branchIp, int userCode, string password)
        //{
        //    var user = await _userRepository.AuthenticateAsync(userCode, password);
        //    if (user == null) return (null, null);

        //    // الأول ناخد من request dto
        //    var branch = await FindBranchByIp(branchIp);

        //    // fallback لو ال branchIp فاضي أو مش لاقي فرع
        //    if (branch == null)
        //    {
        //        var clientIp = _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
        //        branch = await FindBranchByIp(clientIp);
        //    }

        //    return (user, branch);
        //}


        //private async Task<Branch> FindBranchByIp(string branchIp)
        //{
        //    if (string.IsNullOrEmpty(branchIp)) return null;

        //    var branches = await _branchRepository.GetAllAsync();
        //    return branches.FirstOrDefault(b => b.BranchIp == branchIp);
        //}
    }
}
