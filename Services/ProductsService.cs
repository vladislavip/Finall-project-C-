using ConsoleTables;
using Final_project.Common.Enums;
using Final_project.Common.Models;
using Final_project.Storage_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Final_project.Services
{
    internal class ProductsService
    {

        public static int AddNewProduct(string productName,decimal price,string category,int productCount )
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new FormatException("Name is empty!");

            if (price <=0)
                throw new FormatException("Price cannot be zero");

            if (string.IsNullOrWhiteSpace(category))
                throw new FormatException("Department is empty!");

            if (productCount <= 0)
                throw new FormatException("Price is lower than 0!");


            bool isSuccessful
                = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

            if (!isSuccessful)
            {
                throw new InvalidDataException("Category not found!");
            }


            var newProduct= new Product();
            {
                newProduct.ProductName= productName;
                newProduct.Price= price;
                newProduct.ProductCategory = (ProductCategories)parsedCategory; 
                newProduct.ProductCount = productCount;

            }
            ProductsStorage.Products.Add(newProduct);   
            return newProduct.Id;
        }

    }
}
