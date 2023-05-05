using Mobile_ecommerce.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Employee.Controllers
{
    public class EmpHomeController : BaseController
    {
        OrderDAL db = new OrderDAL();
        // GET: Employee/EmpHome
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