namespace Final_project.Services
{
    internal static class SubMenuHelper
    {
        public static void ProductSubMenu()
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
                Console.WriteLine("------------------------------------------------------------");


                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option");
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuAddNewProduct();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case 2:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuChangeProductInfo();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case 3:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuDeleteProduct();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case 4:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuShowAllProducts();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case 5:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuShowAllProductsByCategory();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case 6:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuShowAllProductsByPriceRange();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    case 7:
                        Console.WriteLine("------------------------------------------------------------");
                        ProductsMenu.MenuSearchProductByName();
                        Console.WriteLine("------------------------------------------------------------");
                        break;

                    default:
                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine("Option doesn't exist");
                        Console.WriteLine("------------------------------------------------------------");
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
                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("1.Add new sale");
                Console.WriteLine("2.Return item from sale");
                Console.WriteLine("3.Delete sale ");
                Console.WriteLine("4.Show all sales");
                Console.WriteLine("5.Show all sales in selected date range");
                Console.WriteLine("6.Show all sales in selected price range");
                Console.WriteLine("7.Show all sales at selected date");
                Console.WriteLine("8.Show sales content by selected sales ID");
                Console.WriteLine("0.Return back to main menu");
                Console.WriteLine("------------------------------------------------------------");


                Console.WriteLine("Enter option");

                while (!int.TryParse(Console.ReadLine(), out option))
                {
                    Console.WriteLine("Invalid option!");
                    Console.WriteLine("Enter option again");
                    Console.WriteLine("------------------------------------------------------------");
                }

                switch (option)
                {
                    case 1:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuAddNewSale();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 2:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuReturnSaleItems();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 3:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuDeleteSale();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 4:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuListAllSales();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 5:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuListAllSalesAccordingToDateRange();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 6:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuListAllSalesAccordingToSalesValueRange();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 7:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.MenuShowSaleAccordingToSpecificDate();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    case 8:
                        Console.WriteLine("------------------------------------------------------------");
                        SalesMenu.NenuShowSaleAccordingToId();
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                    default:
                        Console.WriteLine("------------------------------------------------------------");
                        Console.WriteLine("Option doesn't exist");
                        Console.WriteLine("------------------------------------------------------------");
                        break;
                }
            }
            while (option != 0);
        }

    }






}
