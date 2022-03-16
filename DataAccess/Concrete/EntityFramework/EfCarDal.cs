using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetCarDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result = from c in context.Cars
                             join s in context.Segments on c.SegmentId equals s.SegmentId
                             join r in context.Colors on c.ColorId equals r.ColorId
                             select new CarDetailDto {
                                        CarId = c.CarId,
                                        CarDescription = c.CarDescription,
                                        DailyPrice = s.DailyPrice,
                                        SegmentName = s.SegmentName,
                                        ColorName = r.ColorName
                             };
                return result.ToList();
            }
            
        }
    }
}
