using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Domain.DataContext;

namespace BanDienThoai.Models.Service
{
    public class CartService
    {
        public static bool CheckNumberProduct(int id,int soluong)
        {
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            SANPHAM sp = db.SANPHAMs.Where(s => s.MASP == id).SingleOrDefault();
            return sp.SOLUONG >= soluong;
        }
    }
}