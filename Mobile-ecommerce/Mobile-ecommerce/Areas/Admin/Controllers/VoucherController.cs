using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.EF;
using Mobile_ecommerce.Models.ViewModel.Common;
using Mobile_ecommerce.Models.ViewModel.Voucher;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class VoucherController : BaseController
    {
        VoucherDAL db = new VoucherDAL();
        // GET: Admin/Categories
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
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {                      
            var pro = await db.GetById(id.Trim());
            return View(pro);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Edit pro)
        {
            if (ModelState.IsValid)
            {
                var result = await db.Update(pro);
                if (result)
                {
                    SetAlert("Cập nhật thành công", "success");
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            var result = await db.Delete(id.Trim());
            if (result)
            {
                SetAlert("Xóa thành công", "success");
            }
            else
            {
                SetAlert("Có lỗi xảy ra. Vui lòng thử lại!", "error");
            }
            return Json(result);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Edit detailCate)
        {
            if (ModelState.IsValid)
            {
                var result = await db.Create(detailCate);
                if (result > 0)
                {
                    SetAlert("Tạo mã khuyến mãi thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Mã này đã tồn tại");
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