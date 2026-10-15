using Backend_asp.net.DataBase;
using Backend_asp.net.Interface;
using Backend_asp.net.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_asp.net.Models.Repository
{
    public class ProductRepository : IRepository
    {
        // include context BD;
        #region
        private readonly AplicationContext _context;
        public ProductRepository(AplicationContext context)
        {
            _context = context;
        }
        #endregion

        // Return result, where List Main Category;
        public async Task<List<MainCategory>> GetMainCategoryFromDB()
        {
            var mainCategory = await _context.MainCategoryes.ToListAsync();
            if (mainCategory == null)
            {
                throw new Exception("No exist Data Base");
            }
            return mainCategory;
        }

        // The method passes a list product from DB; 
        public async Task<List<Product>> GetAllProductList()
        {
            var products = new List<Product>();
            products= await _context.Products
                .Include(t => t.ProductKeyFeatures).ThenInclude(at => at.KeyFeature)
                .Include(t => t.MainCategory)
                .Include(a=>a.PropertyValues)
                .ThenInclude(cat=>cat.Category)
                .Include(pict=>pict.ProductPicturees)
                .ToListAsync();
            return products;
        }

        //TODO: The method finding product by Id;
        //RU: обрати внимание, нужно чтоб этот метод возвратил пустой объект либо нет;
        public async Task<Product> GetProductById(int id)
        {
            var productId = await _context.Products
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(at => at.KeyFeature)
                .Include(t => t.MainCategory)
                .Include(a => a.PropertyValues)
                .ThenInclude(cat => cat.Category)
                .Include(pict => pict.ProductPicturees)
                .FirstOrDefaultAsync(prod => prod.Id == id);
            return productId ?? new Product { Name = "no name" };
        }
    }
}
