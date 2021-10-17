using System; 
using System.Collections.Generic;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MNPContactManagementWeb.ViewModels
{
    using Models;

    public class ContactDetailVM
    {
        public int ContactID { get; set; }

        [MinLength(3, ErrorMessage = "3- 100 characters"), MaxLength(100, ErrorMessage = "3 - 100 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Name:")]
        public string ContactName { get; set; }

        [MinLength(3, ErrorMessage = "3- 20 characters"), MaxLength(20, ErrorMessage = "3 - 20 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Address:")] 
        public string Address { get; set; }

        [MinLength(3, ErrorMessage = "3- 20 characters"), MaxLength(20, ErrorMessage = "3 - 20 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Last Date Contacted:")] 
        public string LastDateContacted { get; set; }

        [MinLength(3, ErrorMessage = "3- 20 characters"), MaxLength(20, ErrorMessage = "3 - 20 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Job Title:")] 
        public string JobTitle { get; set; }

        [MinLength(3, ErrorMessage = "3- 20 characters"), MaxLength(20, ErrorMessage = "3 - 20 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Phone:")]
        public string Phone { get; set; }

        [MinLength(3, ErrorMessage = "3- 20 characters"), MaxLength(20, ErrorMessage = "3 - 20 characters")]
        [DisplayName("Email:")]
        public string EmailAddress { get; set; }

        [MinLength(3, ErrorMessage = "3- 20 characters"), MaxLength(20, ErrorMessage = "3 - 20 characters")]
        [DisplayName("Comments:")]
        public string Comments { get; set; }

        [DisplayName("Company:")] 
        public int? SelectedCustomer { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }
    }
}