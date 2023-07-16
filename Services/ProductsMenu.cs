using Final_project.Common.Enums;
using System.Collections.Specialized;

namespace Final_project.Services
{
    internal class ProductsMenu
    {

        public static void MenuAddNewProduct()
        {
            try
            {
                //Full name check
                Console.WriteLine("Enter Product Name: ");
                var name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name))
                    throw new FormatException("Name is empty!");

                //Full price check
                Console.WriteLine("Enter Product Price: ");
                var price = decimal.Parse(Console.ReadLine());
                if (price <= 0)
                    throw new FormatException("Price cannot be zero");

                //Full category check
                Console.WriteLine("Select Product Category");

                int counter = 1;
                foreach (string i in Enum.GetNames(typeof(ProductCategories)))

                {

                    Console.WriteLine($"{counter}.{i}");
                    counter++;
                }
                string category = Console.ReadLine();
                category = category.Trim();
                //Triming emnum value passed by user 



                bool isSucces = int.TryParse(category, out int result);

                if (isSucces != true || result <= 0 || result > Enum.GetNames(typeof(ProductCategories)).Length)
                {
                    throw new Exception("Wrong input of category");
                }

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
                int id = ProductsService.AddNewProduct(name.Trim(), price, parsedCategory, count);
                Console.WriteLine($"Succesfuly added product {id} to database");

                //Method calling table for showing products in storage
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
            {   //Method calling table for showing products in storage
                ProductsService.GetAllProductsToTable();


                Console.WriteLine("Select the product by ID you would like to change");
                int id = int.Parse(Console.ReadLine());  //Targeted  product ID

                //Method returning targeted by ID product
                var product = ProductsService.ReturnTargetedByIdProduct(id);


                //User Menu for changing user property
                Console.WriteLine("Select which property you would like to change");
                Console.WriteLine("1.Product Name");
                Console.WriteLine("2.Product Price");
                Console.WriteLine("3.Product Category");
                Console.WriteLine("4.Product Count");
                Console.WriteLine("5.Change one more ");
                Console.WriteLine("5.Exit");


                int option = int.Parse(Console.ReadLine());


                switch (option)
                {
                    case 1:
                        //Check of the property before set
                        Console.WriteLine("Enter New Product Name: ");
                        var name = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(name))
                            throw new FormatException("Name is empty!");
                        product.ProductName = name.Trim();
                        break;
                    case 2:
                        //Check of property before set
                        Console.WriteLine("Enter New Product Price: ");
                        var price = decimal.Parse(Console.ReadLine());
                        if (price <= 0)
                            throw new Exception("Price can't be lower or equal to zero");
                        product.Price = price;
                        break;
                    case 3:

                        Console.WriteLine("Select New Product Category: ");
                        int counter = 1;
                        foreach (string i in Enum.GetNames(typeof(ProductCategories)))

                        {
                            Console.WriteLine($"{counter}.{i}");
                            counter++;
                        }
                        var category = Console.ReadLine().Trim();
                        //Trimmed

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
                        Console.WriteLine("Exiting");
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

                int id;

                string idString =Console.ReadLine();

                bool idIsParsed=int.TryParse(idString, out id);

                if (!idIsParsed)
                    throw new Exception("Wrong ID input)");

                

                if (ProductsService.ProhibitDeletingProductThatAlreadyTransferedToSale(id)==true) 
                {
                    ProductsService.RemoveProduct(id);
                }
             
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

                int counter = 1;
                foreach (string i in Enum.GetNames(typeof(ProductCategories)))

                {

                    Console.WriteLine($"{counter}.{i}");
                    counter++;
                }
                string category = Console.ReadLine();
                category = category.Trim();
                //Triming emnum value passed by user 



                bool isSucces = int.TryParse(category, out int result);

                if (isSucces != true || result <= 0 || result > Enum.GetNames(typeof(ProductCategories)).Length)
                {
                    throw new Exception("Wrong input of category");
                }

                if (string.IsNullOrWhiteSpace(category))
                    throw new FormatException("Category is wrong!");


                bool isSuccessful
              = Enum.TryParse(typeof(ProductCategories), category, true, out object parsedCategory);

                if (!isSuccessful)
                {
                    throw new InvalidDataException("Category not found!");
                }


                //Calling method
                ProductsService.ShowProductsInCattegory(category);


            }

            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

        public static void MenuShowAllProductsByPriceRange()
        {
            try
            {
                Console.WriteLine("Enter lower price boundary");
                decimal lowerBoundary = decimal.Parse(Console.ReadLine());
                if (lowerBoundary < 0)
                    throw new Exception("Lower boundary cant be lower than 0");

                Console.WriteLine("Enter upper price boundary");
                decimal upperBoundary = decimal.Parse(Console.ReadLine());
                if (upperBoundary > int.MaxValue)
                    throw new Exception($"Upper boundary cant be higher than {int.MaxValue}");

                ProductsService.ShowProductsByPriceRange(lowerBoundary, upperBoundary);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }

        }

        public static void MenuSearchProductByName()
        {
            try
            {
                Console.WriteLine("Enter the product search keyword: ");
                string searchWord = Console.ReadLine().Trim(); // Trimmed
                if (string.IsNullOrWhiteSpace(searchWord))
                    throw new FormatException("Name is empty!");

                ProductsService.SearchProductByName(searchWord);
            }
            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

    }


}
