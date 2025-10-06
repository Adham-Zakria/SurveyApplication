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
        Task SubmitAnswersAsync(SubmitAnswersRequestDto dto);
        Task<UserAnswersResponseDto?> GetUserAnswersAsync(int userId);
    }
}
