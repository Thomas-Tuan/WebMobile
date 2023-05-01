using Mobile_ecommerce.Areas.Admin.Model;
using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class AdminHomeController : BaseController
    {
        OrderDAL db = new OrderDAL();
        // GET: Admin/AdminHome
        public ActionResult Index()
        {
            ViewBag.SlNhanVien = new UserDAL().NumEmployee();
            ViewBag.SlTaikhoan = new UserDAL().NumUser();
            ViewBag.SlKhachHang = new UserDAL().NumCustomer();
            ViewBag.TongTien = db.GetTotalOrder();
            return View();
        }       
    }
}