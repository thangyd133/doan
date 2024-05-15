using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanDienThoai.Models.ViewModel;
using BanDienThoai.Models.Models;
using BanDienThoai.Models.Service;
using BanDienThoai.Common;
using BanDienThoai.Domain.DataContext;

namespace BanDienThoai.Controllers
{
    public class CusInfoController : Controller
    {
        // GET: CusInfo
        [HttpGet]
        public ActionResult Index()
        {
            CusInfoViewModel cus = new CusInfoViewModel();
            if(Session["Cart"] ==null)
            {
                Session["Cart"] = new Cart();
            }
            cus.cart = Session["Cart"] as Cart;
            return View(cus);
        }
        [HttpPost]
        public ActionResult Index(CusInfoViewModel model)
        {
            // kiểm tra danh sách sản phẩm về số lượng
            bool isCheck = true;
            if (Session["Cart"] == null)
            {
                Session["Cart"] = new Cart();
            }
            model.cart = Session["Cart"] as Cart;
            foreach (var item in model.cart.GetList())
            {
                if(!CusInfoService.CheckNumberProduct(item.Product.MASP,item.Quantity))
                {
                    isCheck = false;
                    break;
                }
            }
            if(!isCheck)
            {
                return RedirectToAction("Index","Cart");
            }
            //lấy Id Khách hàng
            int Id = 1;// mã khách vãn lai
            if(Session[CommonConstands.USER_SESSION]!=null)
            {
                long MaTK = (Session[CommonConstands.USER_SESSION] as UserLogin).UserID;
                Id = CusInfoService.GetIdCustomer(MaTK);
            }
            // Thêm đơn hàng
            CusInfoService.AddBill(model,Id);
            return RedirectToAction("Success");
        }

        public ActionResult Success()
        {
            Session["Cart"] = null;
            return View();
        }

    }
}