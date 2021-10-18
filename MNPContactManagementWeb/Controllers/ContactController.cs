using System.Collections.Generic;
using System.Linq; 
using Newtonsoft.Json; 
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
            // This Index method gets called when user clicks Creeate New Contact button or selects an existing contact on the home page.

            // Blank Contact Details form or the form with specified contact's details loaded; depend on if id is provided or not. 
            ContactDetailVM vm = new ContactDetailVM();

            // Call API to get list fo Company DDL as the DDL control needs to have items regardless.
            // Initialize a CustomerList collection
            IEnumerable<Customer> CustomerList = Enumerable.Empty<Customer>();
             
            using (HttpResponseMessage responseCustomer = httpClient.GetAsync(baseAddress + "/Customers").Result)
            if (responseCustomer.IsSuccessStatusCode)
            {
                var jsonCustomers = responseCustomer.Content.ReadAsStringAsync().Result;
                CustomerList = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonCustomers);
            }

            // Assign to ContactDetailVM's CustomerList memeber
            vm.CustomerList = from Customer c in CustomerList
                              select new SelectListItem { Value = c.CustomerID.ToString(), Text = c.CustomerName };


            // If id is provided, we are going to call API to get the specified Contact's detailes to the form for reviewing and/or updating; otherwise display a blank form.
            if ((id??0) > 0)
            {
                using (HttpResponseMessage responseContactInfo = httpClient.GetAsync(baseAddress + "/ContactDetails/" + id.ToString()).Result)
                {
                    if (responseContactInfo.IsSuccessStatusCode)
                    {
                        // Assign values returned from API call to ContactDetailVM's memebers
                        var jsonContactInfo = responseContactInfo.Content.ReadAsStringAsync().Result;
                        ContactDetail contactDetail = JsonConvert.DeserializeObject<ContactDetail>(jsonContactInfo); 

                        vm.ContactID = contactDetail.ContactId;
                        vm.ContactName = contactDetail.ContactName;
                        vm.Address = contactDetail.Address;
                        // TODO: Convert datetimeoffset value to date time
                        vm.LastDateContacted = contactDetail.LastDateContacted.ToString().Trim().Substring(0,10); //TODO: Need a function to handle it 
                        vm.JobTitle = contactDetail.JobTitle;
                        vm.Phone = contactDetail.Phone;
                        vm.EmailAddress = contactDetail.EmailAddress;
                        vm.Comments = contactDetail.Comments;
                        vm.CustomerID = contactDetail.CustomerId;
                    }
                } 
            }            
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(ContactDetailVM model)
        {
            // This Index method gets called after clicking Subtmit button.

            ViewBag.Message = string.Empty;

            // Call API to get list fo Company DDL as the DDL control needs to have items to be recreated
            // Initialize a CustomerList collection
            IEnumerable<Customer> CustomerList = Enumerable.Empty<Customer>();

            using (HttpResponseMessage responseCustomer = httpClient.GetAsync(baseAddress + "/Customers").Result)
            if (responseCustomer.IsSuccessStatusCode)
            {
                var jsonCustomers = responseCustomer.Content.ReadAsStringAsync().Result;
                CustomerList = JsonConvert.DeserializeObject<IEnumerable<Customer>>(jsonCustomers);
            }

            // Assign to ContactDetailVM's CustomerList memeber
            model.CustomerList = from Customer c in CustomerList
                              select new SelectListItem { Value = c.CustomerID.ToString(), Text = c.CustomerName };
             
            if (ModelState.IsValid)
            {
                // Specify HttpRequest headers
                //httpClient.DefaultRequestHeaders
                //                .Accept
                //                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                // Call API to create or update Contact Details    

                dynamic jsData = JsonConvert.SerializeObject(model);
                ContactDetail contactDetail = JsonConvert.DeserializeObject<ContactDetail>(jsData);

                if (model.ContactID > 0)                    
                {
                    // Update an existing Contact 
                    using (var content = new StringContent(JsonConvert.SerializeObject(contactDetail), System.Text.Encoding.UTF8, "application/json"))
                    {
                        // NOTE: PUT method's call needs to have the ID in the URL, otherwise API will return 'Method Not Allowed' error.
                        HttpResponseMessage result = httpClient.PutAsync(baseAddress + "/ContactDetails/" + model.ContactID, content).Result;                         
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Contact updated";
                        }
                        else 
                        {
                            ViewBag.Message = "Update failed";
                            // TODO: log error                             
                        } 
                    }          
                }
                else
                {
                    // Create a new Contact
                    using (var content = new StringContent(JsonConvert.SerializeObject(contactDetail), System.Text.Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage result = httpClient.PostAsync(baseAddress + "/ContactDetails", content).Result;
                        if (result.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Contact added";
                        }
                        else
                        {
                            ViewBag.Message = "Failed";
                            // TODO: log error 
                        }
                    }
                }
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