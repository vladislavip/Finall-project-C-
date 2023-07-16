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

            //Make  SalesMenu.isTest true to enable testing mode

            if (SalesMenu.isTest)
            {

                ////Test products 
                Product product = new() { ProductName = "Coffe", Price = 1, ProductCategory = 0, ProductCount = 10 };
                Product product1 = new() { ProductName = "Tea", Price = 1, ProductCategory = 0, ProductCount = 10 };
                Product product2 = new() { ProductName = "Juice", Price = 2, ProductCategory = 0, ProductCount = 20 };
                Product product3 = new() { ProductName = "Soda", Price = 3, ProductCategory = 0, ProductCount = 40 };



                ProductsStorage.Products.Add(product);
                ProductsStorage.Products.Add(product1);
                ProductsStorage.Products.Add(product2);
                ProductsStorage.Products.Add(product3);
                //Test products

            }


            int option;

            do
            {
                Console.WriteLine("1.Do operation(s) on products");
                Console.WriteLine("2.Do operation(s) on sales");
                Console.WriteLine("0.Shutdown program");

                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option: ");
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("------------------------------------------------------------");
                        SubMenuHelper.ProductSubMenu();
                        break;
                    case 2:
                        Console.WriteLine("------------------------------------------------------------");
                        SubMenuHelper.SalesSubMenu();
                        break;
                    default:
                        Console.WriteLine("Option doesn't exist");
                        break;


                }
            }
            while (option != 0);
        }
    }
}