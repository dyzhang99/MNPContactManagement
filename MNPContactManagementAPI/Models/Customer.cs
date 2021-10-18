using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public virtual ICollection<ContactDetail> ContactDetail { get; set; }
    }
}
