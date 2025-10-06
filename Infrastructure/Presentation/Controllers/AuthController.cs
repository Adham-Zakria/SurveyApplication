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

    }
}
