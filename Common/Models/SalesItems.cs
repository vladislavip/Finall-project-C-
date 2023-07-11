using Final_project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Common.Models
{
    internal class SalesItems:BaseEntity
    {
        private static int count = 0;
        public SalesItems()
        {
            Id = count;
            count++;
        }
        public Product SalesItem { get; set; }

        public int SalesItemCount { get; set; }


    }
}
