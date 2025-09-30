using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class QuestionRequestDto
    {
        //public int DepartmentId { get; set; }
        //public int BranchId { get; set; }
        //public int UserId { get; set; }


        //public long QuestionId { get; set; } // هتجيلك من الـ frontend
        public int QuestionType { get; set; }
        public string QuestionHeader { get; set; }
        public List<OptionRequestDto> Options { get; set; } = new();
    }

}
