﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.QuestionnaireDTOs
{
    public class QuestionnaireQuestionsRequestDto
    {
        public int UserId { get; set; }
        public int BranchId { get; set; }
        public int DepartmentId { get; set; }

    }
}
