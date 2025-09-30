using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class QuestionDto
    {
        public string QuestionHeader { get; set; }
        public int QuestionType { get; set; }
        public int QuestionnaireId { get; set; }
    }
}
