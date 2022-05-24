using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from r in context.Rentals
                             join c in context.Cars on r.CarId equals c.CarId
                             join u in context.Users on r.CustomerId equals u.UserId
                             join l in context.Locations on r.RentLocationId equals l.LocationId
                             join rl in context.Locations on r.ReturnLocationId equals rl.LocationId
                             select new RentalDetailDto()
                             {
                                 RentalId = r.RentalId,
                                 UserFullName = u.UserFirstName +" "+ u.UserLastName,
                                 CarDescription = c.CarDescription,
                                 RentLocationDescription = l.LocationDescription,
                                 ReturnLocationDescription = rl.LocationDescription
                             };
                return result.ToList();
            }
        }
    }
}
