using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal 
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            
            _cars = new List<Car> { 
                new Car{CarId=1, BrandId=1, ColorId=2, SegmentId=2, CarDescription= "Volkswagen Passat 1.5 TSI Business - Benzinli - Otomatik - Sedan", CarModelYear=2021,CarStatus=true },
                new Car{CarId=2, BrandId=2, ColorId=1, SegmentId=1, CarDescription= "Fiat Egea 1.4 Fire BZ 95HP Easy - Benzinli - Manuel - Sedan", CarModelYear=2020,CarStatus=false },
                new Car{CarId=3, BrandId=3, ColorId=2, SegmentId=3, CarDescription= "Ford Kuga 1.5 EcoBlue Titanium - Dizel - Otomatik - SUV", CarModelYear=2021,CarStatus=true }
            };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete;

            carToDelete = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            
            _cars.Remove(carToDelete);
            
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Car> GetAllBySegment(int segmentId)
        {
            return _cars.Where(c => c.SegmentId == segmentId).ToList();
        }

        public List<CarDetailDto> GetCarDetails()
        {
            throw new NotImplementedException();
        }

        public void Update(Car car)
        {
            Car carToUpdate;

            carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.SegmentId = car.SegmentId;
            carToUpdate.CarDescription = car.CarDescription;
            carToUpdate.CarModelYear = car.CarModelYear;
            carToUpdate.CarStatus = car.CarStatus;
            

            
        }
    }
}
