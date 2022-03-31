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

        public IResult Add(Car entity)
        {
            if (entity.CarDescription.Length > 15 && entity.CarModelYear > 2015)
            {
                _carDal.Add(entity);
                return new SuccessResult("Araba başarılı bir şekilde eklendi.");
            }
            else
            {
                return new ErrorResult("Araba açıklaması 15 karakterden ve model yılı 2015'ten büyük olmalıdır.");
            }
           
        }

        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult("Araba Başarılı Bir Şekilde Silindi");
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 10)
            {
                return new ErrorDataResult<List<Car>>("Bakımda");
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),"Arabalar");
        }

        public IDataResult<List<Car>> GetAllBySegmentId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.SegmentId == id));
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
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

        public IResult Update(Car entity)
        {
            _carDal.Update(entity);
            return new SuccessResult("Araba Başarılı Bir Şekilde Güncellendi");
        }
    }
}
