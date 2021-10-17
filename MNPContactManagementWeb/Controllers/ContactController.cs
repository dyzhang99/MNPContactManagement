using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Web.Mvc;
using System.Net.Http;

namespace MNPContactManagementWeb.Controllers
{
    using Models;
    using ViewModels;

    public class ContactController : BaseController
    {
        // GET: Contact
        [Route("Contact/{id?}")]
        public ActionResult Index(int? id)
        {
            ContactDetailVM vm = new ContactDetailVM();
            // If id is not provided, we are going to create a new contact; otherwise, updating the existing contact.
            if ((id??0) > 0)
            { 
                HttpResponseMessage responseContactInfo = httpClient.GetAsync(baseAddress + "/ContactDetails/" + id.ToString()).Result;
                if (responseContactInfo.IsSuccessStatusCode)
                {
                    var jsonContactInfo = responseContactInfo.Content.ReadAsStringAsync().Result;
                    ContactDetail contactDetail = JsonConvert.DeserializeObject<ContactDetail>(jsonContactInfo);
                    IEnumerable<Customer> CustomerList = null;

                    HttpResponseMessage responseCustomer = httpClient.GetAsync(baseAddress + "/Customers").Result;
                    if (responseCustomer.IsSuccessStatusCode)
                    {                        
                         var jsonCustomers = responseCustomer.Content.ReadAsStringAsync().Result;
                         CustomerList = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonCustomers); 
                    }

                    vm.ContactID = contactDetail.ContactId;
                    vm.ContactName = contactDetail.ContactName;
                    vm.Address = contactDetail.Address;
                    // TODO: Convert datetimeoffset value to date time
                    vm.LastDateContacted = contactDetail.LastDateContacted.ToString();
                    vm.JobTitle = contactDetail.JobTitle;
                    vm.Phone = contactDetail.Phone;
                    vm.EmailAddress = contactDetail.EmailAddress;
                    vm.Comments = contactDetail.Comments;
                    vm.SelectedCustomer = contactDetail.CustomerId; 
                    vm.CustomerList = from Customer c in CustomerList
                                      select new SelectListItem { Value = c.CustomerID.ToString(), Text = c.CustomerName };
                }
            }
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactDetailVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            } 
            else
            {
                // TODO: GO somewhere 
                return View(model);
            }            
        }
    }
}