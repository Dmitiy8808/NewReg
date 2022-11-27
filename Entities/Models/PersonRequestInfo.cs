using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class PersonRequestInfo
    {
        public Guid Id { get; set; }
        public string LastName { get; set; }

        public string? FirstName { get; set; }

        public string? Patronymic { get; set; }

        public string? Snils { get; set; }

        public string? BirthDate { get; set; }

        public string? BirthPlace { get; set; }

        public string? Country { get; set; }

        public int? Gender { get; set; }

        public string? Post { get; set; }

        public string Email { get; set; }

        public string? OrgUnitName { get; set; }

        public int? PassportType { get; set; }

        public string? PassportSeries { get; set; }

        public string? PassportNumber { get; set; }

        public string? PassportDate { get; set; }

        public string? PassportAddon { get; set; }

        public string? PassportUnit { get; set; }

        public int? CryptoProviderId { get; set; }

        public string? CryptoProviderName { get; set; }

        public string? CryptoProviderCode { get; set; }

        public string? Inn { get; set; }
    }
}