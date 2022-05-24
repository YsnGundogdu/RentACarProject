using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class CarDetailDto :IDto 
    {
        public int CarId { get; set; }
        public int BrandId { get; set; }
        public int SegmentId { get; set; }
        public int ColorId { get; set; }
        public string BrandName { get; set; }
        public short CarModelYear { get; set; }
        public string CarDescription { get; set; }
        public string ColorName { get; set; }
        public string SegmentName { get; set; }
        public decimal DailyPrice { get; set; }

    }
}
