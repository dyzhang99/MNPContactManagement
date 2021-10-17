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
            // Current approach: Use Ajax to call API. Option: Use HttpClient to call API 
            //HttpResponseMessage response = httpClient.GetAsync(baseAddress + "/ContactDetails").Result;
            //if (response.IsSuccessStatusCode)
            //{
            //    var data = response.Content.ReadAsStringAsync().Result;
            //}            

            return View();
        }         
    }
}