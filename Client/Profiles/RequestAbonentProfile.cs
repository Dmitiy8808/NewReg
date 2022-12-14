using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Client.Profiles
{
    public class RequestAbonentProfile : Profile
    {
        public RequestAbonentProfile()
        {
            // Source --> Destination
            CreateMap<RequestAbonentReadDto, RequestAbonentUpdateDto>().ReverseMap();
            CreateMap<RequestAbonent, RequestAbonentReadDto>().ReverseMap();
            // CreateMap<RequestAbonentReadDto, RequestAbonent>()
            //     .ForMember(x => x.Files, opt => opt.Ignore()).ReverseMap();
            CreateMap<RequestAbonentReadDto, RequestAbonentCreateDto>().ReverseMap();
        }
        
    }
}