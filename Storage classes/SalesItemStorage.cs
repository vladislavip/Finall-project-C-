using Final_project.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
