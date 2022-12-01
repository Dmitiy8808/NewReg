using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Server.Profiles
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile()
        {
            CreateMap<Company, CompanyReadDto>().ReverseMap();
            CreateMap<Company, CompanyCreateDto>().ReverseMap();
        }
        
    }
}