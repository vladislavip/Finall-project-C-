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

                var table = new ConsoleTable("Sale item ID", "Sale item product", "Sales items count");

                if (saleItems.Count == 0)

                {
                    Console.WriteLine("No sales yet");
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
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }


        }
        //Show all sales from storage

        public static void GetAllSalesToTable()
        {
            try
            {
                var sales = SalesServices.GetAllSales();



                var table = new ConsoleTable("Sale ID", "Sale Value", "Sale Date", "Sale Items Count");

                if (sales.Count == 0)

                {
                    Console.WriteLine("No sales yet");
                    return;
                }



                foreach (var sale in sales)
                {

                    table.AddRow(sale.Id, sale.SaleValue, sale.SaleDate, sale.SaleItemsList.Count);
                }

                table.Write();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }



        }
        //Shows all sales from storage in table


        public static void GetAnySaleListToTable(List<Sales> list)

        {
            try
            {
                var sales = list;

                var table = new ConsoleTable("Sale ID", "Sale Date", "Sale Value", "Sale Items Count");

                if (sales.Count == 0)

                {
                    Console.WriteLine("No sales yet");
                    return;
                }


                foreach (var sale in sales)
                {
                    table.AddRow(sale.Id, sale.SaleDate, sale.SaleValue, sale.SaleItemsList.Count);
                }

                table.Write();
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
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
                    Console.WriteLine("No sales yet");
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
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }



        }
        //Any sale items list to table

        public static void ListAllSalesAccordingToTimeRange(DateTime startDate, DateTime endDate)
        {
            endDate = endDate.AddDays(1).AddSeconds(-1);

            if (startDate > endDate)
                throw new InvalidDataException("Start date can not be greater than end date!");

            var sales = SalesStorage.Sales.Where(x => x.SaleDate >= startDate && x.SaleDate <= endDate).ToList();


            SalesServices.GetAnySaleListToTable(sales);



        }



        public static void ListAllSalesAccordingToValueRange(decimal lower, decimal upper)

        {
            var sales = SalesStorage.Sales.FindAll(x => x.SaleValue >= lower && x.SaleValue <= upper ).ToList();

            SalesServices.GetAnySaleListToTable(sales);



        }

        public static void ShowSaleAccordingToSaleId(int id)
        { 
           var sales =  SalesStorage.Sales.FindAll(x=>x.Id == id).ToList();

            if (sales == null)
                throw new Exception("Sale doesnt exists");

            Console.WriteLine($"Sale with ID: {id}");
            SalesServices.GetAnySaleListToTable(sales);

        }
        
    }




}





    







