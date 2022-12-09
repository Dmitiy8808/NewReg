using Client.HttpRepository;
using Dadata;
using Entities.DTOs;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Newtonsoft.Json;
using ValidationsCollection;
using System.ComponentModel.DataAnnotations;

namespace Reg.Client.Pages
{
    public partial class CreateRequest1
    {
        private RequestAbonentReadDto _request = new RequestAbonentReadDto();
        public string address { get; set; } = string.Empty;
        public string regionText { get; set; } = string.Empty;
        bool IsJuridical { get; set; } = true;
        public bool IsOrgUnit { get; set; } = false;
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        public ICompanyHttpRepository CompanyRepo { get; set; }
        public IMask innMask = new PatternMask("0000000000");
        public IMask kppMask = new PatternMask("000000000");
        public IMask ogrnMask = new PatternMask("0000000000000");
        private void OnActivePanelIndexChanged(int tabIndex)
        {
            if (tabIndex == 0)
            {
                IsJuridical = true;
            }
            else
            {
                IsJuridical = false;
            }
        }

        Dictionary<int, string> regions = new Dictionary<int, string>()
        {
            [01] = "Республика Адыгея (Адыгея)",

            [02] = "Республика Башкортостан",

            [03] = "Республика Бурятия",

            [04] = "Республика Алтай",

            [05] = "Республика Дагестан",

            [06] = "Республика Ингушетия",

            [07] = "Кабардино-Балкарская Республика",

            [08] = "Республика Калмыкия",

            [09] = "Карачаево-Черкесская Республика",

            [10] = "Республика Карелия",

            [11] = "Республика Коми",

            [12] = "Республика Марий Эл",

            [13] = "Республика Мордовия",

            [14] = "Республика Саха (Якутия)",

            [15] = "Республика Северная Осетия - Алания",

            [16] = "Республика Татарстан (Татарстан)",

            [17] = "Республика Тыва",

            [18] = "Удмуртская Республика",

            [19] = "Республика Хакасия",

            [20] = "Чеченская Республика",

            [21] = "Чувашская Республика - Чувашия",

            [22] = "Алтайский край",

            [23] = "Краснодарский край",

            [24] = "Красноярский край",

            [25] = "Приморский край",

            [26] = "Ставропольский край",

            [27] = "Хабаровский край",

            [28] = "Амурская область",

            [29] = "Архангельская область",

            [30] = "Астраханская область",

            [31] = "Белгородская область",

            [32] = "Брянская область",

            [33] = "Владимирская область",

            [34] = "Волгоградская область",

            [35] = "Вологодская область",

            [36] = "Воронежская область",

            [37] = "Ивановская область",

            [38] = "Иркутская область",

            [39] = "Калининградская область",

            [40] = "Калужская область",

            [41] = "Камчатский край",

            [42] = "Кемеровская область - Кузбасс",

            [43] = "Кировская область",

            [44] = "Костромская область",

            [45] = "Курганская область",

            [46] = "Курская область",

            [47] = "Ленинградская область",

            [48] = "Липецкая область",

            [49] = "Магаданская область",

            [50] = "Московская область",

            [51] = "Мурманская область",

            [52] = "Нижегородская область",

            [53] = "Новгородская область",

            [54] = "Новосибирская область",

            [55] = "Омская область",

            [56] = "Оренбургская область",

            [57] = "Орловская область",

            [58] = "Пензенская область",

            [59] = "Пермский край",

            [60] = "Псковская область",

            [61] = "Ростовская область",

            [62] = "Рязанская область",

            [63] = "Самарская область",

            [64] = "Саратовская область",

            [65] = "Сахалинская область",

            [66] = "Свердловская область",

            [67] = "Смоленская область",

            [68] = "Тамбовская область",

            [69] = "Тверская область",

            [70] = "Томская область",

            [71] = "Тульская область",

            [72] = "Тюменская область",

            [73] = "Ульяновская область",

            [74] = "Челябинская область",

            [75] = "Забайкальский край",

            [76] = "Ярославская область",

            [77] = "г. Москва",

            [78] = "г. Санкт-Петербург",

            [79] = "Еврейская автономная область",

            [83] = "Ненецкий автономный округ",

            [86] = "Ханты-Мансийский автономный округ - Югра",

            [87] = "Чукотский автономный округ",

            [89] = "Ямало-Ненецкий автономный округ",

            [91] = "Республика Крым",

            [92] = "г. Севастополь",

            [99] = "Иные территории, включая город и космодром Байконур"
        };

        bool ValidationLastName(string value)
        {
            return false;
        }

       
        private async Task<string> Validation(string value)
        {
            if (value == null )
            {
                return "Поле не заполнено"; 
            }
            else if (value == string.Empty)
            {
                return "Поле не заполнено"; 
            }
            else if (value.Length < 10)
            {
               return "Неверная длина ИНН"; 
            }
            else if (value.Length == 10)
            {
                if(!Validations.IsValidInnForEntity(value))
                {
                    return "Неверная контрольная сумма ИНН";
                }
                else if(Validations.IsValidInnForEntity(value))
                {
                    Console.WriteLine("Заполнен верный ИНН");
                    return await GetSuggestionPary(value);
                }
            }
            return null;
        }

        private async Task<string> GetSuggestionPary (string value)
        {
            var token = "527fb2e3f0d51051eb2819e252efbea8dfd93c9a";
            var api = new SuggestClientAsync(token);
            var result = await api.FindParty(value);
            if (result.suggestions.Count != 0)
            {
                
            
             _request.Kpp = result.suggestions.Select(x => x.data.kpp).FirstOrDefault();
            _request.Ogrn = result.suggestions.Select(x => x.data.ogrn).FirstOrDefault();
            _request.ShortName = result.suggestions.Select(x => x.data.name.short_with_opf).FirstOrDefault();
            
            _request.LocationAddressPostalCode = result.suggestions.Select(x => x.data.address.data.postal_code).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.postal_code).FirstOrDefault()}";
            bool resultOfParse = int.TryParse((result.suggestions.Select(x => x.data.address.data.region_kladr_id).FirstOrDefault().Substring(0,2)), out var region_kladr_id);
            if (resultOfParse == true)
                _request.LocationAddressRegionId = region_kladr_id;
            else
                Console.WriteLine("Не удалось распознать регион");
            regionText = regions[region_kladr_id];
            var regionWithType = result.suggestions.Select(x => x.data.address.data.region_with_type).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.region_with_type).FirstOrDefault()}";
            _request.LocationAddressArea = result.suggestions.Select(x => x.data.address.data.area).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.area).FirstOrDefault()}";
            _request.LocationAddressCity = result.suggestions.Select(x => x.data.address.data.city).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.city).FirstOrDefault()}";
            _request.LocationAddressStreet = result.suggestions.Select(x => x.data.address.data.street_with_type).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.street_with_type).FirstOrDefault()}";
            _request.LocationAddressBuilding = result.suggestions.Select(x => x.data.address.data.house).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.house_type_full).FirstOrDefault()} {result.suggestions.Select(x => x.data.address.data.house).FirstOrDefault()}";
            var floor = result.suggestions.Select(x => x.data.address.data.floor).FirstOrDefault() == null ? null : $"этаж {result.suggestions.Select(x => x.data.address.data.floor).FirstOrDefault()}";
            _request.LocationAddressBulk = result.suggestions.Select(x => x.data.address.data.block).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.block_type).FirstOrDefault()} {result.suggestions.Select(x => x.data.address.data.block).FirstOrDefault()} {floor}";
            _request.LocationAddressFlat = result.suggestions.Select(x => x.data.address.data.flat).FirstOrDefault() == null ? null : $"{result.suggestions.Select(x => x.data.address.data.flat_type_full).FirstOrDefault()} {result.suggestions.Select(x => x.data.address.data.flat).FirstOrDefault()}";
           address = string.Join(", ", _request.LocationAddressPostalCode, regionText, _request.LocationAddressArea, _request.LocationAddressCity, _request.LocationAddressStreet, _request.LocationAddressBuilding, _request.LocationAddressBulk, _request.LocationAddressFlat).Replace(", ,", ",").Trim().TrimEnd(',');
            StateHasChanged();
            return null;
            }
            else 
            {
                return "ИНН не найден в базе ЕГРЮЛ";
            }
        }


        async Task OpenDialog()
        {
            var parameters = new DialogParameters
            {
                { "PostalCode", _request.LocationAddressPostalCode},
                {"RegionId", _request.LocationAddressRegionId },
                {"RegionNames", regionText },
                {"AddressArea", _request.LocationAddressArea },
                {"City", _request.LocationAddressCity },
                {"AddressStreet", _request.LocationAddressStreet },
                {"AddressBuilding", _request.LocationAddressBuilding },
                {"AddressBulk", _request.LocationAddressBulk  },
                {"AddressFlat", _request.LocationAddressFlat  },
                {"RegionDictionary", regions },
            };

            var dialog = DialogService.Show<CreateRequestDialog>("Редактирование адреса", parameters);
            var result = await dialog.Result;
           
            var json = JsonConvert.SerializeObject(result.Data);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            if (dictionary != null)
            {
                 _request.LocationAddressPostalCode = dictionary["PostalCode"];
                _request.LocationAddressRegionId = int.Parse(dictionary["RegionId"]);
                regionText = regions[_request.LocationAddressRegionId];
                _request.LocationAddressArea = dictionary["AddressArea"];
                _request.LocationAddressCity = dictionary["City"];
                _request.LocationAddressStreet = dictionary["AddressStreet"];
                _request.LocationAddressBuilding = dictionary["AddressBuilding"];
                _request.LocationAddressBulk = dictionary["AddressBulk"];
                _request.LocationAddressFlat = dictionary["AddressFlat"];
                address = string.Join(", ", _request.LocationAddressPostalCode, regionText, _request.LocationAddressArea, _request.LocationAddressCity, _request.LocationAddressStreet, _request.LocationAddressBuilding, _request.LocationAddressBulk, _request.LocationAddressFlat).Replace(", ,", ",").Trim().TrimEnd(',');
                StateHasChanged();
            }
  
        }   


        public class PersonItem
        {
            [Required]
            [EmailAddress]
            public string Email { get; init; }
            [Required]
            public string Name { get; set; }

            public PersonItem(string email, string name)
            {
                Email = email;
                Name = name;
            }
        }

        private List<PersonItem> _persons = new List<PersonItem>();
        
        void AddPerson()
        {

            _persons.Add(new PersonItem
            (
                _request.PersonEmail,
                _request.PersonLastName
            ));

            _request.PersonEmail = string.Empty;
            _request.PersonLastName = string.Empty;

        }

        void OnDelete(PersonItem person)
        {
            _persons.RemoveAt(_persons.FindIndex(e => e.Email == person.Email));
        }

    

        // private void ValueChanged(string value)
        // {
        //     Console.WriteLine(value);
        //     var valResult = Validations.IsValidInnForEntity( value);
        //     Console.WriteLine(valResult);
        //     _request.Inn = value;
        //     _request.Kpp = partySuggest.suggestions.Where(s => s.data.inn == value).Select(x => x.data.kpp).FirstOrDefault();
        //     _request.Ogrn = partySuggest.suggestions.Where(s => s.data.inn == value).Select(x => x.data.ogrn).FirstOrDefault();
        //     _request.ShortName = partySuggest.suggestions.Where(s => s.data.inn == value).Select(x => x.data.name.short_with_opf).FirstOrDefault();
        // }



        

        // private async Task<IEnumerable<string>> SearchOrganizationByInn(string value)
        // {

        //     var token = "527fb2e3f0d51051eb2819e252efbea8dfd93c9a";
        //     var api = new SuggestClientAsync(token);
        //     // var result = await api.FindParty(value);
        //     partySuggest = await api.SuggestParty(value);
            
        //     var sugesions = partySuggest.suggestions.ToList().Select(x => x.data.inn);

        //     // var anotherSugestions = partySuggest.suggestions.ToList();
        //     // foreach (var suggestion in anotherSugestions)
        //     // {
        //     //     Console.WriteLine(suggestion.data.inn);
        //     // }

        //     //var suggestionResult = result.suggestions.Where(x => x.data.inn.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        //     // if (value.Length == 10)
        //     // {
               
        //     //     _request.Kpp = suggestionResult.Where(s => s.data.inn == value).Select(x => x.data.kpp).FirstOrDefault();
        //     //     _request.Ogrn = suggestionResult.Where(s => s.data.inn == value).Select(x => x.data.ogrn).FirstOrDefault();
        //     //     _request.ShortName = suggestionResult.Where(s => s.data.inn == value).Select(x => x.data.name.short_with_opf).FirstOrDefault();
        //     // }
            
        //     return sugesions;
        //     // return suggestionResult.Select(x => x.data.inn + " " + x.data.name.short_with_opf) ;
        //     // var companies = await CompanyRepo.GetCompanies();
            
        //     // if (string.IsNullOrEmpty(value))
        //     //      return new string[0];
        //     // return companies.Where(x => x.Inn.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Inn);
        // }
    }
}