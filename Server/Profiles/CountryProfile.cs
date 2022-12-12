using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Server.Profiles
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {   // Source --> Destination
            CreateMap<CountryReadDto, Country>()
            .ForMember(
                dest => dest.Code,
                opt => opt.MapFrom(src => src.CODE)
            )
            .ForMember(
                dest => dest.CountryName,
                opt => opt.MapFrom(src => src.SHORTNAME.Substring(0, 1).ToUpper() + src.SHORTNAME.Substring(1).ToLower())
            );
        }
       
    }
}