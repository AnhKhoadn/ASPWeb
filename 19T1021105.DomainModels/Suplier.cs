using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19T1021105.DomainModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Suplier
    {
        /// <summary>
        /// 
        /// </summary>
        public int SuplierID { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string SuplierName { get; set; }
        /// <summary>
        /// Tên giao dịch
        /// </summary>
        public string ContactName { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Mã bưu chính
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// Quốc gia
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Điện thoại
        /// </summary>
        public string Phone { get; set; }
    }
}
