using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class PreviousQuestionnairesDto
    {
        public int QuestionnaireId { get; set; }
        public DateOnly? QuestionnaireCreatedDate { get; set; }
        public int Department { get; set; }
        public List<PreviousQuestionnaireQuestionsDto> Questions { get; set; }
    }
}
