using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServicesAbstraction;
using Shared.QuestionnaireDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuestionnaireService(
            IQuestionnaireRepository _questionnaireRepository,
            IBranchRepository _branchRepository,
            IGenericRepository<Question> _questionRepository,
            IGenericRepository<QuestionOption> _optionRepository,
            IGenericRepository<QuestionImage> _imageRepository,
            IGenericRepository<QuestionComment> _commentRepository) : IQuestionnaireService
    {

        public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        {
            var branches = await _branchRepository.GetBranchesByIdsAsync(dto.SelectedBranches);

            var questionnaire = new Questionnaire
            {
                Department = dto.DepartmentId,
                UserId = dto.UserId,
                QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),

                // Branches which got from repository
                Branches = branches,

                // Add questions with options
                Questions = dto.Questions.Select(q => new Question
                {
                    QuestionType = q.QuestionType,
                    QuestionHeader = q.QuestionHeader,
                    QuestionOptions = q.Options.Select(o => new QuestionOption
                    {
                        QuestionHeader = o.OptionHeader
                    }).ToList()
                }).ToList()
            };

            await _questionnaireRepository.AddAsync(questionnaire);
            await _questionnaireRepository.SaveAsync();

            return questionnaire;
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveAsync();
            return question;
        }

        public async Task<QuestionOption> AddOptionAsync(QuestionOption option)
        {
            await _optionRepository.AddAsync(option);
            await _optionRepository.SaveAsync();
            return option;
        }

        public async Task<QuestionImage> AddImageAsync(QuestionImage image)
        {
            await _imageRepository.AddAsync(image);
            await _imageRepository.SaveAsync();
            return image;
        }

        public async Task<QuestionComment> AddCommentAsync(QuestionComment comment)
        {
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveAsync();
            return comment;
        }

        public async Task<IEnumerable<Question>> GetQuestionsByDepartmentBranchUserAsync(int departmentId, int branchId, int userId)
        {
            return await _questionnaireRepository.GetQuestionsByDepartmentBranchUserAsync(departmentId, branchId, userId);
        }

    }
}
