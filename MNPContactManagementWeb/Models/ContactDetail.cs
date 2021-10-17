using System;
 
namespace MNPContactManagementWeb.Models
{
    public class ContactDetail
    {
        public int ContactId { get; set; }
        public int CustomerId { get; set; }
        public string ContactName { get; set; }
        public string JobTitle { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Comments { get; set; }
        public DateTimeOffset LastDateContacted { get; set; }
    }
}