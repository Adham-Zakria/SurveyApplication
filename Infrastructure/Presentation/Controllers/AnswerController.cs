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
        //[HttpPost("submit")]
        //public async Task<IActionResult> SubmitAnswer([FromBody] QuestionsAnswer answer)
        //{
        //    await _answerService.SubmitAnswerAsync(answer);
        //    return Ok(new { message = "Answer submitted successfully" });
        //}

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetAnswersByUser(int userId)
        //{
        //    var answers = await _answerService.GetAnswersByUserAsync(userId);
        //    return Ok(answers);
        //}


        //[HttpPost("submit")]
        //public async Task<IActionResult> SubmitAnswer([FromBody] AnswerRequestDto dto)
        //{
        //    var answer = _mapper.Map<QuestionsAnswer>(dto);
        //    await _answerService.SubmitAnswerAsync(answer);
        //    return Ok(dto);
        //}

        //[HttpGet("user/{userId}")]
        //public async Task<IActionResult> GetByUser(int userId)
        //{
        //    var answers = await _answerService.GetAnswersByUserAsync(userId);
        //    return Ok(_mapper.Map<IEnumerable<AnswerResponseDto>>(answers));
        //}



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
