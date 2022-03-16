using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public IResult Add(Car car)
        {
            if (car.CarDescription.Length > 15 && car.CarModelYear > 2015)
            {
                _carDal.Add(car);
                return new SuccessResult("Araba başarılı bir şekilde eklendi.");
            }
            else
            {
                return new ErrorResult("Araba açıklaması 15 karakterden ve model yılı 2015'ten büyük olmalıdır.");
            }
           
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult();
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 12)
            {
                return new ErrorDataResult<List<Car>>("Bakımda");
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),"Arabalar");
        }

        public IDataResult<List<Car>> GetAllBySegmentId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.SegmentId == id));
        }

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>> (_carDal.GetCarDetails()); 
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == brandId));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == colorId));
        }

        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult();
        }
    }
}
