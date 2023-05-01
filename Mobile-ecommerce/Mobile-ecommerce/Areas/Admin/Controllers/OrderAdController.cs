using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.ViewModel.Common;
using Mobile_ecommerce.Models.ViewModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class OrderAdController : BaseController
    {
        OrderDAL db = new OrderDAL();
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
            var data = await db.GetList(request);
            int totalRecord = data.TotalRecord;
            int totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, totalPage = totalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }
        // GET: Order/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var pro = await db.GetByIdInfo(id);
            pro.ToList();
            return View(pro);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var pro = await db.GetById(id);
            ViewBag.ListShipping = await db.GetAllShipping();
            ViewBag.ListStatus = await db.GetAllStatus();
            return View(pro);
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Edit pro, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await db.UpdatePro(pro, id);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}