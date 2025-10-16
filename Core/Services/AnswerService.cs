using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServicesAbstraction;
using Shared.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AnswerService(IAnswerRepository _answerRepository, IWebHostEnvironment _env, IUserRepository _userRepository) : IAnswerService
    {
        public async Task SubmitAnswersAsync(SubmitAnswersRequestDto dto)
        {

            Console.WriteLine("SubmitAnswersAsync called");
            Console.WriteLine("Answers count: " + (dto.Answers?.Count() ?? 0));

            foreach (var ans in dto.Answers)
            {
                // 1- Save the Answer
                var answerEntity = new QuestionsAnswer
                {
                    QuestionId = ans.Id,
                    UserId = dto.UserId,
                    Answer = ans.QuestionType == 1 ? ans.Answer : ans.Answer,
                    BranchId = dto.BranchId
                };

                await _answerRepository.AddAnswerAsync(answerEntity);

                // 2- Save Comment
                if (!string.IsNullOrEmpty(ans.Comment))
                {
                    var comment = new QuestionComment
                    {
                        QuestionId = ans.Id,
                        Comment = ans.Comment
                    };
                    await _answerRepository.AddCommentAsync(comment);
                }

                if (!string.IsNullOrEmpty(ans.Image))
                {
                    try
                    {
                        // delete Base64 from request 
                        var cleanBase64 = ans.Image.Contains(",")
                            ? ans.Image.Split(",")[1]
                            : ans.Image;

                        var imageBytes = Convert.FromBase64String(cleanBase64);

                        //var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        var rootPath = _env.WebRootPath;
                        Console.WriteLine("WebRootPath: " + _env.WebRootPath);

                        var uploadsFolder = Path.Combine(rootPath, "uploads", "questions");

                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        var fileName = $"{Guid.NewGuid()}.jpg";
                        var filePath = Path.Combine(uploadsFolder, fileName);

                        await File.WriteAllBytesAsync(filePath, imageBytes);

                        var image = new QuestionImage
                        {
                            QuestionId = ans.Id,
                            //ImagePath = $"/uploads/questions/{fileName}"
                            ImagePath = Path.Combine("/uploads/questions", fileName).Replace("\\", "/") 
                        };
                        await _answerRepository.AddImageAsync(image);
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine("Invalid Base64 string: " + ex.Message);
                    }
                }


            }

            try
            {
                await _answerRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Save error: " + ex.Message);
                throw;
            }

        }


        public async Task<UserAnswersResponseDto?> GetUserAnswersAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null) return null;

            var answers = await _answerRepository.GetUserAnswersAsync(userId);
            var questionIds = answers.Select(a => a.QuestionId).ToList();

            var comments = await _answerRepository.GetUserCommentsAsync(questionIds);
            var images = await _answerRepository.GetUserImagesAsync(questionIds);

            return new UserAnswersResponseDto
            {
                UserId = user.UserId,
                UserName = user.UserName,
                BranchId = user.UserGroup,
                Answers = answers.Select(a => new QuestionAnswerDto
                {
                    QuestionId = a.QuestionId,
                    QuestionnaireId=a.Question.QuestionnaireId,
                    QuestionType=a.Question.QuestionType,
                    QuestionText = a.Question?.QuestionHeader ?? "",

                    //
                    BranchId = a.BranchId,
                    BranchName = a.Branch?.BranchName,
                    Answer = a.Question.QuestionType == 2
                         ? a.Answer 
                         : a.Question.QuestionOptions.FirstOrDefault(o => o.OptionId.ToString() == a.Answer)?.QuestionHeader,

                    Comment = comments.FirstOrDefault(c => c.QuestionId == a.QuestionId)?.Comment,
                    ImagePath = images.FirstOrDefault(i => i.QuestionId == a.QuestionId)?.ImagePath
                }).ToList()
            };
        }

    }
}

