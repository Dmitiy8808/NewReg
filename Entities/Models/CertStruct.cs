using Entities.Enums;

namespace Entities.Models
{
    public class CertStruct
    {
        public string ComonName;
        public string Surname;
        public string GivenName;
        public string Country;
        public string State;
        public string Locality;
        public string Street;
        public string Organisation;
        public string OrganisationUnit;
        public string Post;
        public string Ogrn;
        public string Ogrnip;
        public string Snils;
        public string Inn;
        public string Email;
        public string UnstructedName;
        public string RnsFss;
        public string KpFss;
        public int AbonentType;

        public string InnLe { get; set; }

        public IdentificationKind? IdentificationKind { get; set; }
    }
}