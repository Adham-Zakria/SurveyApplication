using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AnswerDTOs
{
    public class SubmitAnswerDto
    {
        public int Id { get; set; } // QuestionId
        public int QuestionType { get; set; } // 1 = MCQ, 2 = Essay
        public string Answer { get; set; } // actual answer or the OptionId
        public string? Comment { get; set; }
        public string? Image { get; set; } // reseve any base64 or any string from frontend
    }
}
