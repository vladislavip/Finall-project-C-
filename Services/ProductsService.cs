using ConsoleTables;
using Final_project.Common.Enums;
using Final_project.Common.Models;
using Final_project.Storage_classes;

namespace Final_project.Services
{
    internal class ProductsService
    {

        public static int AddNewProduct(string productName, decimal price, object parsedCategory, int productCount)
        {
            try
            {
                var newProduct = new Product();
                {
                    newProduct.ProductName = productName.Trim();
                    newProduct.Price = price;
                    newProduct.ProductCategory = (ProductCategories)parsedCategory;
                    newProduct.ProductCount = productCount;

                }
                ProductsStorage.Products.Add(newProduct);
                return newProduct.Id;
                //return product id for final confirmation message
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
                return 0;
            }

        }

        public static List<Product> GetAllProducts()
        {
            return ProductsStorage.Products;
        }

        public static void GetAllProductsToTable()
        {
            try
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

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

        public static void RemoveProduct(int productId)
        {
            try
            {
                var existingProduct = ProductsStorage.Products.FirstOrDefault(x => x.Id == productId);

                if (existingProduct is null)
                    throw new Exception($"Product with ID {productId} not found!");

                ProductsStorage.Products = ProductsStorage.Products.Where(x => x.Id != productId).ToList();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

        public static Product ReturnTargetedByIdProduct(int productId)
        {
            try
            {
                var existingProduct = ProductsStorage.Products.FirstOrDefault(y => y.Id == productId);

                if (existingProduct is null)
                    throw new Exception($"Product with ID {productId} not found!");

                return existingProduct;

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
                return null;
            }

        }

        public static void ShowProductsInCattegory(string category)
        {
            try
            {
                bool isSuccessful
               = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

                if (!isSuccessful)
                {
                    throw new InvalidDataException("Category not found!");
                }

                var categoryList = ProductsStorage.Products.Where(y => y.ProductCategory == (ProductCategories)parsedCategory).ToList();


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

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }
        }

        public static void ShowProductsByPriceRange(decimal lower, decimal upper)
        {
            try
            {
                var found = ProductsStorage.Products.Where(x => x.Price >= lower && x.Price <= upper).ToList();


                var table = new ConsoleTable("Product Id", "Product Name", "Product Price",
                        "Product Category", "Product Count");

                if (found.Count == 0)

                {
                    Console.WriteLine("No products in following price range");
                    return;
                }


                foreach (var product in found)
                {
                    table.AddRow(product.Id, product.ProductName, product.Price,
                        product.ProductCategory, product.ProductCount);
                }

                table.Write();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }
        }

        public static void SearchProductByName(string productName)
        {
            try
            {
                var found = ProductsStorage.Products.FindAll(x => x.ProductName.ToLower().Trim() == productName.ToLower()).ToList();

                var table = new ConsoleTable("Product Id", "Product Name", "Product Price",
                        "Product Category", "Product Count");

                if (found.Count == 0)

                {
                    Console.WriteLine($"Products with Name: {productName} not found");
                    return;
                }


                foreach (var product in found)
                {
                    table.AddRow(product.Id, product.ProductName, product.Price,
                        product.ProductCategory, product.ProductCount);
                }

                table.Write();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }
        }

        public static void GetAnyProductListToTabe(List<Product> productsList)

        {
            try
            {
                var products = productsList;

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

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

    }
}
