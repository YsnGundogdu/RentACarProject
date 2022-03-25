using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public int CustomerId { get; set; }
        public string CustomerFirstName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerPassword { get; set; }
        public string CustomerIdentityNumber { get; set; }
        public string CustomerUserName { get; set; }
        public bool CustomerStatus { get; set; }
        public DateTime CustomerDateTime { get; set; }

    }
}
