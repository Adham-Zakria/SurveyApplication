using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class QuestionnaireRepository : GenericRepository<Questionnaire>, IQuestionnaireRepository
    {
        private readonly SurveyAppContext _context;

        public QuestionnaireRepository(SurveyAppContext context) : base(context)
        {
            _context = context;
        }

        //public async Task<IEnumerable<Questionnaire>> GetByDepartmentAndBranchAsync(int departmentId, int branchId)
        //{
        //    return await _context.Questionnaires
        //        .Include(q => q.Questions)
        //        .Where(q => q.Department == departmentId && q.Branches.Any(b => b.BranchId == branchId))
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Question>> GetQuestionsByDepartmentBranchUserAsync(int departmentId, int branchId, int userId)
        //{
        //    return await _context.Questionnaires
        //        .Where(q => q.Department == departmentId
        //                    && q.Branches.Any(b => b.BranchId == branchId)
        //                    && q.UserId == userId)
        //        .SelectMany(q => q.Questions) // Load the questions
        //        .Include(q => q.QuestionOptions) // Load the Options
        //        .ToListAsync();
        //}

        public async Task<IEnumerable<Question>> GetQuestionsByDepartmentBranchUserAsync(int departmentId, int branchId, int userId)
        {
            // retrive all questionnaires for department & branch
            var questionnaires = await _context.Questionnaires
                .Include(q => q.Questions)
                    .ThenInclude(q => q.QuestionOptions)
                .Where(q => q.Department == departmentId && q.Branches.Any(b => b.BranchId == branchId))
                .ToListAsync();

            var allQuestions = questionnaires.SelectMany(q => q.Questions).ToList();

            // retrive the questions which the user answer
            var answeredQuestionIds = await _context.QuestionsAnswers
                .Where(a => a.UserId == userId)
                .Select(a => a.QuestionId)
                .ToListAsync();

            // retrive unanswered questions
            var unanswered = allQuestions.Where(q => !answeredQuestionIds.Contains(q.QuestionId)).ToList();

            return unanswered;
        }



    }
}
