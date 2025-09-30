using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AnswerDTOs
{
    public class AnswerResponseDto
    {
        //
        public int QuestionnaireId { get; set; }
        public int QuestionId { get; set; }
        public string QuestionHeader { get; set; }
        public string Answer { get; set; }
        public string Username { get; set; }
        //
        public int UserId { get; set; }
    }
}
