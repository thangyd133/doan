using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Domain.DataContext;
namespace BanDienThoai.Models.ViewModel
{
    public class HomePageViewModel
    {
        public IEnumerable<SANPHAM> ProductsSelling { get; set; }
        public IEnumerable<ProductViewModel> NewProducts { get; set; }
    }
}