using AutoMapper;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstraction;
using Shared.QuestionnaireDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController(IQuestionnaireService _questionnaireService, IMapper _mapper) : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateQuestionnaireDto dto)
        {
            try
            {
                var created = await _questionnaireService.CreateQuestionnaireAsync(dto);
                return Ok(new { message = "Questionnaire created successfully", questionnaireId = created.QuestionnaireId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("add-question")]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto dto)
        {
            var question = _mapper.Map<Question>(dto);
            await _questionnaireService.AddQuestionAsync(question);
            return Ok(dto);
        }

        [HttpPost("add-option")]
        public async Task<IActionResult> AddOption([FromBody] QuestionOption option)
        {
            var created = await _questionnaireService.AddOptionAsync(option);
            return Ok(created);
        }

        [HttpPost("add-image")]
        public async Task<IActionResult> AddImage([FromBody] QuestionImage image)
        {
            var created = await _questionnaireService.AddImageAsync(image);
            return Ok(created);
        }

        [HttpPost("add-comment")]
        public async Task<IActionResult> AddComment([FromBody] QuestionComment comment)
        {
            var created = await _questionnaireService.AddCommentAsync(comment);
            return Ok(created);
        }
       
        [HttpGet("{departmentId}/{branchId}/{userId}")]
        public async Task<IActionResult> GetQuestions(int departmentId, int branchId, int userId)
        {
            var questions = await _questionnaireService.GetQuestionsByDepartmentBranchUserAsync(departmentId, branchId, userId);

            if (!questions.Any())
                return Ok("User has already completed this questionnaire");

            var result = _mapper.Map<IEnumerable<QuestionResponseDto>>(questions);
            return Ok(result);
        }

        [HttpPost("GetQuestions")]
        public async Task<IActionResult> GetQuestions([FromBody] QuestionnaireQuestionsRequestDto questionRequest)
        {
            var questions = await _questionnaireService
                .GetQuestionsByDepartmentBranchUserAsync(questionRequest.DepartmentId, questionRequest.BranchId, questionRequest.UserId);

            if (!questions.Any()) 
                return Ok("User has already completed this questionnaire");

            var result = _mapper.Map<IEnumerable<QuestionResponseDto>>(questions);
            return Ok(result);
        }

    }
}
