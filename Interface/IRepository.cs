using Backend_asp.net.Models;

namespace Backend_asp.net.Interface
{
    public interface IRepository
    {
        public Task<List<MainCategory>> GetMainCategoryFromDB();

        //TEMP: Check the code below;
        public Task<List<Product>> GetAllProductList();

        //// return list, where category equals == 0;
        //public Task<List<Category>> GetListCategoryForPage();
        // Returning the model at the Id;
        public Task<Product> GetProductById(int id);

        public Task<List<Category>> GetCategorysFromDB();
    }
}
