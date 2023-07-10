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

        public static int AddNewProduct(string productName, decimal price, string category, int productCount)
        {
            if (string.IsNullOrWhiteSpace(productName))
                throw new FormatException("Name is empty!");

            if (price <= 0)
                throw new FormatException("Price cannot be zero");

            if (string.IsNullOrWhiteSpace(category))
                throw new FormatException("Category is wrong!");

            if (productCount <= 0)
                throw new FormatException("Price can't be lower than 0!");


            bool isSuccessful
                = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

            if (!isSuccessful)
            {
                throw new InvalidDataException("Category not found!");
            }


            var newProduct = new Product();
            {
                newProduct.ProductName = productName;
                newProduct.Price = price;
                newProduct.ProductCategory = (ProductCategories)parsedCategory;
                newProduct.ProductCount = productCount;

            }
            ProductsStorage.Products.Add(newProduct);
            return newProduct.Id;
        }

        public static List<Product> GetAllProducts()

        {
            return ProductsStorage.Products;
        }

        public static void GetAllProductsToTable()
        {

            var products = ProductsService.GetAllProducts();

            var table = new ConsoleTable("Product Id", "Product Name", "Product Price",
                    "Product Category", "Product Count");

            if (products.Count == 0)

            {
                Console.WriteLine("No products yet");
                return;
            }


            foreach (var product in products)
            {
                table.AddRow(product.Id, product.ProductName, product.Price,
                    product.ProductCategory, product.ProductCount);
            }

            table.Write();
        }

        public static void RemoveProduct(int productId)
        {

            var existingProduct = ProductsStorage.Products.FirstOrDefault(x => x.Id == productId);

            if (existingProduct is null)
                throw new Exception($"Product with ID {productId} not found!");

            ProductsStorage.Products = ProductsStorage.Products.Where(x => x.Id != productId).ToList();

          
        }

        public static Product ReturnTargetedByIdProduct(int productId)

        {
            var existingProduct = ProductsStorage.Products.FirstOrDefault(y => y.Id == productId);

            if (existingProduct is null)
                throw new Exception($"Product with ID {productId} not found!");


            return existingProduct;

        }

        public static void ShowProductsInCattegory(string category)
        {

            bool isSuccessful
               = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

            if (!isSuccessful)
            {
                throw new InvalidDataException("Category not found!");
            }
            
            var categoryList=ProductsStorage.Products.Where( y=> y.ProductCategory == (ProductCategories)parsedCategory).ToList();


            var table = new ConsoleTable("Product Id", "Product Name", "Product Price",
                    "Product Category", "Product Count");

            if (categoryList.Count == 0)

            {
                Console.WriteLine("No products related to this category");
                return;
            }


            foreach (var product in categoryList)
            {
                table.AddRow(product.Id, product.ProductName, product.Price,
                    product.ProductCategory, product.ProductCount);
            }

            table.Write();


        }


    } 
}
