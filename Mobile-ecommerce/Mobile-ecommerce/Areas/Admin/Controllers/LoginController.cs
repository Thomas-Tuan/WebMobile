using Mobile_ecommerce.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Mobile_ecommerce.Common;
using System.Threading.Tasks;
using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Areas.Admin.Model;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {     
        public ActionResult Index()
        {
            Session[CommonConstants.ADMIN_SESSION] = null;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Check(LoginCheck user)
        {
            UserDAL userdb = new UserDAL();
            var ketQua = await userdb
                .Login(user.UserName, user.PassWord, CommonConstants.QUAN_TRI);

            if (ketQua == 1)
            {
                var userLogin = await userdb.GetAdminUserName(user.UserName);
                Session[CommonConstants.ADMIN_SESSION] = userLogin;
                TempData["AlertMessage"] = "Đăng nhập thành công !";
                TempData["AlertType"] = "alert-success";
                return RedirectToAction("Index", "AdminHome");
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