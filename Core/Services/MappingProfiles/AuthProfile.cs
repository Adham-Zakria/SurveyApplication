using AutoMapper;
using Domain.Models;
using Shared.AuthDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.MappingProfiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<User, LoginResponseDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.UserDepartment))
                .ForMember(dest => dest.IsManager, opt => opt.MapFrom(src => src.UserGroupNavigation.UserGroupName == "Manager"));


            //
            CreateMap<SignupRequestDto, User>();
            CreateMap<User, SignupResponseDto>();

        }
    }
}
