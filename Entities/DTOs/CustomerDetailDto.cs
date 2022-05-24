﻿using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CustomerDetailDto : IDto
    {
        public int CustomerId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string CustomerIdentityNumber { get; set; }
        public string UserEmail { get; set; }
        public bool CustomerStatus { get; set; }

    }
}
