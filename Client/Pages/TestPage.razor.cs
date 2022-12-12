
using Client.Service;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using Client.HttpRepository;
using Dadata;

namespace Reg.Client.Pages
{
    

    public partial class TestPage
    {
        [Parameter]
        public CertRequestDataDto certRequestDataDto { get; set; }
        [Inject]
        public IRegRequestHttpRepository? RegRequestRepo { get; set; }
        [Inject]
        public IWebSocketService WebSocketService { get; set; }
        public string? responce { get; set; }

        RequestAbonent clientAbonent = new RequestAbonent {
            Inn = "7729510210",
            Kpp = "772901001",
            Ogrn = "1047796526546",
            ShortName = "ООО \"НПЦ \"1С\"",
            FullName = "ООО \"НПЦ \"1С\"",
            Phone = "+7(495)1234567",
            // PostalAddress = new AddressInfo {
            //     PostalCode = null,
            //     RegionId = 2,
            //     City = null,
            //     Locality = "Москва г",
            //     Area = "",
            //     Street = "Мосфильмовская ул",
            //     Building = "42",
            //     Bulk = "1",
            //     Flat = "помещение 1, комната 7"
            // },
            LocationAddress = new AddressInfo {
                PostalCode = null,
                RegionId = 2,
                City = null,
                Locality = "Москва г",
                Area = "",
                Street = "Мосфильмовская ул",
                Building = "42",
                Bulk = "1",
                Flat = "помещение 1, комната 7"
            },
            Person = new PersonRequestInfo {
                LastName = "Ступин1111111111111111",
                FirstName = "Дмитрий",
                Patronymic = "Алексеевич",
                Snils = "134-795-823 41",
                BirthDate = "08.01.1970",
                BirthPlace = "г Орел",
                Country = "RUS",
                Gender = 1,
                Post = "Руководитель направления",
                Email = "test@1c.ru",
                OrgUnitName = "",
                PassportType = 1,
                PassportSeries = "1111",
                PassportNumber = "111111",
                PassportDate = "02.04.2008",
                PassportAddon = "ЖД РОВД",
                PassportUnit = "570-002",
                CryptoProviderId = 11,
                CryptoProviderName = "Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider",
                CryptoProviderCode = "80",
                Inn = "515101119012"
            },
            CertRequest = "",
            CertificationCenter = "ООО \"НПЦ \"1С\"",
            ContainerName = "",
            OrganisationUnit = "",
            IsJuridical = true
        };

        protected async override Task OnInitializedAsync()
        {
            var token = "527fb2e3f0d51051eb2819e252efbea8dfd93c9a";
            var api = new SuggestClientAsync(token);
            var result = await api.FindParty("7707083893");
            foreach (var item in result.suggestions)
            {
                Console.WriteLine(item.data.inn);
                Console.WriteLine(item.data.address.value);
            }

        }

        public void RunWebSocket()
        {
            WebSocketService.GenerateRequest(clientAbonent);
           
        }
    }
}