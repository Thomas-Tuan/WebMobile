using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Data.Entity.Validation;
using System.Web.Mvc;
using System.Threading.Tasks;
using Mobile_ecommerce.Models.ViewModel.Common;
using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.ViewModel.User;

namespace Mobile_ecommerce.Areas.Employee.Controllers
{
    public class UserController : BaseController
    {
        UserDAL userdb = new UserDAL();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> GetPaging(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await userdb.GetList(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }
        public async Task<ActionResult> Create()
        {
            ViewBag.ListRole = await userdb.GetAllRole();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Create create)
        {
            ViewBag.ListRole = await userdb.GetAllRole();
            if (ModelState.IsValid)
            {
                var result = await userdb.Create(create);
                if (result > 0)
                {
                    SetAlert("Tạo tài khoản thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Email này đã tồn tại");
                }
                else if (result == -1)
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }        
    }
}
