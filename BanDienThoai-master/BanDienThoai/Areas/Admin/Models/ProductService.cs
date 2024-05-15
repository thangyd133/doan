using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDienThoai.Domain.DataContext;
namespace BanDienThoai.Areas.Admin.Models
{
    public class ProductService
    {
        BANDIENTHOAIEntities db;
        public ProductService()
        {
            db = new BANDIENTHOAIEntities();
        }
       
        public IEnumerable<SANPHAM> getAllProduct()
        {          
            return db.SANPHAMs;
        }

        public int getTotalRecord()
        {         
            return (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Count();
        }

        public SANPHAM getProductById(int masp)
        {          
            return db.SANPHAMs.Find(masp);
        }

        public bool addProduct(SANPHAM sp)
        {          
            try
            {
                db.SANPHAMs.Add(sp);
                db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool updateProduct(SANPHAM sp)
        {          
            try
            {
                var result = db.SANPHAMs.Find(sp.MASP);
                if(result != null)
                {
                    result.TENSP = sp.TENSP;
                    result.SOLUONG = sp.SOLUONG;
                    result.MATH = sp.MATH;
                    result.MOTA = sp.MOTA;
                    result.DONGIA = sp.DONGIA;
                    result.MALOAISP = sp.MALOAISP;
                    result.HINHLON = sp.HINHLON;
                    result.HINHNHO = sp.HINHNHO;
                    result.DANHGIA = sp.DANHGIA;
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool deleteProduct(int masp)
        {        
            try {
                string query = "DELETE FROM CHITIETKM WHERE MASP = '" + masp + "'";
                string query2 = "DELETE FROM CHITIETDONHANG WHERE MASP = '" + masp + "'";
                string query3 = "DELETE FROM SANPHAM WHERE MASP = '" + masp + "'";
               
                db.Database.ExecuteSqlCommand(query);
                db.Database.ExecuteSqlCommand(query2);
                db.Database.ExecuteSqlCommand(query3);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
            

        }

     
        public IEnumerable<SANPHAM> loadProduct(int pageIndex, int pageSize)
        {
            IEnumerable<SANPHAM> ListProduct = null;
                  
            ListProduct = (from sp in db.SANPHAMs orderby sp.MASP descending select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return ListProduct;
        }

        public IEnumerable<LOAISANPHAM> getLoaiSanPham()
        {           
            return db.LOAISANPHAMs;
        }

		public IEnumerable<SelectListItem> getDropDownLoaiSanPham()
		{
            var list = db.LOAISANPHAMs.Select(x => new SelectListItem
            {
                Value = x.MALOAISP,
                Text = x.TENLOAISP
            }).ToList();
			return list;
		}



		public IEnumerable<SelectListItem> getDropDownThuongHieu()
		{
			var list = db.THUONGHIEUx.Select(x => new SelectListItem
			{
				Value = x.MATH.ToString(),
				Text = x.TENTH
			}).ToList();
			return list;
		}


		public IEnumerable<THUONGHIEU> getThuongHieu()
        {            
            return db.THUONGHIEUx;
        }
    }
}