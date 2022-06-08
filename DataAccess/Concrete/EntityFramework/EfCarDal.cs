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
        public List<CarDetailDto> GetCarDetails(Expression<Func<CarDetailDto, bool>> filter = null)
        {
            using (RentACarContext context = new RentACarContext())
            {
                
                var result = from c in context.Cars
                             join b in context.Brands on c.BrandId equals b.BrandId
                             join s in context.Segments on c.SegmentId equals s.SegmentId
                             join r in context.Colors on c.ColorId equals r.ColorId
                             select new CarDetailDto {
                                        CarId = c.CarId,
                                        BrandId = b.BrandId,
                                        ColorId = c.ColorId,
                                        SegmentId = s.SegmentId,
                                        CarModelYear = c.CarModelYear,
                                        BrandName = b.BrandName, 
                                        CarDescription = c.CarDescription,
                                        DailyPrice = s.DailyPrice,
                                        SegmentName = s.SegmentName,
                                        ColorName = r.ColorName,
                                        ImagePath = (from m in context.CarImages 
                                                     where m.CarId == c.CarId
                                                     select m.ImagePath).FirstOrDefault()
                                         
                             };

                //return result.ToList();
                
                
                return filter == null
                    ? result.ToList()
                    : result.Where(filter)
                    .ToList();
               
            }
            
        }
    }
}
