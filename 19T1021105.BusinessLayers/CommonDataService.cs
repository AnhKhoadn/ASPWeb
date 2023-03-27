using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021105.DataLayers;
using _19T1021105.DomainModels;
using System.Configuration;


namespace _19T1021105.BusinessLayers
{
    /// <summary>
    /// Cung cấp các chức năng nghiệp vụ xử lý dữ liệu chung liên quan đến:
    /// Quốc gia, Nhà cung cấp, Khách hàng, Người giao hàng, Nhân viên, Loại hàng
    /// </summary>
    public static class CommonDataService
    {
        private static ICountryDAL countryDB;
        private static ICommonDAL<Suplier> SuplierDB;
        private static ICommonDAL<Customer> CustomerDB;
        private static ICommonDAL<Shipper> ShipperDB;
        private static ICommonDAL<Employee> EmployeeDB;
        private static ICommonDAL<Category> CategoryDB;

        /// <summary>
        /// ctor
        /// </summary>
        static CommonDataService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            countryDB = new DataLayers.SQLServer.CountryDAL(connectionString);
            SuplierDB = new DataLayers.SQLServer.SuplierDAL(connectionString);
            CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            EmployeeDB = new DataLayers.SQLServer.EmployeeDAL(connectionString);
            CategoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
        }

        #region xử lý liên quan đến quốc gia

        public static List<Country> ListOfCountries()
        {
            return countryDB.List().ToList();
        }

        #endregion

        #region Xử lý liên quan đến nhà cung cấp
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page">Trang cần hiển thị</param>
        /// <param name="pagesize">Số dòng trên mỗi trang (0 nếu không phân trang)</param>
        /// <param name="searchValue">Giá trị tìm kiếm (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <param name="rowCount">THam số đầu ra: số dòng dữ liệu tìm được</param>
        /// <returns></returns>
        public static List<Suplier> ListOfSupliers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = SuplierDB.Count(searchValue);
            return SuplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue">Giá trị tìm kiếm (chuỗi rỗng nếu không tìm kiếm)</param>
        /// <returns></returns>
        public static List<Suplier> ListOfSupliers(string searchValue = "")
        {
            return SuplierDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="suplierID">Mã nhà cung cấp cần lấy thông tin</param>
        /// <returns></returns>
        public static Suplier GetSuplier(int suplierID)
        {
            return SuplierDB.Get(suplierID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data">Mã nhà cung cấp cần thêm</param>
        /// <returns></returns>
        public static int AddSuplier(Suplier data)
        {
            return SuplierDB.Add(data);
        }

        public static bool UpdateSuplier(Suplier data)
        {
            return SuplierDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SuplierID">Mã nhà cung cấp cần xóa</param>
        /// <returns></returns>
        public static bool DeleteSuplier(int suplierID)
        {
            return SuplierDB.Delete(suplierID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SuplierID">Mã nhà cung cấp đã sử dụng</param>
        /// <returns></returns>
        public static bool InUsedSuplier(int suplierID)
        {
            return SuplierDB.InUsed(suplierID);
        }
        #endregion

        #region Xử lý liên quan đến loại hàng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Category> ListOfCategories(string searchValue = "")
        {
            return CategoryDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static Category GetCategory(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }
        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return CategoryDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SuplierID"></param>
        /// <returns></returns>
        public static bool DeleteCategory(int categoryID)
        {
            return CategoryDB.Delete(categoryID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="SuplierID"></param>
        /// <returns></returns>
        public static bool InUsedCategory(int categoryID)
        {
            return CategoryDB.InUsed(categoryID);
        }
        #endregion

        #region Xử lý liên quan đến khách hàng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Customer> ListOfCustomers(string searchValue = "")
        {
            return CustomerDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Customer GetCustomer(int customerID)
        {
            return CustomerDB.Get(customerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteCustomer(int customerID)
        {
            return CustomerDB.Delete(customerID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedCustomer(int customerID)
        {
            return CustomerDB.InUsed(customerID);
        }
        #endregion

        #region Xử lý liên quan đên nhân viên
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(string searchValue = "")
        {
            return EmployeeDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int employeeID)
        {
            return EmployeeDB.Delete(employeeID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedEmployee(int employeeID)
        {
            return EmployeeDB.InUsed(employeeID);
        }
        #endregion

        #region Xử lý liên quan đến nhân viên giao hàng
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(string searchValue = "")
        {
            return ShipperDB.List(1, 0, searchValue).ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int shipperID)
        {
            return ShipperDB.Delete(shipperID);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="categoryID"></param>
        /// <returns></returns>
        public static bool InUsedShipper(int shipperID)
        {
            return ShipperDB.InUsed(shipperID);
        }
        #endregion
    }
}
