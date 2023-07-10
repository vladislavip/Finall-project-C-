using Final_project.Common.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
