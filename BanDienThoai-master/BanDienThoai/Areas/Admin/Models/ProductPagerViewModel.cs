using BanDienThoai.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanDienThoai.Areas.Admin.Models
{
    public class ProductPagerViewModel
    {
        public IEnumerable<SANPHAM> Products { get; set; }
        public Pager Pager { get; set; }

    }

}