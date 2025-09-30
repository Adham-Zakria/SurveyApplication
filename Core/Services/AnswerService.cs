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
        //public async Task SubmitAnswerAsync(QuestionsAnswer answer)
        //{
        //    await _answerRepository.AddAnswerAsync(answer);
        //}

        //public async Task<IEnumerable<QuestionsAnswer>> GetAnswersByUserAsync(int userId)
        //{
        //    return await _answerRepository.GetAnswersByUserAsync(userId);
        //}
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
                    Answer = ans.QuestionType == 1 ? ans.Answer : ans.Answer // لو اختياري يبقى OptionId كـ string
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

                //////// 3- Save Image (if provided)
                //if (ans.Image != null)
                //{
                //    var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads/questions");
                //    if (!Directory.Exists(uploadsFolder))
                //        Directory.CreateDirectory(uploadsFolder);

                //    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(ans.Image.FileName)}";
                //    var filePath = Path.Combine(uploadsFolder, fileName);

                //    using (var stream = new FileStream(filePath, FileMode.Create))
                //    {
                //        await ans.Image.CopyToAsync(stream);
                //    }

                //    var image = new QuestionImage
                //    {
                //        QuestionId = ans.Id,
                //        ImagePath = $"/uploads/questions/{fileName}"
                //    };
                //    await _answerRepository.AddImageAsync(image);
                //}

                //////// 3- Save Image (if provided as Base64 string)
                //if (!string.IsNullOrEmpty(ans.Image))
                //{
                //    //var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "questions");
                //    var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                //    var uploadsFolder = Path.Combine(rootPath, "uploads", "questions");

                //    if (!Directory.Exists(uploadsFolder))
                //        Directory.CreateDirectory(uploadsFolder);

                //    var fileName = $"{Guid.NewGuid()}.jpg"; // دايماً JPG أو حسب اللي جاي من الفرونت
                //    var filePath = Path.Combine(uploadsFolder, fileName);

                //    try
                //    {
                //        // تحويل Base64 → Bytes
                //        var imageBytes = Convert.FromBase64String(ans.Image);
                //        await File.WriteAllBytesAsync(filePath, imageBytes);

                //        var image = new QuestionImage
                //        {
                //            QuestionId = ans.Id,
                //            ImagePath = $"/uploads/questions/{fileName}" // بيتخزن في الداتا بيز
                //        };
                //        await _answerRepository.AddImageAsync(image);
                //    }
                //    catch (FormatException fex)
                //    {
                //        Console.WriteLine("Invalid Base64 for image: " + fex.Message);
                //        // ممكن تتجاهل الصورة أو ترمي Exception
                //    }
                //}

                ////// 3- Save Image (if provided as Base64)
                //if (!string.IsNullOrEmpty(ans.Image))
                //{
                //    var rootPath = _env.WebRootPath ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                //    var uploadsFolder = Path.Combine(rootPath, "uploads", "questions");

                //    if (!Directory.Exists(uploadsFolder))
                //        Directory.CreateDirectory(uploadsFolder);

                //    var fileName = $"{Guid.NewGuid()}.jpg"; // أو .png لو محتاج
                //    var filePath = Path.Combine(uploadsFolder, fileName);

                //    // حوّل Base64 لبايتات
                //    var imageBytes = Convert.FromBase64String(ans.Image);
                //    await File.WriteAllBytesAsync(filePath, imageBytes);

                //    var image = new QuestionImage
                //    {
                //        QuestionId = ans.Id,
                //        ImagePath = $"/uploads/questions/{fileName}"
                //    };
                //    await _answerRepository.AddImageAsync(image);
                //}


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

            //await _answerRepository.SaveAsync();
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


        //public async Task<UserAnswersResponseDto?> GetUserAnswersAsync(int userId)
        //{
        //    return await _answerRepository.GetUserAnswersAsync(userId);
        //}

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
                    //
                    QuestionnaireId=a.Question.QuestionnaireId,
                    QuestionType=a.Question.QuestionType,
                    QuestionText = a.Question?.QuestionHeader ?? "",

                    //Answer = a.Answer,
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


//[ApiController]
//[Route("[controller]")]

//public class uploadfileListController : Controller
//{
//    [HttpPost(Name = "uploadfileList")]
//    public bool POST(List<IFormFile> Filex, string Folder, string? FullPath)
//    {
//        try
//        {
//            string uploads = FullPath ?? Path.Combine(Directory.GetCurrentDirectory(), "upload");
//            string pathToSave = Path.Combine(uploads, Folder);
//            if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);
//            Filex.ForEach(x => {
//                string fullPath = Path.Combine(pathToSave, x.FileName);
//                using FileStream stream = new(fullPath, FileMode.Create);
//                x.CopyTo(stream);
//            });
//            return true;
//        }
//        catch (Exception)
//        {
//            return false;
//        }


//    }
//}
