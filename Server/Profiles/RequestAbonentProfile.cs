using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Server.Profiles
{
    public class RequestAbonentProfile : Profile
    {
        public RequestAbonentProfile()
        {
            // CreateMap<RequestAbonentUpdateDto, RequestAbonent>();
            CreateMap<RequestAbonent, RequestAbonentUpdateDto>().ReverseMap();
            CreateMap<RequestAbonent, RequestAbonentReadDto>();
            CreateMap<RequestAbonent, RequestAbonentCreateDto>().ReverseMap();
        }
        
    }
}