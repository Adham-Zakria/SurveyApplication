using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AnswerDTOs
{
    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        //
        public int QuestionnaireId { get; set; }
        public int QuestionType { get; set; }
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string? Comment { get; set; }
        public string? ImagePath { get; set; }
       
    }
}
