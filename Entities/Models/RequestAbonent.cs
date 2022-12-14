using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RequestAbonent
    {
        public Guid Id { get; set; }
        public string? Inn { get; set; }
        public string? Kpp { get; set; }
        public string? Ogrn { get; set; }
        public string? ShortName { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public AddressInfo? LocationAddress { get; set; }
        public PersonRequestInfo? Person { get; set; }
        public Leader? Leader { get; set; }
        public List<RequestFile> Files { get; set; } = new List<RequestFile>();
        public string? CertRequest { get; set; }
        public string? CertificationCenter { get; set; }
        public string? ContainerName { get; set; }
        public string? OrganisationUnit { get; set; }
        public bool IsJuridical { get; set; }
        public int StepId { get; set; } 
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

    }
}