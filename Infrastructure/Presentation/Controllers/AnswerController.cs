using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController(IAnswerService _answerService, IMapper _mapper) : ControllerBase
    {
        [HttpPost("submit")]
        //[Consumes("multipart/form-data")] //for photos
        public async Task<IActionResult> SubmitAnswers([FromBody] SubmitAnswersRequestDto dto)
        {
            await _answerService.SubmitAnswersAsync(dto);
            return Ok(new { message = "Answers submitted successfully" });
        }

        [HttpGet("user-answers/{userId}")]
        public async Task<IActionResult> GetUserAnswers(int userId)
        {
            var result = await _answerService.GetUserAnswersAsync(userId);
            if (result == null)
                return NotFound(new { message = "User not found or no answers submitted" });

            return Ok(result);
        }

    }
}
