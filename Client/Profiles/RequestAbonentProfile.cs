using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Client.Profiles
{
    public class RequestAbonentProfile : Profile
    {
        public RequestAbonentProfile()
        {
            CreateMap<RequestAbonentReadDto, RequestAbonentUpdateDto>().ReverseMap();
            CreateMap<RequestAbonent, RequestAbonentReadDto>().ReverseMap();
        }
        
    }
}