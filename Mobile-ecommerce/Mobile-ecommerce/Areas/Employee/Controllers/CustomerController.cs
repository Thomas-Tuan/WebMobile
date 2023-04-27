using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.ViewModel.Common;
using Mobile_ecommerce.Models.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Employee.Controllers
{
    public class CustomerController : BaseController
    {
        CusDAL cusdb = new CusDAL();
        // GET: Admin/Customer
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
            var data = await cusdb.GetList(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }
        public ActionResult Create()
        {
            var cus = cusdb.Details();
            ViewBag.Image = cus.Image;
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(Create create)
        {          
            if (ModelState.IsValid)
            {
                var result = await cusdb.Create(create,Server);
                ViewBag.Image = create.Image;
                if (result > 0)
                {
                    SetAlert("Tạo khách hàng thành công", "success");
                    return RedirectToAction("Index");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "khách hàng này đã tồn tại");
                }
                else if (result == -1)
                {
                    SetAlert("Đã có lỗi xảy ra. Vui lòng thử lại", "error");
                }
            }
            return View();
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var cus = await cusdb.GetById(id);
            ViewBag.Image = cus.Image;
            return View(cus);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Edit cus,int id)
        {
            if (ModelState.IsValid)
            {
                var result = await cusdb.EditCus(cus,id,Server);
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