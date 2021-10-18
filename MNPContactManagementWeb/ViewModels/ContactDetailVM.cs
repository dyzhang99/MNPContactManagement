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

        [MinLength(3, ErrorMessage = "2- 100 characters"), MaxLength(100, ErrorMessage = "2 - 100 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Name:")]
        public string ContactName { get; set; }

        [MinLength(3, ErrorMessage = "2- 200 characters"), MaxLength(200, ErrorMessage = "2 - 200 characters")]
        [DisplayName("Address:")]
        public string Address { get; set; }
                 
        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"^\d{2}\/\d{2}\/\d{4}$", ErrorMessage = "format: mm/dd/yyyy")]
        [DisplayName("Last Date Contacted")]
        public string LastDateContacted { get; set; }

        [MinLength(3, ErrorMessage = "2- 50 characters"), MaxLength(50, ErrorMessage = "2 - 50 characters")]
        [DisplayName("Job Title:")]
        public string JobTitle { get; set; }

        [Required(ErrorMessage = "Required")]
        [RegularExpression(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}$", ErrorMessage = "Invalid format")]          
        [DisplayName("Phone:")]
        public string Phone { get; set; }

     
        [MinLength(4, ErrorMessage = "4 - 150 characters"), MaxLength(150, ErrorMessage = "4 - 150 characters")]
        [RegularExpression(@"^([A-Za-z0-9]((\.(?!\.))|[A-Za-z0-9_+-])*)(?<=[A-Za-z0-9_-])@([A-Za-z0-9][A-Za-z0-9-]*(?<=[A-Za-z0-9])\.)+[A-Za-z0-9][A-Za-z0-9-]{0,22}(?<=[A-Za-z0-9])$", ErrorMessage = "Invalid format")]
        [DisplayName("Email:")]
        public string EmailAddress { get; set; }

        [MinLength(2, ErrorMessage = "2 - 500 characters"), MaxLength(500, ErrorMessage = "2 - 500 characters")]
        [Required(ErrorMessage = "Required")]
        [DisplayName("Comments:")]
        public string Comments { get; set; }

        [DisplayName("Company:")]
        [Required(ErrorMessage = "Required")]
        public int? CustomerID { get; set; }
        public IEnumerable<SelectListItem> CustomerList { get; set; }          
    }
}