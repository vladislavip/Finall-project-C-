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
                Console.WriteLine("3.Read project functionality description");
                Console.WriteLine("0.Exit system");

                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option");
                }

                switch (option)
                {
                    case 1:

                        SubMenuHelper.ProductSubMenu();
                        break;
                    case 2:
                        SubMenuHelper.SalesSubMenu();
                        break;
                    case 3:
                        Console.WriteLine("1.Sales are always remain in database as the record , even if all sale items returned they will be still listed in tables and reports ,however my program doesnt allows to return anything " +
                            "from already empty sale (with 0 sale items count and 0 value");
                        Console.WriteLine("2.Sale items are deleted  if their item count==0, e.g if all products from sale item is returned , sale item deleted from storage");
                        Console.WriteLine("3.Sale items count in table (For sales table) is number of sales items per each sale , e.g   products are converted to sale items (kind of basket) and sale items count is" +
                            " the number f following basket with products");
                        Console.WriteLine("If you want test if program storages are working fine turn on SalesMenu.isTest=true ");

                        Console.WriteLine("Press any key to exit");

                        Console.ReadKey();
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