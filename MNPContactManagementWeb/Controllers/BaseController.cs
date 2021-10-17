using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using log4net;

namespace MNPContactManagementWeb.Controllers
{
    using Helpers;

    public class BaseController : Controller
    {
        protected Logger logger = Logger.Current;

        // Current approach: Use Ajax to call API. 
        // Settings when using HttpClient to call API from Controller
        protected HttpClient httpClient;
        protected Uri baseAddress = new Uri("https://localhost:44369/api");

        public BaseController()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
        }
    } 
}