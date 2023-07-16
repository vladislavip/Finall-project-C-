using Final_project.Common.Models;
using Final_project.Storage_classes;
using System.Globalization;

namespace Final_project.Services
{
    internal class SalesMenu
    {
        public static bool isTest = false;


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

                    Console.WriteLine("Count cant be zero , you wil be returned to Id input ");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;
                }

                if (existingProduct.ProductCount - count < 0)
                {
                    Console.WriteLine("Count cant be more than product count , you will be returned to Id ");
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

                Console.WriteLine($"Sale with Id: {sale.Id}succesfuly thadded");
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


        }  //Don't touch , works 

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


                var exisitngSale = SalesStorage.Sales.Find((x => x.SaleItemsList.Contains(existingSaleitems)));

                if (exisitngSale == null)
                    throw new Exception("Sale doesn't contain following sale item ");
                Console.WriteLine("------------------------------------------------------------");



                exisitngSale.SaleValue -= existingSaleitems.SalesItem.Price * count;     //Decresing sales value


                //---------------Storage cleaners--------------------------------------------

                exisitngSale.SaleItemsList.Remove(existingSaleitems);  //Decresing sales items count


                if (exisitngSale.SaleValue == 0)
                {
                    exisitngSale.SaleItemsList.Clear();

                }


                // Cleaning sale's sales items to make count field in table =0 , sale will be remaing but with 0 count and 0 value as the record

                if (existingSaleitems.SalesItemCount == 0)
                {
                    SalesItemStorage.SalesItems.Remove(existingSaleitems);

                }




                //----------------------------------------------------------------------------------------------
                Console.WriteLine("Updated sale items list");
                Console.WriteLine("------------------------------------------------------------");
                SalesServices.GetAllSaleItemsToTable();

                Console.WriteLine("Products succsesfuly returned from sale");
                Console.WriteLine("------------------------------------------------------------");
                ProductsService.GetAllProductsToTable();



                if (isTest)
                {

                    string loading = "LOADING REPORTS";
                    for (int i = 0; i < loading.Length; i++)
                    {
                        Thread.Sleep(100);
                        Console.Write(loading[i]);
                    }

                    // Cleaning sale's sales items to make count =0 , sal will be remaing but with 0 count and 0 value

                    Console.WriteLine("---------------------------------------------------------------------------------");
                    Console.WriteLine("REPORTS");
                    Console.WriteLine("-----------------------Products static storage------------------------------------");
                    ProductsService.GetAllProductsToTable();
                    Console.WriteLine("-----------------------Sale Items static storage---------------------------------");
                    SalesServices.GetAllSaleItemsToTable();
                    Console.WriteLine("-----------------------Sales static storage----------------------------------------");
                    SalesServices.GetAllSalesToTable();
                    Console.WriteLine("-----------------------Sales dynamic salest list property-------------------------");
                    SalesServices.GetAnySaleItemsListToTable(exisitngSale.SaleItemsList);

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
            try
            {


                SalesServices.GetAllSalesToTable();

                Console.WriteLine("Enter sale id");

                string saleIdString = Console.ReadLine();
                bool isSuccesfullParseSalesId = int.TryParse(saleIdString, out int id);
                if (isSuccesfullParseSalesId == false)
                    throw new Exception("Wrong sale id input");


                var existingSale = SalesStorage.Sales.FirstOrDefault(x => x.Id == id);

                if (existingSale == null)
                    throw new Exception($"Sale with Id: {id}  does not exists");

                if (existingSale.SaleValue == 0 && existingSale.SaleItemsList.Count == 0)
                    throw new Exception($"sale with Id:{id} was already returned");

                var salesItemsList = existingSale.SaleItemsList;

                Console.WriteLine("Foloowing sale items will be returned from sale");
                SalesServices.GetAnySaleItemsListToTable(salesItemsList);


                foreach (var item in salesItemsList)    //Returning product
                {

                    var exisitngProduct = ProductsStorage.Products.Find(x => x.Id == item.SalesItem.Id);

                    exisitngProduct.ProductCount += item.SalesItemCount;

                    SalesItemStorage.SalesItems.Remove(item); // removing sales item from static storage

                }

                existingSale.SaleValue = 0;

                existingSale.SaleItemsList.Clear();    //cleaning list of sale items for existing sale that will be deleted

                Console.WriteLine("Products were succesfuly returned: ");
                Console.WriteLine("------------------------------------------------------------");
                ProductsService.GetAllProductsToTable();
                Console.WriteLine( "Your list of sales");
                Console.WriteLine("------------------------------------------------------------");
                SalesServices.GetAllSalesToTable();

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
                    SalesServices.GetAnySaleItemsListToTable(existingSale.SaleItemsList);

                }

            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }

        }
        public static void MenuListAllSales()
        {
            SalesServices.GetAllSalesToTable();   // Works


        }   // Works
        public static void MenuListAllSalesAccordingToDateRange()  // Works
        {
            try
            {

                Console.WriteLine("Enter the starting date");


                Console.WriteLine("Enter sale's date (dd/MM/yyyy) : ");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine().Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter the ending date");

                Console.WriteLine("Enter sale's date (dd/MM/yyyy) : ");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine().Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                endDate = endDate.AddDays(1).AddSeconds(-1);

                if (startDate >= DateTime.Now || endDate >= DateTime.Now.AddDays(2).AddSeconds(-1)) 
                {
                    throw new Exception("Wrong date input");
                }

                SalesServices.ListAllSalesAccordingToTimeRange(startDate, endDate);

                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!");
            }

            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine( ex.Message);


            }

        }
        public static void MenuListAllSalesAccordingToSalesValueRange()
        {
            try
            {

                Console.WriteLine("Enter the lower value");
                string lowerValue = Console.ReadLine();
                bool isSuccesfullParsestringLower = decimal.TryParse(lowerValue, out decimal lower);
                if (isSuccesfullParsestringLower == false)
                    throw new Exception("Wrong  input");
                Console.WriteLine("------------------------------------------------------------");

                Console.WriteLine("Enter the upper value");
                string upperValue = Console.ReadLine();
                bool isSuccesfullParsestringUper = decimal.TryParse(upperValue, out decimal upper);
                if (isSuccesfullParsestringUper == false)
                    throw new Exception("Wrong  input");
                Console.WriteLine("------------------------------------------------------------");

                if (lower < 0 || upper < 0 || lower > upper || lower == upper)
                    throw new Exception("Wrong values input");


                SalesServices.ListAllSalesAccordingToValueRange(lower, upper);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);
            }


        } //works
        public static void MenuShowSaleAccordingToSpecificDate()
        {

            try
            {
                Console.WriteLine("Enter sale's date (dd/MM/yyyy) : ");
                DateTime date = DateTime.ParseExact(Console.ReadLine().Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);



                if (date > DateTime.Now)
                {
                    throw new Exception("Can't predict future :)");
                }

                DateTime DateStart = date.AddSeconds(1);
                DateTime DateEnd = date.AddDays(1).AddSeconds(-1);
               

                SalesServices.ListAllSalesAccordingToTimeRange(DateStart, DateEnd);

            }
            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);

            }


        } //works

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
            catch (Exception ex)

            {
                Console.WriteLine("Error occured");
                Console.WriteLine(ex.Message);


            }





        } //works


    }
}
