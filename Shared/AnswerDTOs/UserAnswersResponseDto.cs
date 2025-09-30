using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AnswerDTOs
{
    public class UserAnswersResponseDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int BranchId { get; set; }
        public List<QuestionAnswerDto> Answers { get; set; } = new();
    }
}
