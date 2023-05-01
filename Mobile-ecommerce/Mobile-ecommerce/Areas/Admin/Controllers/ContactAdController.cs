using Mobile_ecommerce.Models.DAL;
using Mobile_ecommerce.Models.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class ContactAdController : BaseController
    {
        CusDAL cusdb = new CusDAL();
        // GET: Admin/ContactAd
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Info(int id)
        {
            var item = await cusdb.GetByIdContact(id);           
            return View(item);
        }
        public async Task<ActionResult> GetPaging(string keyWord, int pageIndex = 1, int pageSize = 5)
        {
            var request = new GetListPaging()
            {
                keyWord = keyWord,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var data = await cusdb.GetListContact(request);
            int totalRecord = data.TotalRecord;
            int toalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
            return Json(new { data = data.Items, pageCurrent = pageIndex, toalPage = toalPage, totalRecord = totalRecord }
                , JsonRequestBehavior.AllowGet);

            // Datetime:  .NET JavaScriptSerializer
        }
    }
}