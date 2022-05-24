using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class RentalDetailDto : IDto
    {
        public int RentalId { get; set; }
        public string UserFullName { get; set; }
        public string CarDescription { get; set; }
        public string RentLocationDescription { get; set; }
        public string ReturnLocationDescription { get; set; }

    }
}
