using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.DepartmentDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController(IDepartmentService _departmentService,IMapper _mapper) : ControllerBase
    {
        [HttpGet("AllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(departments));
        }

        [HttpGet("DepartmentUsers")]
        public async Task<IActionResult> GetDepartmentUser([FromHeader]int departmentId)
        {
            var users = await _departmentService.GetDepartmentUsersAsync(departmentId);

            if (users == null || !users.Any())
                return NotFound("No users found for this department.");

            return Ok(users);
        }
    }
}
