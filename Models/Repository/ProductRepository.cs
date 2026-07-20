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

        public async Task<List<Product>> GetAllProductList()
        {
            // TODO: The method passes a list product from DB; 
            var products = await _context.products
                .Include(t => t.ProductCategoryes)
                .ThenInclude(a => a.Category)
                .Include(t => t.ProductKeyFeatures)
                .ThenInclude(a => a.KeyFeature)
                .Include(t => t.ProductPicturees)
                .ToListAsync();
            return products;
        }

        // Return result, where category eqwals id;
        public async Task<Category> GetMainCategory(int id)
        {
            var mainCategory=await _context.categorys.FirstOrDefaultAsync(t=>t.Id == id);
            if (mainCategory == null)
            {
                throw new Exception("No exist Data Base");
            }
            return mainCategory;
        }

        public Task<List<Category>> GetListCategoryForPage()
        {
            var ListCategory = _context.categorys.Where(cat => cat.LevelCategory == 0).ToListAsync();
            return ListCategory;
        }


    }
}
