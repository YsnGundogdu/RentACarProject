using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Location : IEntity
    {
        public int LocationId { get; set; }
        public string CityName { get; set; }
        public string LocationDescription { get; set; }
    }
}
