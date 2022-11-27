using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace Reg.Client.Components
{
    partial class RequestAbonentTable
    {
        [Parameter]
        public List<RequestAbonent> RequestAbonents { get; set; }
    }
}