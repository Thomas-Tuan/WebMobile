﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Mobile_ecommerce.Areas.Admin.Controllers
{
    public class AdminHomeController : BaseController
    {
        // GET: Admin/AdminHome
        public ActionResult Index()
        {         
            return View();
        }
    }
}