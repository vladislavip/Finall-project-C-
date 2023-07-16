using Final_project.Common.Base;
using Final_project.Common.Enums;

namespace Final_project.Common.Models
{
    internal class Product : BaseEntity
    {

        private static int count = 0;

        public Product()
        {
            Id = count;
            count++;
        }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductCategories ProductCategory { get; set; }
        public int ProductCount { get; set; }

    }
}
