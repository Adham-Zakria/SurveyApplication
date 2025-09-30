using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Shared.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presistance.Repositories
{
    public class AnswerRepository : GenericRepository<QuestionsAnswer>, IAnswerRepository
    {
        private readonly SurveyAppContext _context;

        public AnswerRepository(SurveyAppContext context) : base(context)
        {

            _context = context;
        }

        public async Task AddAnswerAsync(QuestionsAnswer answer)
        {
            _context.QuestionsAnswers.Add(answer);
            //await _context.SaveChangesAsync();
            Console.WriteLine("Answer added: " + answer.QuestionId);
        }

        public async Task AddCommentAsync(QuestionComment comment)
        {
            await _context.QuestionComments.AddAsync(comment);
            Console.WriteLine("Comment added: " + comment.QuestionId);
        }

        public async Task AddImageAsync(QuestionImage image)
        {
            await _context.QuestionImages.AddAsync(image);
            Console.WriteLine("image added: " + image.QuestionId);
        }

        public async Task<IEnumerable<QuestionsAnswer>> GetAnswersByUserAsync(int userId)
        {
            //return await _context.QuestionsAnswers
            //    .Include(a => a.Question)
            //    .Where(a => a.UserId == userId)
            //    .ToListAsync();

            return await _context.QuestionsAnswers
                 .Include(a => a.User)
                 .Include(a => a.Question)
                 .Where(a => a.UserId == userId)
                 .ToListAsync();

            //        var query = _context.QuestionsAnswers
            //.Where(a => a.UserId == userId)
            //.Join(_context.Questionnaires,
            //    answer => answer.QuestionId,
            //    questionnaire => questionnaire.QuestionnaireId,
            //    (answer, questionnaire) => new { answer, questionnaire })
            //.Join(_context.Questions,
            //    aq => aq.answer.QuestionId,
            //    question => question.QuestionId,
            //    (aq, question) => new { aq.answer, aq.questionnaire, question })
            //.Join(_context.Users,
            //    aqq => aqq.answer.UserId,
            //    user => user.UserId,
            //    (aqq, user) => new QuestionsAnswer
            //    {
            //        QuestionId = aqq.answer.QuestionId,
            //        UserId = aqq.answer.UserId,
            //        Answer = aqq.answer.Answer,

            //        // علاقات
            //        //Questionnaire = aqq.questionnaire,
            //        Question = aqq.question,
            //        User = user
            //    });

            //        return await query.ToListAsync();
        }

        public async Task SaveAsync()
        {
            //Console.WriteLine("Saving changes to DB...");
            var pending = _context.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged)
                  .ToList();

            //Console.WriteLine("Pending entities: " + pending.Count);
            foreach (var entry in pending)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }

            await _context.SaveChangesAsync();
        }


        //public async Task<UserAnswersResponseDto?> GetUserAnswersAsync(int userId)
        //{
        //    var user = await _context.Users
        //        .Include(u => u.Answers)
        //            .ThenInclude(a => a.Question)
        //        .Include(u => u.Comments)
        //        .Include(u => u.Images)
        //        .FirstOrDefaultAsync(u => u.UserId == userId);

        //    if (user == null) return null;

        //    var dto = new UserAnswersResponseDto
        //    {
        //        UserId = user.UserId,
        //        UserName = user.UserName,
        //        BranchId = user.BranchId,
        //        Answers = user.Answers.Select(a => new QuestionAnswerDto
        //        {
        //            QuestionId = a.QuestionId,
        //            QuestionText = a.Question?.Text ?? "",
        //            Answer = a.Answer,
        //            Comment = user.Comments.FirstOrDefault(c => c.QuestionId == a.QuestionId)?.Comment,
        //            ImagePath = user.Images.FirstOrDefault(i => i.QuestionId == a.QuestionId)?.ImagePath
        //        }).ToList()
        //    };

        //    return dto;
        //}

        //public async Task<UserAnswersResponseDto?> GetUserAnswersAsync(int userId)
        //{
        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(u => u.UserId == userId);

        //    if (user == null) return null;

        //    // هجيب كل الإجابات الخاصة باليوزر
        //    var answers = await _context.QuestionsAnswers
        //        .Where(a => a.UserId == userId)
        //        .Include(a => a.Question)
        //            .ThenInclude(q => q.QuestionType)
        //        .ToListAsync();

        //    // هجيب الكومنتات الخاصة بالأسئلة دي
        //    var comments = await _context.QuestionComments
        //        .Where(c => answers.Select(a => a.QuestionId).Contains(c.QuestionId))
        //        .ToListAsync();

        //    // هجيب الصور الخاصة بالأسئلة دي
        //    var images = await _context.QuestionImages
        //        .Where(i => answers.Select(a => a.QuestionId).Contains(i.QuestionId))
        //        .ToListAsync();

        //    var dto = new UserAnswersResponseDto
        //    {
        //        UserId = user.UserId,
        //        UserName = user.UserName,
        //        BranchId = user.UserGroup, // أو BranchId لو عندك فعلاً في جدول users
        //        Answers = answers.Select(a => new QuestionAnswerDto
        //        {
        //            QuestionId = a.QuestionId,
        //            QuestionText = a.Question?.QuestionHeader ?? "",
        //            Answer = a.Answer,
        //            Comment = comments.FirstOrDefault(c => c.QuestionId == a.QuestionId)?.Comment,
        //            ImagePath = images.FirstOrDefault(i => i.QuestionId == a.QuestionId)?.ImagePath
        //        }).ToList()
        //    };

        //    return dto;
        //}

        //

        public async Task<List<QuestionsAnswer>> GetUserAnswersAsync(int userId)
        {
            return await _context.QuestionsAnswers
                .Where(a => a.UserId == userId)
                .Include(a => a.Question)
                    //.ThenInclude(q => q.QuestionType)
                    //
                    .ThenInclude(q => q.QuestionOptions) // get options' headers
                .ToListAsync();
        }

        public async Task<List<QuestionComment>> GetUserCommentsAsync(List<int> questionIds)
        {
            return await _context.QuestionComments
                .Where(c => questionIds.Contains(c.QuestionId))
                .ToListAsync();
        }

        public async Task<List<QuestionImage>> GetUserImagesAsync(List<int> questionIds)
        {
            return await _context.QuestionImages
                .Where(i => questionIds.Contains(i.QuestionId))
                .ToListAsync();
        }
    }
}
