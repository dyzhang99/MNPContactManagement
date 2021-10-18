using System.Web.Mvc; 

namespace MNPContactManagementWeb.Controllers
{
    public class HomeController : BaseController
    {  
        public ActionResult Index()
        {
            // Load data into a jQueryDataTable plugged into this page by making an Ajax call to API
            return View();
        }         
    }
}