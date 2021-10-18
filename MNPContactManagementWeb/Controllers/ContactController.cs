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
                    else
                    { 
                        logging.Error(string.Format("responseContactInfo StatusCode: {0} Reason: {1})", responseContactInfo.StatusCode, responseContactInfo.ReasonPhrase));
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

            // ViewBag stored Success or Failure message that will be displayed on page, it should always been cleaned up.
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
            else
            {
               logging.Error(string.Format("responseCustomer StatusCode: {0} Reason: {1})", responseCustomer.StatusCode, responseCustomer.ReasonPhrase));
            }  

            // Assign to ContactDetailVM's CustomerList memeber
            model.CustomerList = from Customer c in CustomerList
                              select new SelectListItem { Value = c.CustomerID.ToString(), Text = c.CustomerName };
             
            if (ModelState.IsValid)
            {
                // Form validation has passed, serialize form's View Model object to JSON.
                dynamic jsonData = JsonConvert.SerializeObject(model);
                // Deserialize View Model's JSON data into ContactDetail object
                ContactDetail contactDetail = JsonConvert.DeserializeObject<ContactDetail>(jsonData);

                if (model.ContactID > 0)                    
                {
                    // Update an existing Contact 
                    using (var content = new StringContent(JsonConvert.SerializeObject(contactDetail), System.Text.Encoding.UTF8, "application/json"))
                    {
                        // NOTE: PUT method's call needs to have the ID in the URL, otherwise API will return 'Method Not Allowed' error.
                        HttpResponseMessage responsePutContactDetails = httpClient.PutAsync(baseAddress + "/ContactDetails/" + model.ContactID, content).Result;                         
                        if (responsePutContactDetails.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Contact updated successfully";
                        }
                        else 
                        {
                            ViewBag.Message = "Contact update failed"; 
                            logging.Error(string.Format("responsePutContactDetails StatusCode: {0} Reason: {1})", responsePutContactDetails.StatusCode, responsePutContactDetails.ReasonPhrase)); 
                        } 
                    }          
                }
                else
                {
                    // Create a new Contact
                    using (var content = new StringContent(JsonConvert.SerializeObject(contactDetail), System.Text.Encoding.UTF8, "application/json"))
                    {
                        HttpResponseMessage responsePostContactDetails = httpClient.PostAsync(baseAddress + "/ContactDetails", content).Result;
                        if (responsePostContactDetails.IsSuccessStatusCode)
                        {
                            ViewBag.Message = "Contact added";
                        }
                        else
                        {
                            ViewBag.Message = "Failed";
                            logging.Error(string.Format("responsePostContactDetails StatusCode: {0} Reason: {1})", responsePostContactDetails.StatusCode, responsePostContactDetails.ReasonPhrase));
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