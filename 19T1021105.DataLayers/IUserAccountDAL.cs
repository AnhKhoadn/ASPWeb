﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _19T1021105.DomainModels;

namespace _19T1021105.DataLayers
{
    /// <summary>
    /// Định nghĩa phép xử lí dữ liệu liên quan đến tài khoản
    /// </summary>
    public interface IUserAccountDAL
    {
        /// <summary>
        /// Kiểm tra xem tên đăng nhập và mật khẩu của người dùng có hợp lệ?
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserAccount Authorize(string userName, string password);
        /// <summary>
        /// Đổi mật khẩu của người dùng
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="oldPassword"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);
    }
}
