using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Services
{
    internal static class SubMenuHelper
    {
       public static void ProductSubMenu ()
        {
            int option;

            do
            {
             
                Console.WriteLine("1.Add new product");
                Console.WriteLine("2.Change product info");
                Console.WriteLine("3.Delete product");
                Console.WriteLine("4.Show all products");
                Console.WriteLine("5.Show products by category");
                Console.WriteLine("6.Show products by price range");
                Console.WriteLine("7.Search product by name");
                Console.WriteLine("0.Return back to main menu");


                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option)) 
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option");
                }

                switch (option)
                {
                    case 1:

                        ProductsMenu.MenuAddNewProduct();
                        break;
                    case 2:
                        
                        ProductsMenu.MenuChangeProductInfo();
                        break;
                    case 3:
                     
                        ProductsMenu.MenuDeleteProduct();
                        break;
                    case 4:
                       
                        ProductsMenu.MenuShowAllProducts(); 
                        break;
                    case 5:
                   
                        ProductsMenu.MenuShowAllProductsByCategory();
                        break;
                    case 6:
                        
                        ProductsMenu.MenuShowAllProductsByPriceRange();
                        break;
                    case 7:
                   
                        ProductsMenu.MenuSearchProductByName();
                        break;
                    
                    default:
                      
                        Console.WriteLine("Option doesn't exist");
                        break;
                }
            }
            while (option != 0);
        }

        public static void SalesSubMenu()
        {
            int option;

            do
            {
                Console.WriteLine("1.Add new sale");
                Console.WriteLine("2.Return item from sale");
                Console.WriteLine("3.Delete sale ");
                Console.WriteLine("4.Show all sales");
                Console.WriteLine("5.Show all sales in selected date range");
                Console.WriteLine("6.Show all sales in selected price range");
                Console.WriteLine("7.Show all sales at selected date");
                Console.WriteLine("8.Show sales content by selected sales ID");
                Console.WriteLine("0.Return back to main menu");


                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option)) ;
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option");
                }

                switch (option)
                {
                    case 1:
                        //
                        break;
                    case 2:
                        //
                        break;
                    case 3:
                        //
                        break;
                    case 4:
                        //
                        break;
                    case 5:
                        //
                        break;
                    case 6:
                        //
                        break;
                    case 7:
                        //
                        break;
                    case 8:
                        //
                        break;
                    case 0:
                        //
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
