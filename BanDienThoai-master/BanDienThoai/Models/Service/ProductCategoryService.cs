using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BanDienThoai.Constant;
using BanDienThoai.Domain.DataContext;
using BanDienThoai.Models.ViewModel;

namespace BanDienThoai.Models.Service
{
    public class ProductCategoryService
    {
        public static IEnumerable<SANPHAM> LoadProductCategory(string category, ref int totalRecord, int pageIndex = 1, int pageSize = 8)
        {
            IEnumerable<SANPHAM> ListProductCategory = null;
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            if (category == "tat-ca")
            {
                totalRecord = (from sp in db.SANPHAMs
                               orderby sp.MASP descending
                               select sp).Count();
                try
                {
                    ListProductCategory = (from sp in db.SANPHAMs
                                           orderby sp.MASP descending
                                           select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                catch (Exception e)
                {

                }
            }
            if (category == "dien-thoai")
            {
                totalRecord = (from sp in db.SANPHAMs
                               where sp.MALOAISP == "LP00001"
                               orderby sp.MASP descending
                               select sp).Count();
                try
                {
                    ListProductCategory = (from sp in db.SANPHAMs
                                           where sp.MALOAISP == "LP00001"
                                           orderby sp.MASP descending
                                           select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                catch (Exception e)
                {

                }
            }
            if (category == "phu-kien")
            {
                totalRecord = (from sp in db.SANPHAMs
                               where sp.MALOAISP == "LP00002"
                               orderby sp.MASP descending
                               select sp).Count();
                try
                {
                    ListProductCategory = (from sp in db.SANPHAMs
                                           where sp.MALOAISP == "LP00002"
                                           orderby sp.MASP descending
                                           select sp).Skip((pageIndex - 1) * pageSize).Take(pageSize);
                }
                catch (Exception e)
                {

                }
            }
            return ListProductCategory;
        }
        public static List<ProductViewModel> LoadProductAll()
        {
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            List<ProductViewModel> result = new List<ProductViewModel>();
            IEnumerable<SANPHAM> LoadProductAll = new List<SANPHAM>();
            LoadProductAll = (from sp in db.SANPHAMs
                              orderby sp.MASP descending
                              select sp);
            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in LoadProductAll)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }
    
        public static List<ProductViewModel> LoadProductMen()
        {
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            List<ProductViewModel> result = new List<ProductViewModel>();
            IEnumerable<SANPHAM> LoadProductMen = new List<SANPHAM>();

            LoadProductMen = (from sp in db.SANPHAMs
                              where sp.MALOAISP == "LP00001"
                              orderby sp.MASP descending
                              select sp);

            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in LoadProductMen)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }

        public static List<ProductViewModel> LoadProductWomen()
        {
            BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
            List<ProductViewModel> result = new List<ProductViewModel>();
            IEnumerable<SANPHAM> LoadProductWomen = new List<SANPHAM>();

            LoadProductWomen = (from sp in db.SANPHAMs
                              where sp.MALOAISP == "LP00002"
                              orderby sp.MASP descending
                              select sp);

            // Lấy promotion của sản phẩm
            foreach (SANPHAM sp in LoadProductWomen)
            {
                int Promotion = PromotionService.GetPromotion(sp.MASP);
                ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
                result.Add(productViewModel);
            }
            return result;
        }


		
        public static List<ProductViewModel> LoadProductPhone()
		{
			BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
			List<ProductViewModel> result = new List<ProductViewModel>();
			IEnumerable<SANPHAM> LoadProducDienThoai = new List<SANPHAM>();

			LoadProducDienThoai = (from sp in db.SANPHAMs
                                where sp.TypeProduct == TypeProduct.DIENTHOAI
                                orderby sp.MASP descending
                                select sp) ;

			// Lấy promotion của sản phẩm
			foreach (SANPHAM sp in LoadProducDienThoai)
			{
				int Promotion = PromotionService.GetPromotion(sp.MASP);
				ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
				result.Add(productViewModel);
			}
			return result;
		}
        public static List<ProductViewModel> LoadProductPhuKien()
		{
			BANDIENTHOAIEntities db = new BANDIENTHOAIEntities();
			List<ProductViewModel> result = new List<ProductViewModel>();
			IEnumerable<SANPHAM> LoadProductPhuKien = new List<SANPHAM>();

			LoadProductPhuKien = (from sp in db.SANPHAMs
                                where sp.TypeProduct == TypeProduct.PHUKIEN
                                orderby sp.MASP descending
                                select sp) ;

			// Lấy promotion của sản phẩm
			foreach (SANPHAM sp in LoadProductPhuKien)
			{
				int Promotion = PromotionService.GetPromotion(sp.MASP);
				ProductViewModel productViewModel = new ProductViewModel { Product = sp, Promotion = Promotion };
				result.Add(productViewModel);
			}
			return result;
		}




	}
}