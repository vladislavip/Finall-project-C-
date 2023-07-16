using Final_project.Common.Models;

namespace Final_project.Storage_classes
{
    internal class SalesItemStorage
    {
        public static List<SalesItems> SalesItems;

        public SalesItemStorage()
        {
            SalesItems = new List<SalesItems>();
        }
    }


}
