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
        public async Task<List<Product>> GetAllProductList()
        {
            // TODO: The method passes a list product from DB; 
            var products = new List<Product>();
            products= await _context.products
                .Include(t => t.ProductKeyFeatures).ThenInclude(at => at.KeyFeature)
                .Include(t => t.MainCategory)
                .Include(pict=>pict.ProductPicturees)
                .Include(a => a.ProductCategoryes).ThenInclude(ab => ab.Category)
                .ThenInclude(ac => ac!.PropertyTypeValues).ThenInclude(cb => cb.PropertyType)
                .ToListAsync();
            return products;
        }

        //public async Task<List<Category>> GetListCategoryForPage()
        //{
        //    var ListCategory = new List <Category>();
        //    return  ListCategory;
        //}

        public async Task<List<Product>> GetProductById(int id)
        {
            var products = new List<Product>();
            products = await _context.products.Where(prod=>prod.Id==id)
                .Include(t => t.ProductKeyFeatures).ThenInclude(at => at.KeyFeature)
                .Include(t => t.MainCategory)
                .Include(pict => pict.ProductPicturees)
                .Include(a => a.ProductCategoryes).ThenInclude(ab => ab.Category)
                .ThenInclude(ac => ac!.PropertyTypeValues).ThenInclude(cb => cb.PropertyType)
                .ToListAsync();
            return products;
        }
    }
}
