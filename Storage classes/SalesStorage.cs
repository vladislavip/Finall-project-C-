using Final_project.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Storage_classes
{
    internal class SalesStorage
    {
       
        public static List<Sales> Sales;

        public SalesStorage()
        {
            Sales = new List<Sales>();
        }
    }
}
