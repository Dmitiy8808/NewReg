using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Server.Services
{
    public interface IRegistrationService
    {
        Task CreateIdentityUsersFromRequestAbonentList(RequestAbonentListDto requestAbonentCreateDtoList);
    }
}