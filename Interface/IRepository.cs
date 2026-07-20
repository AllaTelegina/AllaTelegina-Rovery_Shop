using Backend_asp.net.Models;
using Backend_asp.net.Models.DataBaseModel;

namespace Backend_asp.net.Interface
{
    public interface IRepository
    {
        public Task<List<Product>> GetAllProductList();
        public Task<Category> GetMainCategory(int id);

        // return list, where category equals == 0;
        public Task<List<Category>> GetListCategoryForPage();
    }
}
