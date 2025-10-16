using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IQuestionnaireRepository : IGenericRepository<Questionnaire>
    {
        Task<IEnumerable<Question>> GetQuestionsByDepartmentBranchUserAsync(int departmentId, int branchId, int userId);
        Task<IEnumerable<Questionnaire>> GetByManagerIdAsync(int userId);
    }
}
