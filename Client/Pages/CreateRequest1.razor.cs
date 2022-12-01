using Client.HttpRepository;
using Entities.DTOs;
using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages
{
    public partial class CreateRequest1
    {
        private RequestAbonentReadDto _request = new RequestAbonentReadDto();
        public bool Basic_Switch2 { get; set; } = true;
        bool IsJuridical { get; set; } = true;
        [Inject]
        public ICompanyHttpRepository CompanyRepo { get; set; }
        private string[] states =
    {
        "Alabama", "Alaska", "American Samoa", "Arizona",
        "Arkansas", "California", "Colorado", "Connecticut",
        "Delaware", "District of Columbia", "Federated States of Micronesia",
        "Florida", "Georgia", "Guam", "Hawaii", "Idaho",
        "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky",
        "Louisiana", "Maine", "Marshall Islands", "Maryland",
        "Massachusetts", "Michigan", "Minnesota", "Mississippi",
        "Missouri", "Montana", "Nebraska", "Nevada",
        "New Hampshire", "New Jersey", "New Mexico", "New York",
        "North Carolina", "North Dakota", "Northern Mariana Islands", "Ohio",
        "Oklahoma", "Oregon", "Palau", "Pennsylvania", "Puerto Rico",
        "Rhode Island", "South Carolina", "South Dakota", "Tennessee",
        "Texas", "Utah", "Vermont", "Virgin Island", "Virginia",
        "Washington", "West Virginia", "Wisconsin", "Wyoming",
    };


        private void OnActivePanelIndexChanged(int tabIndex)
        {
            Console.WriteLine($"Индекс таба: {tabIndex}");
            if (tabIndex == 0)
            {
                IsJuridical = true;
            }
            else
            {
                IsJuridical = false;
            }
        }

        private async Task<IEnumerable<string>> SearchOrganizationByInn(string value)
        {
            var companies = await CompanyRepo.GetCompanies();
            
            if (string.IsNullOrEmpty(value))
                 return new string[0];
            return companies.Where(x => x.Inn.Contains(value, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Inn);
        }
    }
}