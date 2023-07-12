using ConsoleTables;
using Final_project.Common.Enums;
using Final_project.Common.Models;
using Final_project.Storage_classes;
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
            try
            {
                // -----------------------------Generation of SalesItem--------------------------------------------------------------------------------------
                List<SalesItems> tempList = new(); // temp list for passing several sales item objects to transfer them to sales instanse list (property)

            Start:
                Console.WriteLine("Starting conversion  of product to sale item");
                ProductsService.GetAllProductsToTable();
                Console.WriteLine("Enter (ID) Product  that will become sale item: ");

                int id = int.Parse(Console.ReadLine());

                var existingProduct = ProductsStorage.Products.Find(x => x.Id == id);

                if (existingProduct == null)
                    throw new Exception($"Product with Id {id} doesn't exist");

                Console.WriteLine($"Enter quantity of sale items to be sold from product: {existingProduct.ProductName}");
                int count=int.Parse(Console.ReadLine());

                SalesItems salesItem = new();
                {
                    salesItem.SalesItem = existingProduct;
                    salesItem.SalesItemCount = count;

                }

                SalesItemStorage.SalesItems.Add(salesItem);  //Goes to static storage
                tempList.Add(salesItem);  // goes to temp list , for damping several sales items and then moved to sale instance list*

                SalesServices.ProductConversionToSalesItem(ref ProductsStorage.Products, id, count);

                Console.WriteLine($"Product with ID: {id} successfully converted to sale item");

                Console.WriteLine("ITEMS TO BE ADDED TO SALE:");
             //-----------------------------------------------Temporary table for sale items that awaiting to be recorded to sale ------------------------------

                var tempSaleItems = tempList;

                var table = new ConsoleTable("Sale item ID", "Sale item product", "Sale item count");

                if (tempSaleItems.Count == 0)

                {
                    Console.WriteLine("No sale items yet");
                    return;
                }


                foreach (var items in tempSaleItems)
                {
                    table.AddRow(items.Id, items.SalesItem.ProductName, items.SalesItemCount);
                }

                table.Write();

            //---------------------Need to add convert more products to sale item before adding them to sale?------------------------------------------------

            Repeat:
                Console.WriteLine("Do you wish to generate more sale items before proceeding sale ?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");


                int selection = int.Parse(Console.ReadLine());  

                switch (selection)
                {
                   case 1:
                       goto Start;
                   case 2: 
                        goto End;
                    default:
                        goto Repeat;
                }

                End:
            // -----------------------------Generation of Sale----------------------------------------------------------------------------------------------

                Console.WriteLine("Starting generating of sale");


                Sales sale = new();
                {
                    sale.SaleValue = SalesServices.SaleValueCalculator(existingProduct.Price, count);
                    sale.SaleDate = DateTime.Now;
                    sale.SaleItemsList = new();  

                }


                sale.SaleItemsList = tempList;
                //passing temporary records from templist to instance list that further will be recorded to static storage*

                SalesStorage.Sales.Add(sale);
                 //adding to static Sales storage , saved!

                Console.WriteLine($"Sale with ID {sale.Id} succesfuly added to storage");

                SalesServices.GetAllSalesToTable();
                //-------------------------------------------------------------------------------------------------------------------------------------------

            }

            catch (Exception ex) 
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }

        public static void MenuReturnSaleItems()
        {
             //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Finding the sale~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~---------------------------------------
            try
            {
                var sales = SalesServices.GetAllSales ();
                // Looking for sales from static storage

                if (sales == null) 
                {
                    Console.WriteLine("Sales list is empty");
                }

                SalesServices.GetAllSalesToTable();
                //Table view of sales 

                Console.WriteLine("Enter Id of sale  from which you would like to return sale items");

                int saleId = int.Parse(Console.ReadLine());           // Take parameter to update method

                var selectedSale= sales.Find(x => x.Id == saleId);

                if (selectedSale == null)
                    throw new Exception($"Sale whith Id: {saleId} doesn't exist ");

                //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Finding the sale item~~~~~~~~~~~~~~~~~~~~~~~---------------------------------------

                var saleItemsInFoundSale= selectedSale.SaleItemsList.ToList();
                //Getting list of sale items

                SalesServices.GetAnySaleItemsListToTable(saleItemsInFoundSale);

                Console.WriteLine("Select the Sale item ID you want return to product storage");

                int saleItemId= int.Parse(Console.ReadLine());          // Take parameter to update method

                var selectedSaleItemList= saleItemsInFoundSale.Where(x => x.Id == saleItemId).ToList();
                //Got the tageted sale item in form of list for passing to table 

                var selectedSaleItemObject= saleItemsInFoundSale.Find(x=>x.Id == saleItemId);
                //Got the targeted sale item in form of object for moving it further to return

                Console.WriteLine($"Your selected sale item is {saleItemId}, it will be now returned back to product stock: ");
                SalesServices.GetAnySaleItemsListToTable(selectedSaleItemList);


                //------------------------------Returning product to products storage------------------------------------------------------------------------

                int productId = selectedSaleItemObject.SalesItem.Id;
                //Getting the Id of product related to selected sale item TAKE parameter to update method

                //------------------------------Count check---------------------------------------------------------------------------------------------------
                Console.WriteLine( "Enter the the count you wish to return for selected sale item" );

                int count=int.Parse(Console.ReadLine());

                if (count == 0)
                {

                    throw new Exception("Count can't be zero");
                }
                if (count <0) 
                {
                    throw new Exception("Count can't be less than zero");
                }
                if (count < selectedSaleItemObject.SalesItemCount)
                {
                    throw new Exception("Cant return more than were sold ");
                }

                //---------------------------------------------------------------------------------------------------------------------------------------------
                
                var productStorageList = ProductsService.GetAllProducts();
                var salesItemsStorageList = SalesServices.GetAllSaleItems();
                var salesStorageList= SalesServices.GetAllSales();

                //-------------------------------------Global storage update after nproduct return------------------------------------------------------------
                SalesServices.UpdateOfAllStaticStoragesAfterReturningSaleItems(ref productStorageList, ref salesItemsStorageList, ref salesStorageList,
                    ref selectedSale.SaleItemsList, productId, saleItemId, saleId, count);

                //----------------------------------------------Test tables-----------------------------------------------------------------------------------
                //ProductsService.GetAllProductsToTable();
                //SalesServices.GetAllSalesToTable();
                //SalesServices.GetAllSaleItemsToTable();

                //SalesServices.GetAnySaleItemsListToTable(selectedSale.SaleItemsList);

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }
           

        }
        public static void MenuDeleteSale()
        {

        }
        public static void MenuListAllSales() 
        { 
        
        }
        public static void MenuListAllSalesAccordingToDateRange()
        {

        }
        public static void MenuListAllSalesAccordingToSalesValueRange()
        {

        }
        public static void MenuShowSaleAccordingToSpecificDate()
        {


        }

        public static void NenuShowSaleAccordingToId()
        {

        }


    }
}
