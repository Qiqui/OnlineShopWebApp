using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Entities;
using OnlineShop.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace OnlineShop.Infrastructure.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductsRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _appDbContext.Products.ToListAsync();
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await _appDbContext.Products.FirstOrDefaultAsync(product => product.Id == id);
        }
        public async Task Add(Product product)
        {
            await _appDbContext.Products.AddAsync(product);
           await _appDbContext.SaveChangesAsync();
        }

        public async Task<bool> Remove(Guid id)
        {
            var product = GetById(id);
            if (product != null)
            {
                await _appDbContext.Products.Remove(product);
                await _appDbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task Update(Guid id, Product productUpdate)
        {
            var product = GetById(id);
            if (product != null)
            {
                product.Name = productUpdate.Name;
                product.Description = productUpdate.Description;
                product.Category = productUpdate.Category;
                product.Gender = productUpdate.Gender;
                product.Brand = productUpdate.Brand;
                product.Cost = productUpdate.Cost;
                product.Material = productUpdate.Material;
                product.Color = productUpdate.Color;
                product.Collection = productUpdate.Collection;
                product.ImagePaths = productUpdate.ImagePaths;
            }

            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> Search(string keyWords)
        {
            var products = _appDbContext.Products.ToList().Where(product =>
            {
                var searchingLine = (product.Name + product.Description).Replace(" ", "").ToLower();
                var keys = keyWords.ToLower().Split();
                return keys.Any(key => searchingLine.Contains(key));
            })
            .ToList();

            return products;
        }
    }
}
