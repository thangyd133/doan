using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDienThoai.Models.ViewModel;
using BanDienThoai.Models.Service;

namespace BanDienThoai.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
       
        public ActionResult Index(string cateKey, int page = 1)
        {
            int pageSize = 8;
            ViewData["pageSize"] = pageSize;
            ProductCategoryViewModel prctViewModel = new ProductCategoryViewModel();

            //lấy ra danh sách sản phẩm theo loại
            if(cateKey=="tat-ca")
            {
                prctViewModel.ListProductCategory = ProductCategoryService.LoadProductAll();
            }
            else
            {
                if(cateKey == "dien-thoai")
                {
                    prctViewModel.ListProductCategory = ProductCategoryService.LoadProductPhone();
                }
                else
                {
                    prctViewModel.ListProductCategory = ProductCategoryService.LoadProductPhuKien();
                }
            }
            int totalRecord = prctViewModel.ListProductCategory.Count;
            prctViewModel.CateKey = cateKey;
            prctViewModel.Index = page;
            prctViewModel.TotalPage= (int)(Math.Ceiling(((double)totalRecord / pageSize)));


            if(cateKey == "dien-thoai")
            {
                return PartialView("_DienThoai",prctViewModel);
            } else if(cateKey == "phu-kien")
            {
				return PartialView("_PhuKien", prctViewModel);
			} else
            {
                return View(prctViewModel);
            }
        }

        public JsonResult ListName(String q)
        {
            var data = ProductService.ListName(q);
            return Json(new
            {
                data = data,
                status = true
            }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keyword, int page = 1)
        {
            int pageSize = 8;
            ViewData["pageSize"] = pageSize;
            ViewBag.Keyword = keyword;
            ProductCategoryViewModel prctViewModel = new ProductCategoryViewModel();

            //lấy ra danh sách sản phẩm theo tên
            prctViewModel.ListProductCategory = ProductService.Search(keyword);

            int totalRecord = prctViewModel.ListProductCategory.Count;
            ViewBag.TotalRecord = totalRecord;
            prctViewModel.Index = page;
            prctViewModel.TotalPage = (int)(Math.Ceiling(((double)totalRecord / pageSize)));

            return View(prctViewModel);
        }
    }
}