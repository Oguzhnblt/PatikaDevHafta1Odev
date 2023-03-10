using Microsoft.AspNetCore.JsonPatch;
using PatikaDevHafta1Odev.Entities;

namespace PatikaDevHafta1Odev.Repository
{
    /// <summary>
    /// Ürün Repo Interface
    /// </summary>
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProductById(int id);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(Product product);
        Task<Product> DeleteProduct(Product product);




    }
}
