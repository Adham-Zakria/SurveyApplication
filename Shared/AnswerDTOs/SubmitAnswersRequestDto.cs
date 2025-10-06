using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AnswerDTOs
{
    public class SubmitAnswersRequestDto
    {
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public List<SubmitAnswerDto> Answers { get; set; }
    }
}
