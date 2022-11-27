using System;
using Entities.Models;

namespace Entities.DTOs
{
    public class RequestAbonentUpdateDto
    {
        public string? Inn { get; set; }

        public string? Kpp { get; set; }

        public string? Ogrn { get; set; }

        public string? ShortName { get; set; }

        public string? FullName { get; set; }

        public string? Phone { get; set; }
        public string? LocationAddressPostalCode { get; set; }

        public int LocationAddressRegionId { get; set; }

        public string? LocationAddressRegionCode { get; set; }

        public string? LocationAddressArea { get; set; }

        public string? LocationAddressCity { get; set; }

        public string? LocationAddressLocality { get; set; }

        public string? LocationAddressStreet { get; set; }

        public string? LocationAddressBuilding { get; set; }

        public string? LocationAddressBulk { get; set; }

        public string? LocationAddressFlat { get; set; }

        public string PersonLastName { get; set; }

        public string? PersonFirstName { get; set; }

        public string? PersonPatronymic { get; set; }

        public string? PersonSnils { get; set; }

        public string? PersonBirthDate { get; set; }

        public string? PersonBirthPlace { get; set; }

        public string? PersonCountry { get; set; }

        public int? PersonGender { get; set; }

        public string? PersonPost { get; set; }

        public string PersonEmail { get; set; }

        public string? PersonOrgUnitName { get; set; }

        public int? PersonPassportType { get; set; }

        public string? PersonPassportSeries { get; set; }

        public string? PersonPassportNumber { get; set; }

        public string? PersonPassportDate { get; set; }

        public string? PersonPassportAddon { get; set; }

        public string? PersonPassportUnit { get; set; }

        public int? PersonCryptoProviderId { get; set; }

        public string? PersonCryptoProviderName { get; set; }

        public string? PersonCryptoProviderCode { get; set; }

        public string? PersonInn { get; set; }

        public string? CertRequest { get; set; }

        public string? CertificationCenter { get; set; }

        public string? ContainerName { get; set; }

        public string? OrganisationUnit { get; set; }

        public bool IsJuridical { get; set; }

        //  public int? StepId { get; set; }

    }
}