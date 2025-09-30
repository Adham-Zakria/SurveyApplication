using Domain.Models;
using Shared.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesAbstraction
{
    public interface IAnswerService
    {
        //Task SubmitAnswerAsync(QuestionsAnswer answer);
        //Task<IEnumerable<QuestionsAnswer>> GetAnswersByUserAsync(int userId);

        Task SubmitAnswersAsync(SubmitAnswersRequestDto dto);
        Task<UserAnswersResponseDto?> GetUserAnswersAsync(int userId);
    }
}
