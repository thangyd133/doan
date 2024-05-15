using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Domain.DataContext;

namespace BanDienThoai.Areas.Admin.Models
{
    public class LoginService
    {
        public int Login(string name, string pass)
        {
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            var taikhoan = db.TAIKHOANs.SingleOrDefault(x =>x.TENDN == name);
            if (taikhoan == null)
            {
                return 0;
            }
            else
            {
                if (taikhoan.TRANGTHAI == false)
                    return -2;
                else
                {
                    if (taikhoan.MATKHAU == pass)
                        if (taikhoan.MALOAITK == "LK00001")
                            return 1;
                        else
                            return -1;
                    else
                        return -2;
                }
            }
        }

        public TAIKHOAN GetUserByName(string Name)
        {
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            return db.TAIKHOANs.SingleOrDefault(x => x.TENDN == Name);
        }
    }
}