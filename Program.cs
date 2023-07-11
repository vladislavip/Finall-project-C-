using Final_project.Common.Models;
using Final_project.Services;
using Final_project.Storage_classes;

namespace Final_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductsService productsService = new ProductsService();
            ProductsMenu menu = new ProductsMenu(); 
            ProductsStorage productsStorage = new ProductsStorage();
            SalesItemStorage salesItemStorage = new SalesItemStorage();
            SalesMenu salesMenu = new SalesMenu();
            SalesServices salesServices = new SalesServices();  

            

            Product product = new() { Id = 4, ProductName = "w", Price = 1, ProductCategory = 0, ProductCount = 1212 };
            Product product1 = new() { Id = 55, ProductName = "s" , Price = 100, ProductCategory = 0, ProductCount = 122312 };
            Product product2 = new() { Id = 554, ProductName = "q", Price = 200, ProductCategory = 0, ProductCount = 122312 };
            Product product3 = new() { Id = 53345, ProductName = "q", Price = 23322, ProductCategory = 0, ProductCount = 122312 };

            ProductsStorage.Products.Add(product);
            ProductsStorage.Products.Add(product1);
            ProductsStorage.Products.Add(product2);
            ProductsStorage.Products.Add(product3);


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