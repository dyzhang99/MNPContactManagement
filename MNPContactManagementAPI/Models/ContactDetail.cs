using System;
using System.Collections.Generic;

namespace MNPContactManagementAPI.Models
{
    public partial class ContactDetail
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

        public virtual Customer Customer { get; set; }
    }
}
