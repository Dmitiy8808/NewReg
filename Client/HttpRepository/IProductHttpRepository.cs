using Entities.Models;

namespace Reg.Client.HttpRepository
{
    public interface IProductHttpRepository
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProduct(Guid id);
        Task CreateProduct(Product product);

    }
}