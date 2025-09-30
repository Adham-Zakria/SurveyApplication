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
        //[FromForm(Name = "id")]
        public int Id { get; set; } // QuestionId

        //[FromForm(Name = "questionType")]
        public int QuestionType { get; set; } // 1 = MCQ, 2 = Essay

        //[FromForm(Name = "answer")]
        public string Answer { get; set; } // actual answer or the OptionId

        //[FromForm(Name = "comment")]
        public string? Comment { get; set; }

        //[FromForm(Name = "image")]
        //public IFormFile? Image { get; set; } // send photo as multipart form 
        public string? Image { get; set; } // هنا هتستقبل Base64 أو أي string من الفرونت
    }
}
