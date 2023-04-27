using Mobile_ecommerce.Areas.Admin.Model;
using Mobile_ecommerce.Common;
using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Employee.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            Session[CommonConstants.EMPLOYER_SESSION] = null;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Check(LoginCheck user)
        {
            UserDAL userdb = new UserDAL();
            var ketQua = await userdb
                .Login(user.UserName, user.PassWord, CommonConstants.NHAN_VIEN);

            if (ketQua == 1)
            {
                var userLogin = await userdb.GetEmpUserName(user.UserName);
                Session[CommonConstants.EMPLOYER_SESSION] = userLogin;
                TempData["AlertMessage"] = "Đăng nhập thành công !";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index", "EmpHome");
            }
            else if (ketQua == 0)
            {
                ModelState.AddModelError("CustomError", "Tài khoản đã bị khóa");
            }
            else if (ketQua == -1)
            {
                ModelState.AddModelError("CustomError", "Sai tài khoản hoặc mật khẩu");
            }
            return View("Index");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return View("Index");
        }

    }
}