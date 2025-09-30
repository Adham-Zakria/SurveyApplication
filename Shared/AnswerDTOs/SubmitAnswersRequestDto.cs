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
        //[FromForm(Name = "user_id")]
        public int UserId { get; set; }

        //[FromForm(Name = "branch_id")]
        public int BranchId { get; set; }

        //[FromForm]
        public List<SubmitAnswerDto> Answers { get; set; }
    }
}
