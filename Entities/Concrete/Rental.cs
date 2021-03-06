using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Rental : IEntity
    {
        public int RentalId { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public int RentLocationId { get; set; }
        public int ReturnLocationId { get; set; }
        public DateTime RentDate { get; set; } = DateTime.Now.Date;
        public DateTime? ReturnDate { get; set; } = null;

    }
}
