using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class PreviousQuestionnaireQuestionsDto
    {
        //public int QuestionId { get; set; }
        public string QuestionHeader { get; set; }
        public int QuestionType { get; set; }
        public List<QuestionOptionResponseDto> Options { get; set; }
        public List<QuestionImageDto> Images { get; set; }
    }
}
