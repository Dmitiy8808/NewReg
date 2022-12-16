using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Server.Profiles
{
    public class RequestFileProfile : Profile
    {
        public RequestFileProfile()
        {
            // Source --> Destination
            CreateMap<RequestFile, RequestFileReadDto>().ReverseMap();
        }
    }
}