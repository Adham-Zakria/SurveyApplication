using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared;
using Shared.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService _authService, IMapper _mapper) : ControllerBase
    {

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var (user, branch) = await _authService.LoginAsync(loginDto.UserId, loginDto.Password);

            if (user is null)
                return Unauthorized("Invalid credentials");

            var result = _mapper.Map<LoginResponseDto>(user);
            if (result.UserGroup == 1)
                result.IsManager = true;

             // include user's branch 
            //if (branch is not null)
            //    result.BranchId = branch.BranchId;

            return Ok(result);
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupDto)
        {
            try
            {
                var user = _mapper.Map<User>(signupDto);

                var createdUser = await _authService.SignupAsync(user, signupDto.UserPassword);

                var result = _mapper.Map<SignupResponseDto>(createdUser);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestDto dto)
        {
            try
            {
                await _authService.ChangePasswordAsync(dto.UserId, dto.OldPassword, dto.NewPassword);
                return Ok(new { message = "Password updated successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }


        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        //{
        //    var (user, branch) = await _authService.LoginAsync(loginDto.UserId, loginDto.Password);

        //    if (user is null)
        //    {
        //        //
        //        Console.WriteLine("hello");
        //        return Unauthorized("Invalid credentials");
        //    }


        //    var result = _mapper.Map<LoginResponseDto>(user);
        //    if (result.UserGroup == 1)
        //        result.IsManager = true;

        //    return Ok(result);
        //}


        ////
        //[HttpPost("signup")]
        //public async Task<IActionResult> Signup([FromBody] SignupRequestDto signupDto)
        //{
        //    try
        //    {
        //        var user = _mapper.Map<User>(signupDto);

        //        var createdUser = await _authService.SignupAsync(user);

        //        var result = _mapper.Map<SignupResponseDto>(createdUser);
        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}




        //----------------------------------------

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequest request)
        //{
        //    var (user, branch) = await _authService.LoginAsync(request.Username, request.Password);

        //    if (user == null)
        //        return Unauthorized(new { message = "Invalid username or password" });

        //    return Ok(new
        //    {
        //        user.UserId,
        //        user.UserName,
        //        //user.IsManager,
        //        Department = user.UserDepartment,
        //        Branch = branch != null ? branch.BranchName : "Unknown"
        //    });
        //}

        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        //{
        //    var (user, branch) = await _authService.LoginAsync(dto.BranchIp, dto.UserId, dto.Password);

        //    if (user == null || branch == null)
        //        return Unauthorized(new { message = "Invalid credentials or branch not found" });

        //    var response = new LoginResponseDto
        //    {
        //        UserId = user.UserId,
        //        Username = user.UserName,
        //        IsManager = user.UserGroup == 1,
        //        DepartmentId = user.UserDepartment,
        //        UserGroup = user.UserGroup,
        //        BranchId = branch.BranchId
        //    };

        //    return Ok(response);
        //}
    }


}
