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
    public class QuestionProfile : Profile
    {
        public QuestionProfile()
        {
            // Question → QuestionResponseDto
            CreateMap<Question, QuestionResponseDto>()
                .ForMember(dest => dest.Options,
                           opt => opt.MapFrom(src => src.QuestionOptions));

            // QuestionOption → QuestionOptionResponseDto
            CreateMap<QuestionOption, QuestionOptionResponseDto>()
                .ForMember(dest => dest.OptionHeader,
                           opt => opt.MapFrom(src => src.QuestionHeader));

            
        }
    }
}
