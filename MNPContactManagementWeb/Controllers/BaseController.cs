using System; 
using System.Web.Mvc;
using System.Net.Http;
using log4net;

namespace MNPContactManagementWeb.Controllers
{ 
    public class BaseController : Controller
    {
        // Base Controller  variables  
        protected static readonly ILog logging = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
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