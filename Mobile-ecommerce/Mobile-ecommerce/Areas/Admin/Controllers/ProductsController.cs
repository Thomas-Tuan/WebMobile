using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.ViewModel.Common;
using Mobile_ecommerce.Models.ViewModel.Product;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class ProductsController : BaseController
    {
        ProductDAL db = new ProductDAL();
        // GET: Admin/Products
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Create()
        {
            var pro = db.Details();
            ViewBag.Image = pro.Image;
            ViewBag.ImageNote = pro.ImagePro;
            var selectColor = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Selected = true, Text ="Chọn màu sắc", Value = "-1"},
            new SelectListItem {Text = "Màu đen", Value = "đen"},
            new SelectListItem {Text = "Màu trắng", Value = "trắng"},
            new SelectListItem {Text = "Màu xanh lá cây", Value = "xanh lá cây"},
            new SelectListItem {Text = "Màu xanh dương", Value = "xanh dương"},
            new SelectListItem {Text = "Màu tím", Value = "tím"},
            new SelectListItem {Text = "Màu xám", Value = "xám"},
        }, "Value", "Text");
            ViewBag.lstColor = selectColor;              
            ViewBag.ListCate = await db.GetAllCategories();
            return View(pro);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Create create)
        {
            ViewBag.ListCate = await db.GetAllCategories();         
            if (ModelState.IsValid)
            {
                var result = await db.Create(create, Server);
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
        [HttpGet]
        public async Task<ActionResult> Edit(int id)
        {
            var pro = await db.GetById(id);
            ViewBag.Image = pro.Image;
            ViewBag.key = id;
            ViewBag.ImageNote = pro.ImagePro;
            var selectColor = new SelectList(new List<SelectListItem>
        {
            new SelectListItem { Selected = true, Text ="Chọn màu sắc", Value = "-1"},
            new SelectListItem {Text = "Màu đen", Value = "đen"},
            new SelectListItem {Text = "Màu trắng", Value = "trắng"},
            new SelectListItem {Text = "Màu xanh lá cây", Value = "xanh lá cây"},
            new SelectListItem {Text = "Màu xanh dương", Value = "xanh dương"},
            new SelectListItem {Text = "Màu tím", Value = "tím"},
            new SelectListItem {Text = "Màu xám", Value = "xám"},
        }, "Value", "Text");
            ViewBag.lstColor = selectColor;
            ViewBag.ListCate = await db.GetAllCategories();
            return View(pro);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(Edit pro, int id)
        {
            if (ModelState.IsValid)
            {
                var result = await db.UpdatePro(pro, id, Server);
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
        public async Task<ActionResult> GetPaging(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await db.GetList(request);
            foreach (var item in data.Items)
            {
                CultureInfo culture = new CultureInfo("en-US");
                decimal value = decimal.Parse(item.Price, NumberStyles.AllowThousands);            
                item.Price = string.Format(culture, "{0:N0}", value); ;
            }
            int totalRecord = data.TotalRecord;
            int totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, totalPage = totalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }
    }   
}
