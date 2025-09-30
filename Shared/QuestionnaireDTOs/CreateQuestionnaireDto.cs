using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class CreateQuestionnaireDto
    {
        public int DepartmentId { get; set; } //user department
        //public int Department { get; set; }
        //public int UserId { get; set; }

        //---------
        public int UserId { get; set; }
        //public int UserDepartment { get; set; }
        public List<int> SelectedBranches { get; set; } = new();
        public List<QuestionRequestDto> Questions { get; set; } = new();
    }
}
