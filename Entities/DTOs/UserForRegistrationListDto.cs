using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class UserForRegistrationListDto
    {
       public Guid Id { get; set; }
       public List<UserForRegistrationDto> UserForRegistrationList { get; set; }
       public UserForRegistrationListDto()
       {
            Id = Guid.NewGuid();
            UserForRegistrationList = new List<UserForRegistrationDto>();
       }
    }
}