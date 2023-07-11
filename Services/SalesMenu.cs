using Final_project.Common.Enums;
using Final_project.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Services
{
    internal class SalesMenu
    {

        public static void MenuAddNewSale()
        {
              //Sales item properties
                //    public Product SoldItem { get; set; }

                //public int ItemCount { get; set; }


                //----------------------------------------------------

                //Sales  properties
               //         public decimal SaleValue { get; set; }
               //public DateTime SaleDate { get; set; }

            //public List<SalesItems> SoldGoodsList;


            try
            {
                //Full name check
                ProductsService.GetAllProductsToTable();
                Console.WriteLine("Enter ID Product  that will be sold: ");
                
                
                if (string.IsNullOrWhiteSpace(name))
                    throw new FormatException("Name is empty!");

                //Full price check
                Console.WriteLine("Enter Product Price: ");
                var price = decimal.Parse(Console.ReadLine());
                if (price <= 0)
                    throw new FormatException("Price cannot be zero");

                //Full category check
                Console.WriteLine("Select Product Category");
                foreach (string i in Enum.GetNames(typeof(ProductCategories)))
                {
                    Console.WriteLine(i);
                }
                var category = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(category))
                    throw new FormatException("Category is wrong!");

                bool isSuccessful
              = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

                if (!isSuccessful)
                {
                    throw new InvalidDataException("Category not found!");
                }

                //Full count check
                Console.WriteLine("Enter the product's count: ");
                var count = int.Parse(Console.ReadLine());
                if (count <= 0)
                    throw new FormatException("Price can't be lower than 0!");

                //Calling method to create and add new product to storage
                int id = ProductsService.AddNewProduct(name, price, parsedCategory, count);
                Console.WriteLine($"Succesfuly added product {id} to database");

                //Method calling table for showing products in storage
                ProductsService.GetAllProductsToTable();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
