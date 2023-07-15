using Final_project.Common.Models;
using Final_project.Storage_classes;
using System.Globalization;

namespace Final_project.Services
{
    internal class SalesMenu
    {
        static bool isTest = true;


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
                {
                    Console.WriteLine("Do again");
                    goto Start;
                }

               

                var existingProduct = ProductsStorage.Products.Find(x => x.Id == id);


                if (existingProduct == null)
                {
                    Console.WriteLine("Do again");
                    goto Start;
                }


                Console.WriteLine($"Enter quantity of sale items to be sold from product: {existingProduct.ProductName}");
                Console.WriteLine("------------------------------------------------------------");


                string stringCount = Console.ReadLine();
                bool isSuccesfullParsestringCount = int.TryParse(stringCount, out int count);                 //Count check
                if (isSuccesfullParsestringCount == false)
                {
                    Console.WriteLine("Wrong input");
                    goto Start;

                }
                    
                Console.WriteLine("------------------------------------------------------------");



                //-----------------------Filtering input------------------------------------------------
                if (count == 0 || count < 0)
                {

                    Console.WriteLine("Count cant be zero , you wil be returned to INPUT START==> ");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;
                }

                if (existingProduct.ProductCount - count < 0)
                {
                    Console.WriteLine("Count cant be more than product count , you will be returned to INPUT START==> ");
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

                //----------------------------------------Sales item generation---------------------------------------------

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
                Console.WriteLine("------------------------------------------------------------");

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

                Console.WriteLine("Sale succesfuly added");
                SalesServices.GetAllSalesToTable();


                if (isTest)
                {

                    string loading = "LOADING REPORTS";
                    for (int i = 0; i < loading.Length; i++)
                    {
                        Thread.Sleep(100);
                        Console.Write(loading[i]);
                        Console.Write("_");
                    }

                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine("REPORTS");
                    Console.WriteLine("-----------------------Products static storage------------------------------------");
                    ProductsService.GetAllProductsToTable();
                    Console.WriteLine("-----------------------Sale Items static storage---------------------------------");
                    SalesServices.GetAllSaleItemsToTable();
                    Console.WriteLine("-----------------------Sales static storage----------------------------------------");
                    SalesServices.GetAllSalesToTable();
                    Console.WriteLine("-----------------------Sales dynamic sales list property-------------------------");
                    SalesServices.GetAnySaleItemsListToTable(sale.SaleItemsList);
                    Console.WriteLine("-----------------------Sales dynamic temp sales list property-------------------------");
                    SalesServices.GetAnySaleItemsListToTable(tempList);

                }

            }

            catch (Exception Ex)
            {


                Console.WriteLine(Ex.Message);

            }


        }  //Don;t touch , works 


        public static void MenuReturnSaleItems()
        {

            try
            {
            Start:
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


                //------------------Count filter--------------------------------------------------
                if (count == 0 || count < 0)
                {

                    Console.WriteLine("Count cant be zero , you wil be returned to start");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;
                }

                if (existingSaleitems.SalesItemCount - count < 0)
                {
                    Console.WriteLine("Count cant be more than product count , you will be returned to start");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;

                }
                if (existingSaleitems.SalesItemCount - count >= 0 || existingSaleitems.SalesItemCount - count == 0)
                {
                    //---------Product------------------------
                    existingSaleitems.SalesItem.ProductCount += count;

                    //--------Sales item ---------------------
                    existingSaleitems.SalesItemCount -= count;

                }


                var saleList = SalesStorage.Sales.Find((x => x.SaleItemsList.Contains(existingSaleitems)));

                if (saleList == null)
                    throw new Exception("Sale doesn't contain following sale item ");



                saleList.SaleValue -= existingSaleitems.SalesItem.Price * count;

                Console.WriteLine("Products succsesfuly returned from sale");
                ProductsService.GetAllProductsToTable();

                if (isTest)
                {

                    string loading = "LOADING REPORTS";
                    for (int i = 0; i < loading.Length; i++)
                    {
                        Thread.Sleep(100);
                        Console.Write(loading[i]);
                    }

                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine("REPORTS");
                    Console.WriteLine("-----------------------Products static storage------------------------------------");
                    ProductsService.GetAllProductsToTable();
                    Console.WriteLine("-----------------------Sale Items static storage---------------------------------");
                    SalesServices.GetAllSaleItemsToTable();
                    Console.WriteLine("-----------------------Sales static storage----------------------------------------");
                    SalesServices.GetAllSalesToTable();
                    Console.WriteLine("-----------------------Sales dynamic salest list property-------------------------");
                    SalesServices.GetAnySaleItemsListToTable(saleList.SaleItemsList);

                }


            }

            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }

        } //Works , ask if empty storages should be cleaned
        public static void MenuDeleteSale()      //.....
        {
            //try
            //{
            //    ////--------------------------------------------Storages------------------------------------------------------------------------------------
            //    var productStorageList = ProductsService.GetAllProducts();
            //    var salesItemsStorageList = SalesServices.GetAllSaleItems();
            //    var salesStorageList = SalesServices.GetAllSales();

            //    SalesServices.GetAllSalesToTable();

            //    Console.WriteLine("Enter sale id");

            //    int saleId = int.Parse(Console.ReadLine());
            //    if (isSuccesfullParseProductId == false)
            //        throw new Exception("Wrong ID input");

            //    var existingSale = salesStorageList.FirstOrDefault(x => x.Id == saleId);


            //    var saleItemsListPropList = existingSale.SaleItemsList;

            //    int cumulativeQty = 0;
            //    decimal totalValue = 0;
            //    foreach (var salesItem in saleItemsListPropList)
            //    {

            //        cumulativeQty += salesItem.SalesItem.ProductCount;

            //        var productId = salesItem.SalesItem.Id;


            //        totalValue += salesItem.SalesItemCount * salesItem.SalesItem.Price;

            //        var exitingproduct = productStorageList.FirstOrDefault(x => x.Id == productId);

            //        exitingproduct.ProductCount += salesItem.SalesItemCount;


            //    }
            //    saleItemsListPropList.Clear();


            //    existingSale.SaleValue -= totalValue;

            //    salesStorageList = salesStorageList.Where(x => x.Id != saleId).ToList();
            //    //Updating static storage 

            //    existingSale.SaleItemsList.Clear();



            //    if (isTest)
            //    {

            //        string loading = "LOADING REPORTS";
            //        for (int i = 0; i < loading.Length; i++)
            //        {
            //            Thread.Sleep(100);
            //            Console.Write(loading[i]);
            //        }

            //        Console.WriteLine("---------------------------------------------------------------------------------");
            //        Console.WriteLine("REPORTS");
            //        Console.WriteLine("-----------------------Products static storage------------------------------------");
            //        ProductsService.GetAllProductsToTable();
            //        Console.WriteLine("-----------------------Sale Items static storage---------------------------------");
            //        SalesServices.GetAllSaleItemsToTable();
            //        Console.WriteLine("-----------------------Sales static storage----------------------------------------");
            //        SalesServices.GetAllSalesToTable();
            //        Console.WriteLine("-----------------------Sales dynamic salest list property-------------------------");
            //        SalesServices.GetAnySaleItemsListToTable(existingSale.SaleItemsList);

            //    }

            //}

            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error occured");
            //    Console.WriteLine(ex.Message);
            //}

        }
            public static void MenuListAllSales()  
            {
                SalesServices.GetAllSalesToTable();   // Works

           
            }   
        public static void MenuListAllSalesAccordingToDateRange()  // Works
        {
            try
            {

                Console.WriteLine("Enter the starting date");


                Console.WriteLine("Enter sale's date (MM/dd/yyyy) : ");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter the ending date");

                Console.WriteLine("Enter sale's date (MM/dd/yyyy) : ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                endDate = endDate.AddDays(1).AddSeconds(-1);

                if (startDate >= DateTime.Now  || endDate > DateTime.Now) 
                {
                    throw new Exception("Wrong date input");
                }

                SalesServices.ListAllSalesAccordingToTimeRange(startDate, endDate);

                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!");
            }

            catch (Exception ex)
            {
                Console.WriteLine("");

            }

        }
        public static void MenuListAllSalesAccordingToSalesValueRange() //Works
        {
            try
            {
                string lowerValue = Console.ReadLine();
                bool isSuccesfullParsestringLower = decimal.TryParse(lowerValue, out decimal lower);                
                if (isSuccesfullParsestringLower == false)
                    throw new Exception("Wrong count input");
                Console.WriteLine("------------------------------------------------------------");


                string upperValue = Console.ReadLine();
                bool isSuccesfullParsestringUper= decimal.TryParse(upperValue, out decimal upper);                 
                if (isSuccesfullParsestringUper == false)
                    throw new Exception("Wrong count input");
                Console.WriteLine("------------------------------------------------------------");

                SalesServices.ListAllSalesAccordingToValueRange(lower, upper);


            }
            catch(Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }


        }
        public static void MenuShowSaleAccordingToSpecificDate()
        {

            try
            {
                Console.WriteLine("Enter sale's date (MM/dd/yyyy HH:mm) : ");
                DateTime date = DateTime.ParseExact(Console.ReadLine(), "MM/dd/yyyy", CultureInfo.InvariantCulture);

                if (date > DateTime.Now)
                {
                    throw new Exception("Wrong date input");
                }

                DateTime DateEnd = date.AddDays(1).AddSeconds(-1);
                DateTime DateStart =date.AddDays(-1).AddSeconds(1);

                SalesServices.ListAllSalesAccordingToTimeRange(DateStart, DateEnd);

            }
            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }

           
        }

        public static void NenuShowSaleAccordingToId()
        {

            try
            {
                Console.WriteLine("List of all sales");
                SalesServices.GetAllSalesToTable();
                Console.WriteLine("------------------------------------------------------------");

                Console.WriteLine("Enter (ID) Sale  to search ");
                Console.WriteLine("------------------------------------------------------------");

                string idString = Console.ReadLine();
                bool isSuccesfullParseProductId = int.TryParse(idString, out int id);
                if (isSuccesfullParseProductId == false)
                    throw new Exception("Wrong ID input");

                SalesServices.ShowSaleAccordingToSaleId(id);
            }
            catch(Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);


            }
            




        }


    }
}
