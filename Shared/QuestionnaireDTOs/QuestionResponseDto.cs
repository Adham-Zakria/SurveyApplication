using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class QuestionResponseDto
    {
        public int QuestionId { get; set; }
        public int QuestionType { get; set; }
        public string QuestionHeader { get; set; }
        public List<QuestionOptionResponseDto> Options { get; set; }
    }
}
