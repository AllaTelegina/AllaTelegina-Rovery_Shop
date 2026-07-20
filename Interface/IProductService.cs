using Backend_asp.net.Models;
using Backend_asp.net.Models.Class_for_views;
using Backend_asp.net.Models.Intermediate_class;

namespace Backend_asp.net.Interface
{
    public interface IProductService
    {
        // Prepares the model for submission to the presentation
        public Task <ProductsGeneral> ModelSendInViews();
        public Task <Product> JsonSerialFromJS(string StringFromJS);
    }
}
