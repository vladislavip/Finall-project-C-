using ConsoleTables;
using Final_project.Common.Enums;
using Final_project.Common.Models;
using Final_project.Storage_classes;
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
                foreach (string i in Enum.GetNames(typeof(ProductCategories)))
                {
                    Console.WriteLine(i);
                }
                var category= Console.ReadLine();

                Console.WriteLine("Enter the product's count: ");
                var count =int.Parse( Console.ReadLine());

                int id =ProductsService.AddNewProduct( name, price, category, count );

                Console.WriteLine($"Succesfuly added product {id} to database");

                ProductsService.GetAllProductsToTable();


            }

            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static void MenuChangeProductInfo()
        {
            try
            {

                ProductsService.GetAllProductsToTable();
                Console.WriteLine("Select the product by ID you would like to change"); 

                int id=int.Parse( Console.ReadLine());

                var product = ProductsService.ReturnTargetedByIdProduct(id);

                Console.WriteLine("Select which preoperty you would like to change");
                Console.WriteLine("1.Product Name");
                Console.WriteLine("2.Product Price");
                Console.WriteLine("3.Product Category");
                Console.WriteLine("4.Product Count");
                Console.WriteLine("5.Exit");
                int option = int.Parse( Console.ReadLine());

                switch (option) 
                { 
                 case 1:

                        Console.WriteLine("Enter New Product Name: ");
                        var name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                            throw new FormatException("Name is empty!");
                        product.ProductName = name;
                        break;
                case 2:
                        Console.WriteLine("Enter New Product Price: ");
                        var price = decimal.Parse(Console.ReadLine());
                        if (price <= 0)
                            throw new Exception("Price can't be lower or equal to zero");
                        product.Price = price;
                        break;
                case 3:
                        Console.WriteLine("Select new Product Category");
                        Console.WriteLine("Select Product Category");
                        foreach (string i in Enum.GetNames(typeof(ProductCategories)))
                        {
                            Console.WriteLine(i);
                        }
                        var category = Console.ReadLine();

                        bool isSuccessful
                 = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

                        if (!isSuccessful)
                        {
                            throw new InvalidDataException("Category not found!");
                        }

                        product.ProductCategory = (ProductCategories)parsedCategory;

                        break;
                case 4:
                        Console.WriteLine("Enter the updated product's count: ");
                        var count = int.Parse(Console.ReadLine());
                        if (count < 0)
                            throw new Exception("Count cant be lower than zero ");
                        product.ProductCount = count;   
                        break;
                 case 5:
                        Console.WriteLine("Exiting" );
                        break;
                default:
                        Console.WriteLine("Option Doesnt exist, exiting");
                        break;
                }
                ProductsService.GetAllProductsToTable();
            }

            catch (Exception ex) 
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }
            

        }

        public static void MenuDeleteProduct() 
        {
            try
            {
                ProductsService.GetAllProductsToTable();
                Console.WriteLine("Write product ID you would like to delete: ");

                int id=int.Parse( Console.ReadLine());  
                ProductsService.RemoveProduct( id );
                ProductsService.GetAllProductsToTable();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);  
            }   
        }

        public static void MenuShowAllProducts()
        {

            ProductsService.GetAllProductsToTable();

        }

        public static void MenuShowAllProductsByCategory()
        {
            try
            {
                Console.WriteLine("Select Product Category");
                foreach (string name in Enum.GetNames(typeof(ProductCategories)))
                {
                    Console.WriteLine(name);
                }
                var category = Console.ReadLine();

                ProductsService.ShowProductsInCattegory(category);


            }

            catch(Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

        public static void MenuShowAllProductsByPriceRange()
        {


        }
        public static void MenuSearchProductByName()
        {


        }

        

    }


}
