using System.Text;
using Entities.DTOs;
using Entities.Enums;
using Entities.Models;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.X509;
using Server.Helpers;
using Server.Repository;
using bcrypto = Org.BouncyCastle.X509;

namespace Server.Services
{
    public class QualifiedCertificateManager : IQualifiedCertificateManager
    {
        private readonly IProviderRepository _providerRepository;
        private readonly IRegionRepository _regionRepository;
        private readonly IFileRepository _fileRepository;

        public QualifiedCertificateManager(IProviderRepository providerRepository, IRegionRepository regionRepository, 
            IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
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
                refCertStruct.Inn = clientAbonent.Person.Inn;
                refCertStruct.Ogrnip = string.IsNullOrEmpty(clientAbonent.Ogrn) ? (string) null : clientAbonent.Ogrn;
                refCertStruct.ComonName = string.Format("{0} {1}{2}", (object) clientAbonent.Person.LastName.Trim(), (object) clientAbonent.Person.FirstName.Trim(), string.IsNullOrWhiteSpace(clientAbonent.Person.Patronymic) ? (object) string.Empty : (object) (" " + clientAbonent.Person.Patronymic.Trim()));
            }
        }

        private async Task SetCommonRequisites(CertStruct refCertStruct, RequestAbonent clientAbonent)
        {
            Region region = await _regionRepository.GetRegion(clientAbonent.LocationAddress.RegionId);
            string str1 = string.Empty;
            if (!string.IsNullOrWhiteSpace(clientAbonent.LocationAddress.Street))
                str1 = str1 + clientAbonent.LocationAddress.Street + " ";
            if (!string.IsNullOrWhiteSpace(clientAbonent.LocationAddress.Building))
                str1 = str1 + clientAbonent.LocationAddress.Building + " ";
            if (!string.IsNullOrWhiteSpace(clientAbonent.LocationAddress.Bulk))
                str1 = str1 + clientAbonent.LocationAddress.Bulk + " ";
            if (!string.IsNullOrWhiteSpace(clientAbonent.LocationAddress.Flat))
                str1 += clientAbonent.LocationAddress.Flat;
            if (string.IsNullOrWhiteSpace(str1))
                str1 = "0";
            string str2 = clientAbonent.LocationAddress.City == null || !(clientAbonent.LocationAddress.City.Trim() != string.Empty) ? (clientAbonent.LocationAddress.Locality == null || !(clientAbonent.LocationAddress.Locality.Trim() != "") ? "0" : clientAbonent.LocationAddress.Locality.Trim().Replace("\"", string.Empty)) : clientAbonent.LocationAddress.City.Trim().Replace("\"", string.Empty);
            if (!string.IsNullOrWhiteSpace(clientAbonent.Person.Email))
                refCertStruct.Email = clientAbonent.Person.Email;
            refCertStruct.GivenName = string.Format("{0}{1}", (object) clientAbonent.Person.FirstName, clientAbonent.Person.Patronymic == null ? (object) string.Empty : (object) (" " + clientAbonent.Person.Patronymic));
            StringHelper stringHelper = new StringHelper();
            refCertStruct.Surname = stringHelper.RemoveCertSpace(clientAbonent.Person.LastName);
            refCertStruct.Country = QualifiedCertificateVolatile.Country;
            if (region != null)
            {
                 refCertStruct.State = string.Format("{0} {1}", (object) region.RegionCode, (object) region.RegionName);
            }
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
            if (certStruct.State != null)
                addAttribute("2.5.4.8", certStruct.State);
            if (certStruct.Locality != "0")
                addAttribute("2.5.4.7", certStruct.Locality);
            if (certStruct.Street != "0")
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

        private static Asn1Object GetExtensionValue(X509Certificate cert, String oid) 
        {
            byte[] bytes = cert.GetExtensionValue(new DerObjectIdentifier(oid)).GetDerEncoded();
            if (bytes == null) {
                return null;
            }
            Asn1InputStream aIn = new Asn1InputStream(new MemoryStream(bytes));
            Asn1OctetString octs = (Asn1OctetString) aIn.ReadObject();
            aIn = new Asn1InputStream(new MemoryStream(octs.GetOctets()));
            return aIn.ReadObject();
        }

        public static string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-","");
        }

        public async Task<CertificateStructureDto> GetCertificateData(Guid id)
        {
            var certStructure = new CertificateStructureDto();

            var certData = await _fileRepository.GetCertificateFileByRequestId(id);





            X509CertificateParser parser = new X509CertificateParser();
            X509Certificate cert = parser.ReadCertificate(certData.Data);

            certStructure.AuthoritySignTool = GetExtensionValue(cert, "1.2.643.100.112").ToString().Split(',')[0].TrimStart('[');
            certStructure.AuthoritySignToolCertificate = GetExtensionValue(cert, "1.2.643.100.112").ToString().Split(',')[2];
            certStructure.AuthoritySignToolCertificate = GetExtensionValue(cert, "1.2.643.100.112").ToString().Split(',')[2];
            certStructure.AuthorityCaTool = GetExtensionValue(cert, "1.2.643.100.112").ToString().Split(',')[1];
            certStructure.AuthorityCaCertificate = GetExtensionValue(cert, "1.2.643.100.112").ToString().Split(',')[3].TrimEnd(']');
            certStructure.CertSigAlgOid = cert.SigAlgOid;
            if (certStructure.CertSigAlgOid == "1.2.643.7.1.1.3.2")
                certStructure.CertAlgorithm = "ГОСТ Р 34.10-2012 256 бит";
            if (certStructure.CertSigAlgOid == "1.2.643.7.1.1.3.3")
                certStructure.CertAlgorithm = "ГОСТ Р 34.10-2012 512 бит";
            if (certStructure.CertSigAlgOid == "1.2.643.2.2.3")
                certStructure.CertAlgorithm = "ГОСТ Р 34.10-2001";
            certStructure.CertSignTool = GetExtensionValue(cert, "1.2.643.100.111").ToString();
           
            certStructure.PublicKey = cert.CertificateStructure.SubjectPublicKeyInfo.PublicKeyData.ToString();
            certStructure.Signature = ByteArrayToString(cert.GetSignature());
            certStructure.IdentificationKindCode = GetExtensionValue(cert, "1.2.643.100.114").ToString();
            if (certStructure.IdentificationKindCode == "0")
                certStructure.IdentificationKind = "При личном присутствии";
            if (certStructure.IdentificationKindCode == "1")
                certStructure.IdentificationKind = "С использованием квалифицированного сертификата";
            if (certStructure.IdentificationKindCode == "2")
                certStructure.IdentificationKind = "С использованием загранпаспорта";
            if (certStructure.IdentificationKindCode == "3")
                certStructure.IdentificationKind = "С использованием ЕБС";

            Console.WriteLine($"KeyUsage: {certStructure.Organization}");

            certStructure.SerialNumber = ByteArrayToString(cert.CertificateStructure.SerialNumber.GetDerEncoded()).ToUpper().ToCharArray()
                                        .Aggregate("",
                                        (result, c) => result += ((!string.IsNullOrEmpty(result) && (result.Length+1) % 5 == 0)
                                                                ? " " : "")
                                                                + c.ToString()
                                                    );
            certStructure.NotBefore = cert.NotBefore.ToString("dd.MM.yyyy mm:ss");
            certStructure.NotAfter = cert.NotAfter.ToString("dd.MM.yyyy mm:ss");
            certStructure.CertAlgorithm = cert.SigAlgName;
            
            DerSequence subject =  cert.SubjectDN.ToAsn1Object() as DerSequence;
            foreach (Asn1Encodable setItem in subject)
            {
                DerSet subSet = setItem as DerSet;
                if (subSet == null)
                    continue;

                // Первый элемент множества SET - искомая последовательность SEQ of {OID/value}
                DerSequence subSeq = subSet[0] as DerSequence;

                foreach (Asn1Encodable subSeqItem in subSeq)
                {
                    DerObjectIdentifier oid = subSeqItem as DerObjectIdentifier;
                    if (oid == null)
                        continue;

                    string value = subSeq[1].ToString();

                    if (oid.Id.Equals("2.5.4.4"))
                        certStructure.FirstName = value;
                    if (oid.Id.Equals("2.5.4.42"))
                        certStructure.GivenName = value;
                    if (oid.Id.Equals("1.2.643.100.3"))
                        certStructure.Snils = value;
                    if (oid.Id.Equals("1.2.643.3.131.1.1"))
                        certStructure.PersonInn = value;
                    if (oid.Id.Equals("1.2.643.3.131.1.1"))
                        certStructure.PersonInn = value;
                    if (oid.Id.Equals("1.2.840.113549.1.9.1"))
                        certStructure.Email = value;
                    if (oid.Id.Equals("1.2.840.113549.1.9.1"))
                        certStructure.Email = value;
                    if (oid.Id.Equals("2.5.4.10"))
                        certStructure.Organization = value;
                }
            }

            DerSequence issuerSubject =  cert.IssuerDN.ToAsn1Object() as DerSequence;
            foreach (Asn1Encodable setItem in issuerSubject)
            {
                DerSet subSet = setItem as DerSet;
                if (subSet == null)
                    continue;
                // Первый элемент множества SET - искомая последовательность SEQ of {OID/value}
                DerSequence subSeq = subSet[0] as DerSequence;

                foreach (Asn1Encodable subSeqItem in subSeq)
                {
                    DerObjectIdentifier oid = subSeqItem as DerObjectIdentifier;
                    if (oid == null)
                        continue;

                    string value = subSeq[1].ToString();

                    if (oid.Id.Equals("2.5.4.10"))
                        certStructure.AuthorityName = value;
                    if (oid.Id.Equals("2.5.4.9"))
                        certStructure.AuthorityAddressStreet = value;
                    if (oid.Id.Equals("2.5.4.7"))
                        certStructure.AuthorityAddressCity = value;
                }
            }

            return certStructure;
        }


    }
}