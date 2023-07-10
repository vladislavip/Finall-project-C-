using Final_project.Common.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Final_project.Common.Models
{
    internal class Sales:BaseEntity
    {
        private static int count = 0;
        public Sales()
        {
            Id = count;
            count++;
        }

        public decimal SaleValue {get; set;}    
        public DateTime SaleDate { get; set;}

        public List<SalesItems> SoldGoodsList;
    }
}
