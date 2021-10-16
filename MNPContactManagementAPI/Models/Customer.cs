using System;
using System.Collections.Generic;

namespace MNPContactManagementAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            ContactDetail = new HashSet<ContactDetail>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }

        public virtual ICollection<ContactDetail> ContactDetail { get; set; }
    }
}
