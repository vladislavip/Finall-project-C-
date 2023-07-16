using ConsoleTables;
using Final_project.Common.Models;
using Final_project.Storage_classes;

namespace Final_project.Services
{

    internal class SalesServices
    {
        public static List<Sales> GetAllSales()
        {
            return SalesStorage.Sales;
        }
        //Returns sales storage
        public static List<SalesItems> GetAllSaleItems()
        {
            return SalesItemStorage.SalesItems;
        }
        //Returns sales item storage
        public static void GetAllSaleItemsToTable()
        {
            try
            {

                var saleItems = SalesServices.GetAllSaleItems();

                var table = new ConsoleTable(/*"Sale ID",*/ "Sale item ID", "Sale item product", "Sales items count");

                if (saleItems.Count == 0)

                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("No Sales items yet or maybe they are all deleted due to return of products: ");
                    return;
                }



                foreach (var items in saleItems)
                {
                    table.AddRow(items.Id, items.SalesItem.ProductName, items.SalesItemCount);
                }


                table.Write();

            }

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }

        }
        //Show all sales from storage
        public static void GetAllSalesToTable()
        {
            try
            {
                var sales = SalesStorage.Sales;


                var table = new ConsoleTable("Sale Id", "Sale Value", "Sale Items Count", "Sale Item Id", "Sale Item Value", "Product Name", "Product Price", "Sale Date");

                foreach (var sale in sales)
                {
                    var list = sale.SaleItemsList;

                    foreach (var saleItem in list)
                    {
                        table.AddRow(sale.Id, sale.SaleValue, saleItem.SalesItemCount, saleItem.Id, saleItem.SalesItemCount * saleItem.SalesItem.Price, saleItem.SalesItem.ProductName, saleItem.SalesItem.Price, sale.SaleDate);

                    }
                }

                table.Write();

            }

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }



        }
        //Shows all sales from storage in table
        public static void GetAnySaleListToTable(List<Sales> list)

        {
            try
            {
                var sales = list;

                var table = new ConsoleTable("Sale Id", "Sale Value", "Sale Items Count", "Sale Item Id", "Sale Item Value", "Product Name", "Product Price", "Sale Date");

                foreach (var sale in sales)
                {
                    var listprop = sale.SaleItemsList;

                    foreach (var saleItem in listprop)
                    {
                        table.AddRow(sale.Id, sale.SaleValue, saleItem.SalesItemCount, saleItem.Id, saleItem.SalesItemCount * saleItem.SalesItem.Price, saleItem.SalesItem.ProductName, saleItem.SalesItem.Price, sale.SaleDate);
                    }
                }

                table.Write();

            }

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }


        }
        //Any sale list to table
        public static void GetAnySaleItemsListToTable(List<SalesItems> list)

        {
            try
            {


                var saleItems = list;

                var table = new ConsoleTable("Sale item ID", "Sale item product", "Sales items count");

                if (saleItems.Count == 0)

                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine("No Sales items yet or maybe they are all deleted due to return of products: ");
                    return;
                }



                foreach (var items in saleItems)
                {
                    table.AddRow(items.Id, items.SalesItem.ProductName, items.SalesItemCount);
                }



                table.Write();

            }

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }



        }
        //Any sale items list to table
        public static void ListAllSalesAccordingToTimeRange(DateTime startDate, DateTime endDate)
        {
            try
            {
                Console.WriteLine($"Your start date is: {startDate}");
                Console.WriteLine($"Your end date is: {endDate} ");

                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!");

                var sales = SalesStorage.Sales.Where(x => x.SaleDate >= startDate && x.SaleDate <= endDate).ToList();

                if (sales.Count == 0)
                    Console.WriteLine("No sales at the selected date/period: ");
                Console.WriteLine("------------------------------------------------------------");

                SalesServices.GetAnySaleListToTable(sales);
            }

            catch (Exception ex)

            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }
        }
        //Listing sale according to data range , also works with with specific date
        public static void ListAllSalesAccordingToValueRange(decimal lower, decimal upper)

        {
            try
            {
                var sales = SalesStorage.Sales.FindAll(x => x.SaleValue >= lower && x.SaleValue <= upper).ToList();

                if (sales.Count == 0)
                {
                    Console.WriteLine($"No sales betwen {lower} and {upper} price range: ");
                }

                SalesServices.GetAnySaleListToTable(sales);
            }

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }
        }
        //Sales Value range
        public static void ShowSaleAccordingToSaleId(int id)
        {
            try
            {
                var sales = SalesStorage.Sales.FindAll(x => x.Id == id).ToList();

                if (sales == null)
                {
                    throw new Exception($"Sale with id:{id} doesnt exists");
                }


                Console.WriteLine($"Sale with ID: {id}");
                SalesServices.GetAnySaleListToTable(sales);
            }

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Oops! Got an error!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }
        }

    }
}













