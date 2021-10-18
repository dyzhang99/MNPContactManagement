using System; 
using System.Web.Mvc;
using System.Net.Http;
using log4net;

namespace MNPContactManagementWeb.Controllers
{
    using Helpers;

    public class BaseController : Controller
    { 
        // Control-shared variables
        protected Logger logger = Logger.Current;         
        protected HttpClient httpClient;
        protected Uri baseAddress = new Uri("https://localhost:44369/api");

        public BaseController()
        {
            // Create an instance of HttpClient
            httpClient = new HttpClient();
            httpClient.BaseAddress = baseAddress;
        }
    } 
}