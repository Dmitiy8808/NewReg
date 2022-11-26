using Reg.Client.HttpRepository;
using Entities.Models;
using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages
{
    public partial class CreateProduct
    {
        private Product _product = new Product();
        [Inject]
        public IProductHttpRepository ProductRepo { get; set; }
        private async Task Create()
        {
            await ProductRepo.CreateProduct(_product);
        }
    }
}