using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.ViewModel.Category;
using Mobile_ecommerce.Models.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class CategoriesController : BaseController
    {
        CategoryDAL db = new CategoryDAL();
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
        public async Task<ActionResult> Edit(int id)
        {
            var pro = await db.GetById(id);          
            return View(pro);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(DetailCate pro)
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
        public async Task<ActionResult> Delete(int id)
        {
            var result = await db.Delete(id);
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
        public async Task<ActionResult> Create(DetailCate detailCate)
        {
            if (ModelState.IsValid)
            {
                var result = await db.Create(detailCate);
                if (result > 0)
                {
                    SetAlert("Tạo sản phẩm thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Sản phẩm này đã tồn tại");
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