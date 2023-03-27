using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021105.DomainModels;
using _19T1021105.DataLayers;
using System.Configuration;

namespace _19T1021105.BusinessLayers
{
    /// <summary>
    /// Các chức năng tác nghiệp liên quan đến tài khoản
    /// </summary>
    public static class UserAccountService
    {
        private static IUserAccountDAL employeeAccountDB;
        private static IUserAccountDAL CustomerAccountDB;


        static UserAccountService()
        {        
            string connectionString = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;

            employeeAccountDB = new DataLayers.SQLServer.EmployeeAccountDAL(connectionString);
            CustomerAccountDB = new DataLayers.SQLServer.CustomerAccountDAL(connectionString);
        }

        public static UserAccount Authorize(AccountTypes accountType, string userName, string password)
        {
            if (accountType == AccountTypes.Employee)
                return employeeAccountDB.Authorize(userName, password);
            else
                return CustomerAccountDB.Authorize(userName, password);
        }

        public static bool ChangePassword(AccountTypes accountTpe, string userName, string oldPassword, string newPasaword)
        {
            if (accountTpe == AccountTypes.Employee)
                return employeeAccountDB.ChangePassword(userName, oldPassword, newPasaword);
            else
                return CustomerAccountDB.ChangePassword(userName, oldPassword, newPasaword);
        }
    }
}
