using Microsoft.EntityFrameworkCore;
using PatikaDevHafta1Odev.Context;
using PatikaDevHafta1Odev.Entities;

namespace PatikaDevHafta1Odev.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly PatikaContext _context;

        public ProductRepository(PatikaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> AddProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateProduct(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return product;
        }


        public async Task<Product> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);

            _context.Entry(product).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            return product;
        }

    }

}
