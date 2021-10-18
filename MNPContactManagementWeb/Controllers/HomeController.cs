using System.Web.Mvc; 

namespace MNPContactManagementWeb.Controllers
{
    public class HomeController : BaseController
    {  
        public ActionResult Index()
        { 
            // Load contact list into a jQueryDataTable (plugged) of this page by making an Ajax call to API
            return View();
        }         
    }
}