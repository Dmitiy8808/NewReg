using AutoMapper;
using Entities.DTOs;

namespace Client.Profiles
{
    public class RequestAbonentProfile : Profile
    {
        public RequestAbonentProfile()
        {
            CreateMap<RequestAbonentReadDto, RequestAbonentUpdateDto>().ReverseMap();
        }
        
    }
}