// using Entities.Models;
// using Entities.RequestFeatures;
// using Microsoft.EntityFrameworkCore;
// using Reg.Server.Context;
// using Server.Paging;

// namespace Reg.Server.Repository
// {
//     public class ProductRepository : IProductRepository
//     {
//         private readonly RegContext _context;

//         public ProductRepository(RegContext context)
//         {
//             _context = context;
//         }

//         public async Task<PagedList<Product>> GetProducts(ProductParameters productParameters)
//         {
//             // // var products = await _context.Products.ToListAsync();
//             // return PagedList<Product>
//             //         .ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
//         }
        

//         public async Task<Product> GetProduct(Guid id) =>
//             await _context.Products.FirstOrDefaultAsync(p => p.Id.Equals(id));

//         public async Task CreateProduct(Product product)
//         {
//             _context.Add(product);
//             await _context.SaveChangesAsync();
//         }
//     }
// }
