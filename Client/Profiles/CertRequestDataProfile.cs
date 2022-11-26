using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Client.Profiles
{
    public class CertRequestDataProfile : Profile
    {
        public CertRequestDataProfile()
        {
            // Source --> Destination
            CreateMap<CertRequestData, CertRequestDataDto>();
        }
        
    }
}