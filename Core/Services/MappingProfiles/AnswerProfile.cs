using AutoMapper;
using Domain.Models;
using Shared.AnswerDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class AnswerProfile : Profile
    {
        public AnswerProfile()
        {
            CreateMap<QuestionsAnswer, AnswerResponseDto>()
                 .ForMember(dest => dest.QuestionHeader,
                            opt => opt.MapFrom(src => src.Question.QuestionHeader))
                 .ForMember(dest => dest.Username,
                            opt => opt.MapFrom(src => src.User.UserName))
                 .ForMember(dest => dest.QuestionnaireId,
                            opt => opt.MapFrom(src => src.Question.QuestionnaireId));

            //
            CreateMap<AnswerRequestDto, QuestionsAnswer>().ReverseMap();

        }
    }
}
