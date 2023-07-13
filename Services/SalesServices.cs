using ConsoleTables;
using Final_project.Common.Models;
using Final_project.Storage_classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

                var table = new ConsoleTable("Sale item ID", "Sale item product", "Sale item count");

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

                var table = new ConsoleTable("Sale ID", "Sale Value", "Sale Date");

                if (sales.Count == 0)

                {
                    Console.WriteLine("No sales yet");
                    return;
                }


                foreach (var sale in sales)
                {
                    table.AddRow(sale.Id, sale.SaleValue, sale.SaleDate);
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


        public static int ProductConversionToSalesItem(ref List<Product> products, int productId, int quantityToDeduct)

        {
            try
            {
                var productExsist = products.Find(x => x.Id == productId);

                if (productExsist == null)
                    throw new Exception($"Product with ID {productId} doesn't exist.");


                if (productExsist.ProductCount < quantityToDeduct)
                    throw new Exception("Not enough product");

                if (productExsist.ProductCount == quantityToDeduct)
                    Console.WriteLine($"Product {productExsist.ProductName} is out of stock");

                productExsist.ProductCount = productExsist.ProductCount - quantityToDeduct;

                return productId;
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
                return 0;
            }

        }
        // This method will control product storage when product is withdrawn for a sale (Find returns object while Where returns list)

        public static decimal SaleValueCalculator(decimal productPrice, int saleItemCount)
        {
            try
            {
                decimal saleValue = productPrice * saleItemCount;

                return saleValue;

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
                return 0;
            }


        }
        //Returns sale value for setting sale instance

        public static void GetAnySaleListToTable(List<Sales> list)

        {
            try
            {
                var sales = list;

                var table = new ConsoleTable("Sale ID", "Sale Date", "Sale Value","Sale Items Count");

                if (sales.Count == 0)

                {
                    Console.WriteLine("No sales yet");
                    return;
                }


                foreach (var sale in sales)
                {
                    table.AddRow(sale.Id, sale.SaleDate, sale.SaleValue);
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

                var table = new ConsoleTable("Sale item ID", "Sale item product", "Sale item count");

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



        //-------------------------------------------------------------Storages update method if items returned----------------------------------------------------------------------------------------

        public static void UpdateOfAllStaticStoragesAfterReturningSaleItems(ref List<Product> productList, ref List<SalesItems> salesItems, ref List<Sales> sales,
          ref List<SalesItems> listOfSaleItemsInSaleClass, int ptoductId, int salesItemId, int saleId, int returnProructCount)
        {

            try
            {  //-----------------------------------------Change on Product Storage----------------------------------------------------------------------
                var existingProduct = productList.Find(x => x.Id == ptoductId);

                if (existingProduct == null)
                {
                    Console.WriteLine("There is no such product");
                }

                existingProduct.ProductCount += returnProructCount;


                //-----------------------------------------Change on Sales Items Storage-----------------------------------------------------------------

                var existingSaleItem = salesItems.Find(x => x.Id == salesItemId);

                if (existingSaleItem == null)
                {
                    Console.WriteLine("There is no such sales item");
                }
                //existingSaleItem.SalesItem.ProductCount += returnProructCount;

                existingSaleItem.SalesItemCount -= returnProructCount;

                //----------------------------------------Change on Sale Items Storage--------------------------------------------------------------------------


                var exsistingSale = sales.Find(x => x.Id == saleId);

                if (exsistingSale == null)
                {
                    Console.WriteLine("There is no such sales");

                }
                foreach (var saleItem in exsistingSale.SaleItemsList)
                {
                    //saleItem.SalesItemCount -= returnProructCount;
                    //saleItem.SalesItem.ProductCount += returnProructCount;

                }

                exsistingSale.SaleValue -= returnProructCount * existingProduct.Price; // Updating sale Value

                //----------------------------------------Change on Sale  Class List Property --------------------------------------------------------------------------


                //var exsistingSaleProperty = listOfSaleItemsInSaleClass.Where(x => x.Id == saleId).ToList();

                //if (exsistingSale == null)
                //{
                //    Console.WriteLine("There is no such sales");

                //}

                //foreach (var saleItem in exsistingSaleProperty)

                //{
                //    saleItem.SalesItemCount -= returnProructCount;
                //    saleItem.SalesItem.ProductCount += returnProructCount;

                //}
            }

            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }
        }


        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        public static void ListAllSalesAccordingToTimeRange(DateTime startDate, DateTime endDate )
        {
            endDate = endDate.AddDays(1).AddSeconds(-1);

            if (startDate > endDate)
                throw new InvalidDataException("Start date can not be greater than end date!");

            var sales = SalesStorage.Sales.Where(x => x.SaleDate >= startDate && x.SaleDate <= endDate).ToList();


            SalesServices.GetAnySaleListToTable(sales); 
           


        }

     
        
    }
}
      



    

