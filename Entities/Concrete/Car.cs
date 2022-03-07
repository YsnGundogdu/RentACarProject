using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Car:IEntity
    {
        public int CarId { get; set; }      //Primary Key
        public int BrandId { get; set; }    //Foreign Key
        public int SegmentId { get; set; }  //Foreign Key
        public int ColorId { get; set; }    //Foreign Key
        public bool CarStatus{ get; set; }
        public short CarModelYear { get; set; }
        public string CarDescription { get; set; }
        
    }
}
