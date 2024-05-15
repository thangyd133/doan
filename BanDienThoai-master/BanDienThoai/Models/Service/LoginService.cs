using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Domain.DataContext;
using BanDienThoai.Models.ViewModel;
using BanDienThoai.Common;

namespace BanDienThoai.Models.Service
{
    public class LoginService
    {
        BANDIENTHOAIEntities db = null;
        public LoginService()
        {
            db = new BANDIENTHOAIEntities();
        }

        /// <summary>
        /// Service lấy username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public TAIKHOAN GetById(string userName)
        {
            return db.TAIKHOANs.SingleOrDefault(n => n.TENDN == userName);
        }

        /// <summary>
        /// Service kiểm tra username và password
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public int Login(string userName, string passWord)
        {
            var result = db.TAIKHOANs.SingleOrDefault(n => n.TENDN == userName);
            if(result == null)
            {
                return 0;
            }
            else
            {
                if (result.TRANGTHAI == false)
                {
                    return -1;
                }
                else
                {
                    if (result.MATKHAU == passWord)
                        return 1;
                    else
                        return -2;
                }
            }
        }
    }
}