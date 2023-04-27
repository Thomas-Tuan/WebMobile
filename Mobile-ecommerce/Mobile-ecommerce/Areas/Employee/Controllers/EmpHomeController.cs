using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Employee.Controllers
{
    public class EmpHomeController : BaseController
    {
        // GET: Employee/EmpHome
        public ActionResult Index()
        {
            return View();
        }
    }
}