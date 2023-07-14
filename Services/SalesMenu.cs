using Final_project.Common.Models;
using Final_project.Storage_classes;
using System.Globalization;

namespace Final_project.Services
{
    internal class SalesMenu
    {

        public static void MenuAddNewSale()
        {
            List<SalesItems> tempList = new();
            try
            {
            Start:
                ProductsService.GetAllProductsToTable();
                Console.WriteLine("Enter (ID) Product  that will become sale item: ");
                Console.WriteLine("------------------------------------------------------------");

                string idString = Console.ReadLine();
                bool isSuccesfullParseProductId = int.TryParse(idString, out int id);
                if (isSuccesfullParseProductId == false)
                    throw new Exception("Wrong ID input");

                //-------------------------------------------------------------------------------------

                var existingProduct = ProductsStorage.Products.Find(x => x.Id == id);


                if (existingProduct == null)
                {
                    throw new Exception($"Product with Id {id} doesn't exist");
                }


                Console.WriteLine($"Enter quantity of sale items to be sold from product: {existingProduct.ProductName}");
                Console.WriteLine("------------------------------------------------------------");


                string stringCount = Console.ReadLine();
                bool isSuccesfullParsestringCount = int.TryParse(stringCount, out int count);                 //Count check
                if (isSuccesfullParsestringCount == false)
                    throw new Exception("Wrong count input");
                Console.WriteLine("------------------------------------------------------------");



                //-----------------------Filtering input------------------------------------------------
                if (count == 0)
                {

                    Console.WriteLine("Count cant be zero , you wil be returned to start");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;
                }

                if (existingProduct.ProductCount - count < 0)
                {
                    Console.WriteLine("Count cant be more than product count , you will be returned to start");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;

                }
                if (existingProduct.ProductCount - count >= 0)
                {
                    existingProduct.ProductCount = existingProduct.ProductCount - count;

                }

                if (existingProduct.ProductCount - count == 0)
                {

                    Console.WriteLine($"Product {existingProduct.ProductName} is out from stock");

                }



                SalesItems salesItem = new();
                {
                    salesItem.SalesItem = existingProduct;
                    salesItem.SalesItemCount = count;

                }




                SalesItemStorage.SalesItems.Add(salesItem);
                tempList.Add(salesItem);


                ProductsService.GetAllProductsToTable();
                SalesServices.GetAllSaleItemsToTable();
                SalesServices.GetAnySaleItemsListToTable(tempList);

                Console.WriteLine("Do you want to add one more product to sales list?");
                Console.WriteLine("1.Yes");
                Console.WriteLine("2.No");

                string choice = Console.ReadLine();

                bool isTrue = int.TryParse(choice, out int select);

                switch (select)
                {
                    case 1:
                        goto Start;

                    case 2:
                        goto End;
                    default:
                        goto End;
                }
            End:
                decimal cumulativeSaleValue = 0;
                Console.WriteLine("Sale generating....");
                Thread.Sleep(1000);
                Console.WriteLine("Done");
                Sales sale = new();
                {

                    sale.SaleDate = DateTime.Now;
                    sale.SaleItemsList = new();


                }

                foreach (var item in tempList)
                {
                    cumulativeSaleValue += item.SalesItem.Price * item.SalesItemCount;
                }

                sale.SaleValue = cumulativeSaleValue;


                sale.SaleItemsList = tempList;



                SalesStorage.Sales.Add(sale);

                ProductsService.GetAllProductsToTable();
                SalesServices.GetAllSaleItemsToTable();
                SalesServices.GetAllSalesToTable();
                SalesServices.GetAnySaleItemsListToTable(sale.SaleItemsList);
                SalesServices.GetAnySaleItemsListToTable(tempList);

            }

            catch (Exception Ex)
            {


                Console.WriteLine(Ex.Message);

            }


        }

        public static void MenuReturnSaleItems()
        {

            try
            {
                SalesServices.GetAllSaleItemsToTable();


                Console.WriteLine("Enter sales item's id you want to return");
                Console.WriteLine("------------------------------------------------------------");

                string idString = Console.ReadLine();                                   //ID filter
                bool isSuccesfullParseSalesId = int.TryParse(idString, out int id);
                if (isSuccesfullParseSalesId == false)
                    throw new Exception("Wrong ID input");

                var existingSaleitems = SalesItemStorage.SalesItems.FirstOrDefault(x => x.Id == id);
                if (existingSaleitems == null)
                {
                    throw new Exception("Id doesn't exist");
                }

                Console.WriteLine("Input the quantity you want you want to return ");
                Console.WriteLine("------------------------------------------------------------");

                string countString = Console.ReadLine();                                   //count filter
                bool isSuccesfullParseSalesCount = int.TryParse(countString, out int count);
                if (isSuccesfullParseSalesId == false)
                    throw new Exception("Wrong count input");


                //---------Product------------------------
                existingSaleitems.SalesItem.ProductCount += count;

                //--------Sales item ---------------------
                existingSaleitems.SalesItemCount -= count;

                //--------------------------------------

                var saleList = SalesStorage.Sales.Find((x => x.SaleItemsList.Contains(existingSaleitems)));

                if (saleList == null)
                    throw new Exception("Sale doesn't contain following sale item ");



                saleList.SaleValue -= existingSaleitems.SalesItem.Price * count;


                //var productId = existingSaleitems.SalesItem.Id;
                //var  productPrice = existingSaleitems.SalesItem.Price;

                //decimal totalValueOfSaleItem = productPrice * count;     



                //var selectedproduct = ProductsStorage.Products.FirstOrDefault(x => x.Id == productId);
                //selectedproduct.ProductCount = selectedproduct.ProductCount + count;


                //---------------------------------Removing empty items-------------------------------------------------

                // if (existingSaleitems.SalesItemCount == 0  ) // just in case >0
                // {
                //     SalesItemStorage.SalesItems.Remove(existingSaleitems);     // throw sale item from static list 
                // }

                //foreach (var item in saleList.SaleItemsList)
                // {
                //     if (item.SalesItemCount==0)
                //  
                // foreach (var item in SalesStorage.Sales)
                // { 
                //   if(item.SaleItemsList.Count==0) 
                //         SalesStorage.Sales.Remove(item);
                // }       saleList.SaleItemsList.Remove(item);                        // throw sale item from property list 


                // }


                ProductsService.GetAllProductsToTable();   //Shows correct
                SalesServices.GetAllSaleItemsToTable();
                SalesServices.GetAllSalesToTable();


                SalesServices.GetAnySaleItemsListToTable(saleList.SaleItemsList);

            }

            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }




            //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Finding the sale~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~---------------------------------------
            //try
            //{
            //    var sales = SalesServices.GetAllSales ();
            //    // Looking for sales from static storage

            //    if (sales == null) 
            //    {
            //        Console.WriteLine("Sales list is empty");
            //    }

            //    SalesServices.GetAllSalesToTable();
            //    //Table view of sales 

            //    Console.WriteLine("Enter Id of sale  from which you would like to return sale items");

            //    int saleId = int.Parse(Console.ReadLine());           // Take parameter to update method

            //    var selectedSale= sales.Find(x => x.Id == saleId);

            //    if (selectedSale == null)
            //        throw new Exception($"Sale whith Id: {saleId} doesn't exist ");

            //    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~Finding the sale item~~~~~~~~~~~~~~~~~~~~~~~---------------------------------------

            //    var saleItemsInFoundSale= selectedSale.SaleItemsList.ToList();
            //    //Getting list of sale items

            //    SalesServices.GetAnySaleItemsListToTable(saleItemsInFoundSale);

            //    Console.WriteLine("Select the Sale item ID you want return to product storage");

            //    int saleItemId= int.Parse(Console.ReadLine());          // Take parameter to update method

            //    var selectedSaleItemList= saleItemsInFoundSale.Where(x => x.Id == saleItemId).ToList();
            //    //Got the tageted sale item in form of list for passing to table 

            //    var selectedSaleItemObject= saleItemsInFoundSale.Find(x=>x.Id == saleItemId);
            //    //Got the targeted sale item in form of object for moving it further to return

            //    Console.WriteLine($"Your selected sale item is {saleItemId}, it will be now returned back to product stock: ");
            //    SalesServices.GetAnySaleItemsListToTable(selectedSaleItemList);


            //    //------------------------------Returning product to products storage------------------------------------------------------------------------

            //    int productId = selectedSaleItemObject.SalesItem.Id;
            //    //Getting the Id of product related to selected sale item TAKE parameter to update method

            //    //------------------------------Count check---------------------------------------------------------------------------------------------------
            //    Console.WriteLine( "Enter the the count you wish to return for selected sale item" );

            //    int count=int.Parse(Console.ReadLine());

            //    if (count == 0)
            //    {

            //        throw new Exception("Count can't be zero");
            //    }
            //    if (count <0) 
            //    {
            //        throw new Exception("Count can't be less than zero");
            //    }
            //    if (count > selectedSaleItemObject.SalesItemCount)
            //    {
            //        throw new Exception("Cant return more than were sold ");
            //    }

            //    //---------------------------------------------------------------------------------------------------------------------------------------------

            //    var productStorageList = ProductsService.GetAllProducts();
            //    var salesItemsStorageList = SalesServices.GetAllSaleItems();
            //    var salesStorageList= SalesServices.GetAllSales();

            //    //-------------------------------------Global storage update after nproduct return------------------------------------------------------------
            //    SalesServices.UpdateOfAllStaticStoragesAfterReturningSaleItems(ref productStorageList, ref salesItemsStorageList, ref salesStorageList,
            //        ref selectedSale.SaleItemsList, productId, saleItemId, saleId, count);

            //    Console.WriteLine("All Storages update after  sales item return");
            //    //----------------------------------------------Test tables-----------------------------------------------------------------------------------
            //    //ProductsService.GetAllProductsToTable();
            //    //SalesServices.GetAllSalesToTable();
            //    //SalesServices.GetAllSaleItemsToTable();

            //    //SalesServices.GetAnySaleItemsListToTable(selectedSale.SaleItemsList);

            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error occured");
            //    Console.WriteLine(ex.Message);
            //}


        } //....
        public static void MenuDeleteSale()      //.....
        {
            try
            {
                ////--------------------------------------------Storages------------------------------------------------------------------------------------
                var productStorageList = ProductsService.GetAllProducts();
                var salesItemsStorageList = SalesServices.GetAllSaleItems();
                var salesStorageList = SalesServices.GetAllSales();

                SalesServices.GetAllSalesToTable();

                Console.WriteLine("Enter sale id");

                int saleId = int.Parse(Console.ReadLine());

                var existingSale = salesStorageList.FirstOrDefault(x => x.Id == saleId);


                var saleItemsListPropList = existingSale.SaleItemsList;

                int cumulativeQty = 0;
                decimal totalValue = 0;
                foreach (var salesItem in saleItemsListPropList)
                {

                    cumulativeQty += salesItem.SalesItem.ProductCount;

                    var productId = salesItem.SalesItem.Id;


                    totalValue += salesItem.SalesItemCount * salesItem.SalesItem.Price;

                    var exitingproduct = productStorageList.FirstOrDefault(x => x.Id == productId);

                    exitingproduct.ProductCount += salesItem.SalesItemCount;


                }
                saleItemsListPropList.Clear();


                existingSale.SaleValue -= totalValue;

                salesStorageList = salesStorageList.Where(x => x.Id != saleId).ToList();
                //Updating static storage 

                existingSale.SaleItemsList.Clear();



                ProductsService.GetAllProductsToTable();
                SalesServices.GetAllSalesToTable();
                SalesServices.GetAllSaleItemsToTable();

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }
        public static void MenuListAllSales()
        {
            SalesServices.GetAllSalesToTable();
        }   // Works
        public static void MenuListAllSalesAccordingToDateRange()  // Works
        {
            try
            {

                Console.WriteLine("Enter the starting date");


                Console.WriteLine("Enter sale's date (MM/dd/yyyy HH:mm) : ");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter the ending date");

                Console.WriteLine("Enter sale's date (MM/dd/yyyy HH:mm) : ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy HH:mm", CultureInfo.InvariantCulture);

                SalesServices.ListAllSalesAccordingToTimeRange(startDate, endDate);

            }

            catch (Exception ex)
            {
                Console.WriteLine("");

            }

        }
        public static void MenuListAllSalesAccordingToSalesValueRange() //
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
