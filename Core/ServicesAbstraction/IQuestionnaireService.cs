using Domain.Models;
using Shared.QuestionnaireDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IQuestionnaireService
    {
        Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto);
        Task<Question> AddQuestionAsync(Question question);
        Task<QuestionOption> AddOptionAsync(QuestionOption option);
        Task<QuestionImage> AddImageAsync(QuestionImage image);
        Task<QuestionComment> AddCommentAsync(QuestionComment comment);
        Task<IEnumerable<Question>> GetQuestionsByDepartmentBranchUserAsync(int departmentId, int branchId, int userId);
    }
}
