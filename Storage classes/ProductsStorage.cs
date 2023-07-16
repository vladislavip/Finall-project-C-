using Final_project.Common.Models;

namespace Final_project.Storage_classes
{
    internal class ProductsStorage
    {
        public static List<Product> Products;

        public ProductsStorage()
        {
            Products = new List<Product>();
        }
    }
}
