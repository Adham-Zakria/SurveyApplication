using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IAnswerRepository : IGenericRepository<QuestionsAnswer>
    {
        Task AddAnswerAsync(QuestionsAnswer answer);
        Task<IEnumerable<QuestionsAnswer>> GetAnswersByUserAsync(int userId);
        Task AddImageAsync(QuestionImage image);
        Task AddCommentAsync(QuestionComment comment);
        Task<List<QuestionImage>> GetUserImagesAsync(List<int> questionIds);
        Task<List<QuestionComment>> GetUserCommentsAsync(List<int> questionIds);
        Task<List<QuestionsAnswer>> GetUserAnswersAsync(int userId);
    }
}
