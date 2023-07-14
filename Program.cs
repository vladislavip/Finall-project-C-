using Final_project.Common.Models;
using Final_project.Services;
using Final_project.Storage_classes;

namespace Final_project
{

    //delegate List<Product> ProductUpdateDelegate();
    
    internal class Program
    {
        static void Main(string[] args)
        {   //Instance to product members
            ProductsService productsService = new ProductsService();
            ProductsMenu menu = new ProductsMenu();


            //Instance to sales memvers
            SalesMenu salesMenu = new SalesMenu();
            SalesServices salesServices = new SalesServices();

            //Instance to storages
            ProductsStorage productsStorage = new ProductsStorage();
            SalesItemStorage salesItemStorage = new SalesItemStorage(); // remove, any sale items will be store in Sales list instance, delete class at the end
            SalesStorage salesStorage = new SalesStorage();


            ////Test products 
            //Product product = new() { Id = 0, ProductName = "w", Price = 1, ProductCategory = 0,  ProductCount = 10};
            Product product1 = new() { Id = 5, ProductName = "s", Price = 1, ProductCategory = 0, ProductCount = 10 };
            Product product2 = new() { Id = 6, ProductName = "q", Price = 2, ProductCategory = 0, ProductCount = 20 };
            Product product3 = new() { Id = 7, ProductName = "q", Price = 3, ProductCategory = 0, ProductCount = 30 };



            //ProductsStorage.Products.Add(product);
            ProductsStorage.Products.Add(product1);
            ProductsStorage.Products.Add(product2);
            ProductsStorage.Products.Add(product3);
            //Test products

            //Test Sale items

            //SalesItems salesItems = new() { Id = 0, SalesItem = product, SalesItemCount = 10 };
            //SalesItems salesItems1 = new() { Id = 1, SalesItem = product1, SalesItemCount = 20 };
            //SalesItems salesItems2 = new() { Id = 2, SalesItem = product2, SalesItemCount = 20 };

            //SalesItemStorage.SalesItems.Add(salesItems);
            //SalesItemStorage.SalesItems.Add(salesItems1);
            //SalesItemStorage.SalesItems.Add(salesItems2);
            ////Test Sale items

            //Sales sale = new() { Id = 1, SaleDate = DateTime.Now, SaleItemsList = new List<SalesItems> { salesItems, salesItems1, salesItems2 } };


            //Delegates
            //ProductUpdateDelegate productUpdateDelegate;
            //productUpdateDelegate = ProductsService.GetAllProducts();




            int option;

            do
            {
                Console.WriteLine("1.Do operation(s) on products");
                Console.WriteLine("2.Do operation(s) on sales");
                Console.WriteLine("0.Exit system");

                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option");
                }

                switch(option) 
                {
                    case 1:

                        SubMenuHelper.ProductSubMenu();
                        break;
                    case 2:
                        SubMenuHelper.SalesSubMenu();
                        break;
                    case 3:
                        Console.WriteLine("Shutting down");
                        break;
                    default:
                        Console.WriteLine("Option doesn't exist");
                        break;
                }
            }
            while (option!=0);
        }
    }
}