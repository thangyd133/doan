﻿using BanDienThoai.Areas.Admin.Models;
using BanDienThoai.Common;
using BanDienThoai.Domain.DataContext;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BanDienThoai.Areas.Admin.Controllers
{
   
    public class ProductController : Controller
    {

        ProductService productService = new ProductService();
        // GET: Admin/Product

        public ActionResult Index(int? page)
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }

            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductPagerViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);
        }

        public ActionResult Product(int? page)
        {
            var pager = new Pager(productService.getTotalRecord(), page);
            var viewModel = new ProductPagerViewModel
            {
                Products = productService.loadProduct(pager.CurrentPage, pager.PageSize),
                Pager = pager
            };

            return View(viewModel);

        }

        public ActionResult Create()
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            ProductViewModel productviewmodel = new ProductViewModel();
            ViewBag.ThuongHieu = productService.getDropDownThuongHieu() as List<SelectListItem>;
			ViewBag.LoaiSanPham = productService.getDropDownLoaiSanPham() as List<SelectListItem>;

            return View(productviewmodel);
        }


        public ActionResult Detail(int masp)
        {
            var userSession = (UserLogin)Session[CommonConstands.ADMIN_SESSION];
            if (userSession == null)
            {
                return Redirect("~/Admin/Login/Login");
            }
            return View(productService.getProductById(masp));
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel sanpham)
        {
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                SANPHAM sp = new SANPHAM();
                sp.TENSP = sanpham.TENSP;
                sp.SOLUONG = sanpham.SOLUONG;
                sp.MATH = int.Parse(sanpham.MATH);
                sp.MOTA = sanpham.MOTA;
                sp.DANHGIA = sanpham.DANHGIA;
                sp.DONGIA = sanpham.DONGIA;
                sp.MALOAISP = sanpham.MALOAISP;
                sp.HINHLON = sanpham.HINHLON;
                sp.TypeProduct = sanpham.LOAISANPHAM;
                sp.HINHNHO = sanpham.HINHLON.Split('.')[0];

				ViewBag.ThuongHieu = productService.getDropDownThuongHieu() as List<SelectListItem>;
				ViewBag.LoaiSanPham = productService.getDropDownLoaiSanPham() as List<SelectListItem>;
				if (productService.addProduct(sp))
                {
                    ViewBag.message = "Thêm mới sản phẩm thành công";
                    return View();
                } else
                {
					ViewBag.message = "Có lỗi xảy ra";
					return View();
				}
            }
            return View();
           
        }

        [HttpGet]
        public ActionResult Update(int masp)
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();

            ProductViewModel sp = new ProductViewModel();
            var sanpham = productService.getProductById(masp);
            sp.MASP = sanpham.MASP;
            sp.TENSP = sanpham.TENSP;
            sp.SOLUONG = (int) sanpham.SOLUONG;
            sp.MATH = sanpham.MATH.ToString();
            sp.MOTA = sanpham.MOTA;
            sp.DANHGIA = sanpham.DANHGIA;
            sp.DONGIA = (double)sanpham.DONGIA;
            sp.MALOAISP = sanpham.MALOAISP;
            sp.HINHLON = sanpham.HINHLON;
            sp.HINHNHO = sanpham.HINHNHO;

            return View(sp);
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel sanpham)
        {
            ViewBag.ThuongHieu = productService.getThuongHieu();
            ViewBag.LoaiSanPham = productService.getLoaiSanPham();
            ViewBag.message = "";
            if (ModelState.IsValid)
            {
                SANPHAM sp = new SANPHAM();
                sp.MASP = sanpham.MASP;
                sp.TENSP = sanpham.TENSP;
                sp.SOLUONG = (int)sanpham.SOLUONG;
                sp.MATH = int.Parse(sanpham.MATH);
                sp.MOTA = sanpham.MOTA;
                sp.DANHGIA = sanpham.DANHGIA;
                sp.DONGIA = (double)sanpham.DONGIA;
                sp.MALOAISP = sanpham.MALOAISP;
                sp.HINHLON = sanpham.HINHLON;
                sp.HINHNHO = sanpham.HINHNHO;

                if (productService.updateProduct(sp))
                {
                    ViewBag.message = "Sửa sản phẩm thành công";
                }
                return View(sanpham);

            }
            else
            {
                return View(sanpham);
            }         
        }

        public ActionResult Delete(int masp)
        {
            return Json(new { result = productService.deleteProduct(masp) });
        }

        [HttpPost]
        public JsonResult PhotoCreate(string tenhinh)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null)
                {

                    var path = Path.Combine(Server.MapPath("~/images/HINHLON/"), tenhinh);

                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult Photo(string hinhlon, string hinhnho)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                var FolderUploadDir = Server.MapPath("~/images/HINHNHO/" + hinhlon);
           
                if (file != null)
                {
                    if (!Directory.Exists(FolderUploadDir))
                    {
                        Directory.CreateDirectory(FolderUploadDir);
                    }
                    var path = Path.Combine(Server.MapPath("~/images/HINHNHO/" + hinhlon + "/"), hinhnho);
                    
                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            return Json(new { result = true });
        }

        [HttpPost]
        public JsonResult PhotoUpdate(string hinhlon, string hinhnho)
        {
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];

                if (file != null)
                {
                                
                    var path = Path.Combine(Server.MapPath("~/images/HINHNHO/" + hinhlon + "/"), hinhnho);
                    file.SaveAs(path);
                    return Json(new { result = true });
                }
                else
                {
                    return Json(new { result = false });
                }

            }
            return Json(new { result = true });
        }
    

    }
}