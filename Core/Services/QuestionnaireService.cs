using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using ServicesAbstraction;
using Shared.QuestionnaireDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class QuestionnaireService(
            IQuestionnaireRepository _questionnaireRepository,
            IBranchRepository _branchRepository,
            IGenericRepository<Question> _questionRepository,
            IGenericRepository<QuestionOption> _optionRepository,
            IGenericRepository<QuestionImage> _imageRepository,
            IGenericRepository<QuestionComment> _commentRepository) : IQuestionnaireService
    {

        //public async Task<Questionnaire> CreateQuestionnaireAsync(Questionnaire questionnaire)
        //{
        //    await _questionnaireRepository.AddAsync(questionnaire);
        //    await _questionnaireRepository.SaveAsync();
        //    return questionnaire;
        //}

        //public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        //{
        //    var questionnaire = new Questionnaire
        //    {
        //        Department = dto.DepartmentId,
        //        UserId = dto.UserId,
        //        //QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.Now)
        //    };

        //    // ربط الفروع بالـ questionnaire
        //    questionnaire.Branches = dto.SelectedBranches
        //        .Select(branchId => new Branch
        //        {
        //            BranchId = branchId
        //        }).ToList();

        //    // إضافة الأسئلة
        //    questionnaire.Questions = dto.Questions.Select(q => new Question
        //    {
        //        //QuestionId = (int)q.QuestionId, // أو تولد GUID/Identity لو DB بيعمل Auto Increment
        //        QuestionType = q.QuestionType,
        //        QuestionHeader = q.QuestionHeader,
        //        QuestionOptions = q.Options.Select(o => new QuestionOption
        //        {
        //            //OptionId = (int)o.OptionId,
        //            QuestionHeader = o.OptionHeader
        //        }).ToList()
        //    }).ToList();

        //    await _questionnaireRepository.AddAsync(questionnaire);
        //    await _questionnaireRepository.SaveAsync();

        //    return questionnaire;
        //}

        //public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        //{
        //    var questionnaire = new Questionnaire
        //    {
        //        Department = dto.DepartmentId,
        //        UserId = dto.UserId,
        //        QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.Now),
        //        Questions = new List<Question>(),
        //        QuestionnairesBranches = new List<QuestionnaireBranch>()
        //    };

        //    // ربط الفروع بالـ questionnaire عبر جدول questionnaires_branches
        //    questionnaire.QuestionnairesBranches = dto.SelectedBranches
        //        .Select(branchId => new QuestionnaireBranch
        //        {
        //            BranchId = branchId
        //        }).ToList();

        //    // إضافة الأسئلة والاختيارات
        //    questionnaire.Questions = dto.Questions.Select(q => new Question
        //    {
        //        QuestionType = q.QuestionType,
        //        QuestionHeader = q.QuestionHeader,
        //        QuestionOptions = q.Options.Select(o => new QuestionOption
        //        {
        //            QuestionHeader = o.OptionHeader
        //        }).ToList()
        //    }).ToList();

        //    await _questionnaireRepository.AddAsync(questionnaire);
        //    await _questionnaireRepository.SaveAsync();

        //    return questionnaire;
        //}

        //public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        //{
        //    var questionnaire = new Questionnaire
        //    {
        //        Department = dto.DepartmentId,
        //        UserId = dto.UserId,
        //        QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),

        //        // اربط الفروع بالجدول الوسيط
        //        QuestionnairesBranches = dto.SelectedBranches.Select(branchId => new QuestionnaireBranch
        //        {
        //            BranchId = branchId
        //        }).ToList(),

        //        // أضف الأسئلة مع الاختيارات
        //        Questions = dto.Questions.Select(q => new Question
        //        {
        //            QuestionType = q.QuestionType,
        //            QuestionHeader = q.QuestionHeader,
        //            QuestionOptions = q.Options.Select(o => new QuestionOption
        //            {
        //                QuestionHeader = o.OptionHeader
        //            }).ToList()
        //        }).ToList()
        //    };

        //    await _questionnaireRepository.AddAsync(questionnaire);
        //    await _questionnaireRepository.SaveAsync();

        //    return questionnaire;
        //}


        //public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        //{
        //    var questionnaire = new Questionnaire
        //    {
        //        Department = dto.DepartmentId,
        //        UserId = dto.UserId,
        //        QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),

        //        // اربط الفروع مباشرة
        //        Branches = dto.SelectedBranches
        //            .Select(branchId => new Branch { BranchId = branchId })
        //            .ToList(),

        //        // أضف الأسئلة مع الاختيارات
        //        Questions = dto.Questions.Select(q => new Question
        //        {
        //            QuestionType = q.QuestionType,
        //            QuestionHeader = q.QuestionHeader,
        //            QuestionOptions = q.Options.Select(o => new QuestionOption
        //            {
        //                QuestionHeader = o.OptionHeader
        //            }).ToList()
        //        }).ToList()
        //    };

        //    await _questionnaireRepository.AddAsync(questionnaire);
        //    await _questionnaireRepository.SaveAsync();

        //    return questionnaire;
        //}

        //public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        //{
        //    // هات الفروع من الداتابيز بناءً على الـ Ids اللي جاية من الـ request
        //    var branches = await _context.Branches
        //        .Where(b => dto.SelectedBranches.Contains(b.BranchId))
        //        .ToListAsync();

        //    var questionnaire = new Questionnaire
        //    {
        //        Department = dto.DepartmentId,
        //        UserId = dto.UserId,
        //        QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),

        //        // اربط الفروع اللي جبتها من الداتابيز
        //        Branches = branches,

        //        // أضف الأسئلة مع الاختيارات
        //        Questions = dto.Questions.Select(q => new Question
        //        {
        //            QuestionType = q.QuestionType,
        //            QuestionHeader = q.QuestionHeader,
        //            QuestionOptions = q.Options.Select(o => new QuestionOption
        //            {
        //                QuestionHeader = o.OptionHeader
        //            }).ToList()
        //        }).ToList()
        //    };

        //    await _questionnaireRepository.AddAsync(questionnaire);
        //    await _questionnaireRepository.SaveAsync();

        //    return questionnaire;
        //}


        public async Task<Questionnaire> CreateQuestionnaireAsync(CreateQuestionnaireDto dto)
        {
            // استخدم الـ repository بدل الـ context
            var branches = await _branchRepository.GetBranchesByIdsAsync(dto.SelectedBranches);

            var questionnaire = new Questionnaire
            {
                Department = dto.DepartmentId,
                UserId = dto.UserId,
                QuestionnaireCreatedDate = DateOnly.FromDateTime(DateTime.UtcNow),

                // اربط الفروع اللي جبتها من الـ repository
                Branches = branches,

                // أضف الأسئلة مع الاختيارات
                Questions = dto.Questions.Select(q => new Question
                {
                    QuestionType = q.QuestionType,
                    QuestionHeader = q.QuestionHeader,
                    QuestionOptions = q.Options.Select(o => new QuestionOption
                    {
                        QuestionHeader = o.OptionHeader
                    }).ToList()
                }).ToList()
            };

            await _questionnaireRepository.AddAsync(questionnaire);
            await _questionnaireRepository.SaveAsync();

            return questionnaire;
        }





        public async Task<Question> AddQuestionAsync(Question question)
        {
            await _questionRepository.AddAsync(question);
            await _questionRepository.SaveAsync();
            return question;
        }

        public async Task<QuestionOption> AddOptionAsync(QuestionOption option)
        {
            await _optionRepository.AddAsync(option);
            await _optionRepository.SaveAsync();
            return option;
        }

        public async Task<QuestionImage> AddImageAsync(QuestionImage image)
        {
            await _imageRepository.AddAsync(image);
            await _imageRepository.SaveAsync();
            return image;
        }

        public async Task<QuestionComment> AddCommentAsync(QuestionComment comment)
        {
            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveAsync();
            return comment;
        }

        //public async Task<IEnumerable<Questionnaire>> GetByDepartmentAndBranchAsync(int departmentId, int branchId)
        //{
        //    return await _questionnaireRepository.GetByDepartmentAndBranchAsync(departmentId, branchId);
        //}
        public async Task<IEnumerable<Question>> GetQuestionsByDepartmentBranchUserAsync(int departmentId, int branchId, int userId)
        {
            return await _questionnaireRepository.GetQuestionsByDepartmentBranchUserAsync(departmentId, branchId, userId);
        }

    }
}
