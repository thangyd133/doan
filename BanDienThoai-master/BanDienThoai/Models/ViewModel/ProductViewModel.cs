using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Domain.DataContext;


namespace BanDienThoai.Models.ViewModel
{
    public class ProductViewModel
    {
        public SANPHAM Product { get; set; }
        public int Promotion { get; set; }
    }
}