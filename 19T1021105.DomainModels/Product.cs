using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021105.DomainModels
{
    /// <summary>
    /// Mặt hàng
    /// </summary>
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int SuplierID { get; set; }
        public int CategoryID { get; set; }
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public string Photo { get; set; }
    }
}
