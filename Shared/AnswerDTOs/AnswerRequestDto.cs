using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.AnswerDTOs
{
    public class AnswerRequestDto
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Answer { get; set; }
    }
}
