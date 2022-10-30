using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Models;
using AutoMapper;

namespace API.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            // for not similar names in models
            // .ForMember(dest => dest.Id , options => options.MapFrom(src=> src.Id))

        }
    }
}