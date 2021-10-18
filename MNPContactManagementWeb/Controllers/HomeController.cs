using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace MNPContactManagementWeb.Controllers
{
    public class HomeController : BaseController
    { 
        public ActionResult Index()
        {
            logger.Debug("Hey");
            return View();
        }         
    }
}