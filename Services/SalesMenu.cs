using Final_project.Abstract;
using Final_project.Common.Models;
using Final_project.Storage_classes;
using System.Globalization;

namespace Final_project.Services
{
    internal class SalesMenu : ISalesMenu
    {
        public static bool isTest = false;


        public static void MenuAddNewSale()
        {
            List<SalesItems> tempList = new();
            try
            {
            Start:

                ProductsService.GetAllProductsToTable();

                if (ProductsStorage.Products.Count == 0)
                {
                    throw new Exception("Product storage is empty");
                    Console.WriteLine("------------------------------------------------------------");
                }

                Console.WriteLine("Enter (ID) Product  that will become sale item: ");
                Console.WriteLine("------------------------------------------------------------");

                string idString = Console.ReadLine();
                bool isSuccesfullParseProductId = int.TryParse(idString, out int id);
                if (isSuccesfullParseProductId == false)
                {
                    Console.WriteLine("Input error, try again: ");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;
                }


                var existingProduct = ProductsStorage.Products.Find(x => x.Id == id);

                
                if (existingProduct == null)
                {
                    Console.WriteLine("Product not found,try again: ");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;
                }


                Console.WriteLine($"Enter quantity of sale items to be sold from product: {existingProduct.ProductName}");
                Console.WriteLine("------------------------------------------------------------");


                string stringCount = Console.ReadLine();
                bool isSuccesfullParsestringCount = int.TryParse(stringCount, out int count);                 //Count check
                if (isSuccesfullParsestringCount == false)
                {
                    Console.WriteLine("Wrong input");
                    Console.WriteLine("------------------------------------------------------------");
                    goto Start;

                }

               
                //-----------------------Filtering input------------------------------------------------
                if (count == 0 || count < 0)
                {

                    Console.WriteLine("Count cant be zero , you wil be returned to Id input: ");
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
                    Console.WriteLine("------------------------------------------------------------");
                }

                //----------------------------------------Sales item generation---------------------------------------------

                SalesItems salesItem = new();
                {
                    salesItem.SalesItem = existingProduct;
                    salesItem.SalesItemCount = count;

                }


                SalesItemStorage.SalesItems.Add(salesItem);
                tempList.Add(salesItem);

                Console.WriteLine("List of products that will be transfered to sale: ");
                //ProductsService.GetAllProductsToTable();
                //SalesServices.GetAllSaleItemsToTable();
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

                Console.WriteLine($"Sale with Id:{sale.Id} succesfuly added: ");
                Console.WriteLine("------------------------------------------------------------");
                SalesServices.GetAllSalesToTable();


                //-------------------------------------------Test area----------------------------------------------------

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

            catch (Exception ex)
            {
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");

            }


        }  //Don't touch , works 

        public static void MenuReturnSaleItems()
        {

            try
            {
            Start:

                if (SalesStorage.Sales.Count == 0)        //Check this code
                    throw new Exception("No sales yet or all sales are deleted!:");
                Console.WriteLine("------------------------------------------------------------");

                Console.WriteLine("------------------------------------------------------------");
                Console.WriteLine("List of all sales items: ");
                SalesServices.GetAllSaleItemsToTable();


                Console.WriteLine("Enter sales item's id you want to return: ");
                Console.WriteLine("------------------------------------------------------------");

                string idString = Console.ReadLine();                                   //ID filter
                bool isSuccesfullParseSalesId = int.TryParse(idString, out int id);
                if (isSuccesfullParseSalesId == false)
                    throw new Exception("Wrong ID input: ");

                var existingSaleitems = SalesItemStorage.SalesItems.FirstOrDefault(x => x.Id == id);
                if (existingSaleitems == null)
                {
                    Console.WriteLine("------------------------------------------------------------");
                    throw new Exception($"Id{id} doesn't exist: ");
                }

                Console.WriteLine("Input the quantity you want you want to return:  ");
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




                exisitngSale.SaleValue -= existingSaleitems.SalesItem.Price * count;     //Decreasing sales value


                //---------------Storage cleaners---------------------------------------------------------------------
                if (exisitngSale.SaleValue==0)
                {
                    exisitngSale.SaleItemsList.Remove(existingSaleitems);
                }
                  //Decreasing sales items count


                if (exisitngSale.SaleValue == 0)
                {
                    exisitngSale.SaleItemsList.Clear();

                }

                // Cleaning sale's sales items to make count field in table =0 , sale will be remaing but with 0 count and 0 value as the record

                if (existingSaleitems.SalesItemCount == 0)
                {
                    SalesItemStorage.SalesItems.Remove(existingSaleitems);

                }

                //Cleaning the Sale storage - ******thoroughly DeBug !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

                if (exisitngSale.SaleValue == 0)

                {
                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine($"ATTENTION sale with id:{exisitngSale.Id}, will be deleted , due to full return of its sales items: ");
                    SalesStorage.Sales.Remove(exisitngSale);


                }

                //----------------------------------------------------------------------------------------------

                Console.WriteLine("Products  returning....");
                Thread.Sleep(1000);
                Console.WriteLine("Done");
                Console.WriteLine("------------------------------------------------------------");


                Console.WriteLine("Updated Products storage: ");
                Console.WriteLine("------------------------------------------------------------");
                ProductsService.GetAllProductsToTable();


                Console.WriteLine("Updated Sale storage: ");
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
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");

            }

        } //Works 
        public static void MenuDeleteSale()      //Works
        {
            try
            {
                if (SalesStorage.Sales.Count == 0)
                    throw new Exception("No sales yet or all sales are deleted!:");
                Console.WriteLine("------------------------------------------------------------");

                SalesServices.GetAllSalesToTable();

                Console.WriteLine("Enter sale id");

                string saleIdString = Console.ReadLine();
                bool isSuccesfullParseSalesId = int.TryParse(saleIdString, out int id);
                if (isSuccesfullParseSalesId == false)
                    throw new Exception("Wrong sale id input: ");



                var existingSale = SalesStorage.Sales.FirstOrDefault(x => x.Id == id);

                if (existingSale == null)
                    throw new Exception($"Sale with Id: {id}  does not exists");


                if (existingSale.SaleValue == 0 && existingSale.SaleItemsList.Count == 0)
                    throw new Exception($"sale with Id:{id} was already returned");


                var salesItemsList = existingSale.SaleItemsList;

                Console.WriteLine("Following sale items will be returned from sale: ");
                Console.WriteLine("------------------------------------------------------------");
                SalesServices.GetAnySaleItemsListToTable(salesItemsList);


                foreach (var item in salesItemsList)    //Returning product
                {

                    var exisitngProduct = ProductsStorage.Products.Find(x => x.Id == item.SalesItem.Id);

                    exisitngProduct.ProductCount += item.SalesItemCount;

                    SalesItemStorage.SalesItems.Remove(item); // removing sales item from static storage

                }


              


                Console.WriteLine("Sale  deleting....");
                Thread.Sleep(1000);
                Console.WriteLine("Done");
                Console.WriteLine("------------------------------------------------------------");


                existingSale.SaleValue = 0;

                existingSale.SaleItemsList.Clear();    //cleaning list of sale items for existing sale that will be deleted
                if (existingSale.SaleValue == 0)

                {


                    Console.WriteLine("------------------------------------------------------------");
                    Console.WriteLine($"ATTENTION sale with id:{existingSale.Id}, will be deleted , due to full return of its sales items: ");
                    SalesStorage.Sales.Remove(existingSale);


                }

                Console.WriteLine("Products were succesfuly returned: ");
                Console.WriteLine("------------------------------------------------------------");
                ProductsService.GetAllProductsToTable();
                Console.WriteLine("Your list of sales: ");
                Console.WriteLine("------------------------------------------------------------");
                SalesServices.GetAllSalesToTable();

                ProductsService.GetAllProductsToTable();

                //-----------------------------------------Test area-------------------------------------------------------------------
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
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
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



                Console.WriteLine("Enter sale's range start date (dd/MM/yyyy): ");
                Console.WriteLine("------------------------------------------------------------");
                DateTime startDate = DateTime.ParseExact(Console.ReadLine().Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                Console.WriteLine("Enter the ending date");

                Console.WriteLine("Enter sale's range  end date (dd/MM/yyyy): ");
                Console.WriteLine("------------------------------------------------------------");
                DateTime endDate = DateTime.ParseExact(Console.ReadLine().Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                endDate = endDate.AddDays(1).AddSeconds(-1);

                if (startDate >= DateTime.Now || endDate >= DateTime.Now.AddDays(2).AddSeconds(-1))
                {
                    Console.WriteLine("------------------------------------------------------------");
                    throw new Exception("Wrong date input, Start date can't be higher or equal to End date!: ");

                }

                SalesServices.ListAllSalesAccordingToTimeRange(startDate, endDate);

                if (startDate > endDate)
                    throw new InvalidDataException("Start date can not be greater than end date!: ");
            }

            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

        }
        public static void MenuListAllSalesAccordingToSalesValueRange()
        {
            try
            {

                Console.WriteLine("Enter the lower value of sale value: ");
                Console.WriteLine("------------------------------------------------------------");
                string lowerValue = Console.ReadLine();
                bool isSuccesfullParsestringLower = decimal.TryParse(lowerValue, out decimal lower);
                if (isSuccesfullParsestringLower == false)
                    throw new Exception("Wrong sale value input: ");


                Console.WriteLine("Enter the upper value of sale value: ");
                Console.WriteLine("------------------------------------------------------------");
                string upperValue = Console.ReadLine();
                bool isSuccesfullParsestringUper = decimal.TryParse(upperValue, out decimal upper);
                if (isSuccesfullParsestringUper == false)
                    throw new Exception("Wrong sale value input: ");


                if (lower < 0 || upper < 0 || lower > upper || lower == upper)
                    throw new Exception("Wrong values input");


                SalesServices.ListAllSalesAccordingToValueRange(lower, upper);


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


        } //works
        public static void MenuShowSaleAccordingToSpecificDate()
        {

            try
            {
                Console.WriteLine("Enter sale's date (dd/MM/yyyy) : ");
                Console.WriteLine("------------------------------------------------------------");
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
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");
            }


        } //works
        public static void NenuShowSaleAccordingToId()
        {

            try
            {
                Console.WriteLine("List of all sales");
                Console.WriteLine("------------------------------------------------------------");
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
                Console.WriteLine("------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Error occured!");
                Console.WriteLine(ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("------------------------------------------------------------");

            }


        } //works


    }
}
