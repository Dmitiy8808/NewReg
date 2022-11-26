using Entities.Enums;
using Entities.Models;
using Server.Helpers;
using Server.Repository;

namespace Server.Services
{
    public class QualifiedCertificateManager : IQualifiedCertificateManager
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IRegionRepository _regionRepository;

        public QualifiedCertificateManager(IProviderRepository providerRepository, IRegionRepository regionRepository)
        {
            _providerRepository = providerRepository;
            _regionRepository = regionRepository;
        }

        private async Task SetSharedRequisites(CertStruct refCertStruct, RequestAbonent clientAbonent)
        {
            refCertStruct.Post = clientAbonent.Person.Post;
            if (clientAbonent.IsJuridical)
            {
                refCertStruct.Organisation = clientAbonent.ShortName;
                refCertStruct.InnLe = clientAbonent.Inn;
                refCertStruct.Inn = clientAbonent.Person.Inn;
                refCertStruct.Ogrn = clientAbonent.Ogrn;
                refCertStruct.ComonName = clientAbonent.ShortName;
                if (string.IsNullOrEmpty(clientAbonent.OrganisationUnit))
                return;
                refCertStruct.OrganisationUnit = clientAbonent.OrganisationUnit;
            }
            else
            {
                refCertStruct.Inn = clientAbonent.Inn;
                refCertStruct.Ogrnip = string.IsNullOrEmpty(clientAbonent.Ogrn) ? (string) null : clientAbonent.Ogrn;
                refCertStruct.ComonName = string.Format("{0} {1}{2}", (object) clientAbonent.Person.LastName.Trim(), (object) clientAbonent.Person.FirstName.Trim(), string.IsNullOrWhiteSpace(clientAbonent.Person.Patronymic) ? (object) string.Empty : (object) (" " + clientAbonent.Person.Patronymic.Trim()));
            }
        }

        private async Task SetCommonRequisites(CertStruct refCertStruct, RequestAbonent clientAbonent)
        {
            Region region = await _regionRepository.GetRegion(clientAbonent.PostalAddress.RegionId);
            string str1 = string.Empty;
            if (!string.IsNullOrWhiteSpace(clientAbonent.PostalAddress.Street))
                str1 = str1 + clientAbonent.PostalAddress.Street + " ";
            if (!string.IsNullOrWhiteSpace(clientAbonent.PostalAddress.Building))
                str1 = str1 + clientAbonent.PostalAddress.Building + " ";
            if (!string.IsNullOrWhiteSpace(clientAbonent.PostalAddress.Bulk))
                str1 = str1 + clientAbonent.PostalAddress.Bulk + " ";
            if (!string.IsNullOrWhiteSpace(clientAbonent.PostalAddress.Flat))
                str1 += clientAbonent.PostalAddress.Flat;
            if (string.IsNullOrWhiteSpace(str1))
                str1 = "0";
            string str2 = clientAbonent.PostalAddress.City == null || !(clientAbonent.PostalAddress.City.Trim() != string.Empty) ? (clientAbonent.PostalAddress.Locality == null || !(clientAbonent.PostalAddress.Locality.Trim() != "") ? "0" : clientAbonent.PostalAddress.Locality.Trim().Replace("\"", string.Empty)) : clientAbonent.PostalAddress.City.Trim().Replace("\"", string.Empty);
            if (!string.IsNullOrWhiteSpace(clientAbonent.Person.Email))
                refCertStruct.Email = clientAbonent.Person.Email;
            refCertStruct.GivenName = string.Format("{0}{1}", (object) clientAbonent.Person.FirstName, clientAbonent.Person.Patronymic == null ? (object) string.Empty : (object) (" " + clientAbonent.Person.Patronymic));
            StringHelper stringHelper = new StringHelper();
            refCertStruct.Surname = stringHelper.RemoveCertSpace(clientAbonent.Person.LastName);
            refCertStruct.Country = QualifiedCertificateVolatile.Country;
            refCertStruct.State = string.Format("{0} {1}", (object) region.RegionCode, (object) region.RegionName);
            refCertStruct.Street = str1;
            refCertStruct.Snils = stringHelper.RemoveSpaces(stringHelper.RemoveDashs(clientAbonent.Person.Snils));
            refCertStruct.Locality = str2;
            refCertStruct.IdentificationKind = new IdentificationKind?(IdentificationKind.Personal);
        }
        public async Task<CertRequestData> GetCertificateRequestData(RequestAbonent clientAbonent)
        {
            if (clientAbonent == null)
                return (CertRequestData) null;
            CertRequestData certRequestData1 = new CertRequestData();
            Provider provider = await _providerRepository.GetProvider(clientAbonent.Person.CryptoProviderId);
            int num = provider == null ? 2 : provider.ProviderType;
            string str = provider == null ? clientAbonent.Person.CryptoProviderName : provider.ProviderName;
            certRequestData1.Provider = new CertRequestDataCryptoProvider()
            {
                Code = num,
                Name = str
            };
            certRequestData1.SignTool = provider == null ? "СКЗИ \"ViPNet CSP\"" : provider.SignTool;
            certRequestData1.ContainerName = clientAbonent.Person.LastName.ToString() + " " + clientAbonent.Person.FirstName.ToString() + " " + Guid.NewGuid().ToString();
            certRequestData1.EnhKeyUsage = QualifiedCertificateVolatile.EnhKeyUsage;
            certRequestData1.CertPolicies = QualifiedCertificateVolatile.CertPolicies;
            certRequestData1.KeyUsage = QualifiedCertificateVolatile.KeyUsage;
            CertStruct certStruct = new CertStruct();
            await SetSharedRequisites(certStruct, clientAbonent);
            await SetCommonRequisites(certStruct, clientAbonent);
            certRequestData1.CertAttributes = GetCertAttributes(certStruct, clientAbonent.IsJuridical);
            certRequestData1.NotAfter = string.Empty;
            certRequestData1.NotBefore = string.Empty;
            IdentificationKind? identificationKind = certStruct.IdentificationKind;
            if (identificationKind.HasValue)
            {
                CertRequestData certRequestData2 = certRequestData1;
                identificationKind = certStruct.IdentificationKind;
                int? nullable = new int?((int) identificationKind.Value);
                certRequestData2.IdentificationKind = nullable;
            }
            return certRequestData1;
        }

        private static CertAttribute[] GetCertAttributes(CertStruct certStruct, bool isJuridical)
        {
            List<CertAttribute> certAttributes = new List<CertAttribute>();
            Action<string, string> addAttribute = (Action<string, string>) ((oid, value) => certAttributes.Add(new CertAttribute()
            {
                Oid = oid,
                Value = value
            }));
            Action<string, string> action = (Action<string, string>) ((oid, value) =>
            {
                if (value == null)
                return;
                addAttribute(oid, value);
            });
            addAttribute("2.5.4.3", certStruct.ComonName);
            addAttribute("2.5.4.4", certStruct.Surname);
            addAttribute("2.5.4.42", certStruct.GivenName);
            addAttribute("2.5.4.6", certStruct.Country);
            addAttribute("2.5.4.8", certStruct.State);
            addAttribute("2.5.4.7", certStruct.Locality);
            addAttribute("2.5.4.9", certStruct.Street);
            action("1.2.643.3.131.1.1", certStruct.Inn);
            action("1.2.643.100.4", certStruct.InnLe);
            action("2.5.4.10", certStruct.Organisation);
            action("2.5.4.11", certStruct.OrganisationUnit);
            action("2.5.4.12", certStruct.Post);
            action("1.2.643.100.5", certStruct.Ogrnip);
            if (isJuridical)
                addAttribute("1.2.643.100.1", certStruct.Ogrn ?? QualifiedCertificateVolatile.AbsentOgrn);
            addAttribute("1.2.643.100.3", certStruct.Snils ?? QualifiedCertificateVolatile.AbsentSnils);
            action("1.2.840.113549.1.9.1", certStruct.Email);
            return certAttributes.ToArray();
        }

        public string CreateRegRequest(RequestAbonent requestInfo)
        {
            return "Nrcn";
        }
    }
}