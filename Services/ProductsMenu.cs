using ConsoleTables;
using Final_project.Common.Enums;
using Final_project.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Final_project.Services
{
    internal class ProductsMenu
    {
      
        public static void MenuAddNewProduct()
        {
            try
            {
                Console.WriteLine("Enter Product Name: " );
                var name=Console.ReadLine();

                Console.WriteLine("Enter Product Price: " );
                var price = decimal.Parse( Console.ReadLine());

                Console.WriteLine("Select Product Category");
                foreach (int i in Enum.GetValues(typeof(ProductCategories)))
                {
                    Console.WriteLine($" {i}");
                }
                var category= Console.ReadLine();

                Console.WriteLine("Enter the product's count: ");
                var count =int.Parse( Console.ReadLine());

                ProductsService.AddNewProduct( name, price, category, count );  

            }

            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void MenuChangeProductInfo()
        {


        }

        public static void MenuDeleteProduct() 
        { 


        }

        public static void MenuShowAllProducts()
        {
            

        }

        public static void MenuShowAllProductsByCategory()
        {


        }

        public static void MenuShowAllProductsByPriceRange()
        {


        }
        public static void MenuSearchProductByName()
        {


        }

        

    }


}
