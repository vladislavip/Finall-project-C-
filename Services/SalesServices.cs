using ConsoleTables;
using Final_project.Common.Models;
using Final_project.Storage_classes;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
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
        //Show all sales from storage

        public static void GetAllSalesToTable()
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
        //Shows all sales from storage in table

        public static int ProductConversionToSalesItem(ref List<Product> products, int productId, int quantityToDeduct)

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
        // This method will control product storage when product is withdrawn for a sale (Find returns object while Where returns list)

        public static decimal SaleValueCalculator(decimal productPrice, int saleItemCount)
        {
            decimal saleValue = productPrice * saleItemCount;

            return saleValue;

        }
        //Returns sale value for setting sale instance

        public static void GetAnySaleListToTable(List<Sales> list)

        {

            var sales = list;

            var table = new ConsoleTable("Sale ID", "Sale Value", "Sale Date", "Sale Items");

            if (sales.Count == 0)

            {
                Console.WriteLine("No sales yet");
                return;
            }


            foreach (var sale in sales)
            {
                table.AddRow(sale.Id, sale.SaleValue, sale.SaleDate, sale.SaleItemsList);
            }

            table.Write();

        }
        //Any sale list to table

        public static void GetAnySaleItemsListToTable(List<SalesItems> list)
        
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
        //Any sale items list to table



        //------------------------------------Updating after sale item return methods-------------------------------------------------------------------------
        public static void UpdatingSaleValuePropertyLocatedInSalesClass(ref List<Product> productList, ref List<Sales> salesList,
            int productID, int saleSaleId, int returnProductCount)
        {

            try 
            {
                var product = productList.Find(x => x.Id == productID);
                
                var sale = salesList.Find(x => x.Id == saleSaleId);

                if (product != null)
                {
                    Console.WriteLine("Product not found");
                }
                sale.SaleValue -= returnProductCount * product.Price;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }
           
           
        }
        public static void UpdatingSaleListLocatedInSalesClass(ref List<SalesItems> listOfSaleItemsInSaleClass, int saleID, int returnProductCount)

        {

            try
            {
                var exsistingSale= listOfSaleItemsInSaleClass.Where(x => x.Id == saleID).ToList();

                if (exsistingSale != null)
                {
                    Console.WriteLine("There is no such sales");

                }

                foreach (var saleItem in exsistingSale)

                {
                    saleItem.SalesItemCount -= returnProductCount;
                    saleItem.SalesItem.ProductCount += returnProductCount;

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }
            
        }
        public static void UpdatingSalesStorageAfterReturn(ref List<Sales> sales, int saleID, int returnProductCount )
        {


            try
            {

                var exsistingSale = sales.Find(x => x.Id == saleID);

                if (exsistingSale != null)
                {
                    Console.WriteLine("There is no such sales");

                }
                foreach(var saleItem in exsistingSale.SaleItemsList )
                {
                    saleItem.SalesItemCount-= returnProductCount;
                    saleItem.SalesItem.ProductCount += returnProductCount;
                  
                }
            




            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

         }

        public static void UpdatingSaleItemsStorageAfterReturn(ref List<SalesItems> salesItems, int saleItemID, int returnProductCount)
        {

            try

            {
                var existingSaleItem = salesItems.Find(x => x.Id == saleItemID);

                if (existingSaleItem != null)
                {
                    Console.WriteLine("There is no such sales item");
                }
                existingSaleItem.SalesItem.ProductCount += returnProductCount;

                existingSaleItem.SalesItemCount -= returnProductCount;

            }

             catch (Exception ex)
               {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
               }

        }

        public static void UpdatingProductStorageAfterReturn (ref List<Product> products, int productId, int returnProductCount)
        {
            try

            {
                var existingProduct = products.Find(x => x.Id == productId);

                if (existingProduct != null)
                {
                    Console.WriteLine("There is no such product");
                }

                existingProduct.ProductCount += returnProductCount;

            }

            catch (Exception ex)
            {
                Console.WriteLine( "Error occured");
                Console.WriteLine( ex.Message);
            }

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------
    }





}
      



    

