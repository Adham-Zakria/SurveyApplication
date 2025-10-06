using AutoMapper;
using Domain.Models;
using Shared.QuestionnaireDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class QuestionnaireProfile : Profile
    {
        public QuestionnaireProfile()
        {
            CreateMap<Questionnaire, CreateQuestionnaireDto>()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department));

            CreateMap<CreateQuestionnaireDto, Questionnaire>()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.DepartmentId));

            CreateMap<Question, QuestionDto>().ReverseMap();
            CreateMap<QuestionOption, QuestionOptionDto>().ReverseMap();
            CreateMap<QuestionImage, QuestionImageDto>().ReverseMap();
            CreateMap<QuestionComment, QuestionCommentDto>().ReverseMap();
        }
    }
}
