using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.BranchDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController(IBranchService _branchService, IMapper _mapper) : ControllerBase
    {
        [HttpGet("AllBranches")]
        public async Task<IActionResult> GetAllBranches()
        {
            var branches = await _branchService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<BranchDto>>(branches));
        }
    }
}
