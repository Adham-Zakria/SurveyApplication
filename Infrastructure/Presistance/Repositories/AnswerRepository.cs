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
            return await _context.QuestionsAnswers
                 .Include(a => a.User)
                 .Include(a => a.Question)
                 .Where(a => a.UserId == userId)
                 .ToListAsync();
        }

        public async Task SaveAsync()
        {
            // for debugging
            var pending = _context.ChangeTracker.Entries()
                  .Where(e => e.State != EntityState.Unchanged)
                  .ToList();

            foreach (var entry in pending)
            {
                Console.WriteLine($"Entity: {entry.Entity.GetType().Name}, State: {entry.State}");
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<QuestionsAnswer>> GetUserAnswersAsync(int userId)
        {
            return await _context.QuestionsAnswers
                .Where(a => a.UserId == userId)
                .Include(a => a.Question)
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
