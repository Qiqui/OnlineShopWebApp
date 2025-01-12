using OnlineShop.Db.Models;
using OnlineShop.Db.Interfaces;

namespace OnlineShop.Db.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProductsRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public List<Product> GetAll()
        {
            return _databaseContext.Products.ToList();
        }

        public Product TryGetById(Guid id)
        {
            return _databaseContext.Products.FirstOrDefault(product => product.Id == id);
        }
        public void Add(Product product)
        {
            _databaseContext.Products.Add(product);
            _databaseContext.SaveChanges();
        }

        public bool TryToRemove(Guid id)
        {
            var product = TryGetById(id);
            if (product != null)
            {
                _databaseContext.Products.Remove(product);
                _databaseContext.SaveChanges();
                return true;
            }

            return false;
        }

        public void Update(Guid id, Product productUpdate)
        {
            var product = TryGetById(id);
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

            _databaseContext.SaveChanges();
        }

        public List<Product> Search(string keyWords)
        {
            var products = _databaseContext.Products.ToList().Where(product =>
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
