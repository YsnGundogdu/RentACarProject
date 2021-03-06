using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Segment:IEntity
    {
        public int SegmentId { get; set; }
        public string SegmentName{ get; set; }
        public string SegmentDescription { get; set; }
        public decimal DailyPrice{ get; set; }
        public decimal WeeklyPrice{ get; set; }
        public decimal MonthlyPrice{ get; set; }
    }
}
