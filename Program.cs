using Final_project.Services;

namespace Final_project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ProductsService productsService = new ProductsService();
            ProductsMenu menu = new ProductsMenu(); 
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