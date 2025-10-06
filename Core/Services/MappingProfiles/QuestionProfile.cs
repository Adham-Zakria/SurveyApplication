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
            // from Question to QuestionResponseDto
            CreateMap<Question, QuestionResponseDto>()
                .ForMember(dest => dest.Options,
                           opt => opt.MapFrom(src => src.QuestionOptions));

            // from QuestionOption to QuestionOptionResponseDto
            CreateMap<QuestionOption, QuestionOptionResponseDto>()
                .ForMember(dest => dest.OptionHeader,
                           opt => opt.MapFrom(src => src.QuestionHeader));

        }
    }
}
