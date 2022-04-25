using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
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
        ISegmentService _segmentService;

        public CarManager(ICarDal carDal, ISegmentService segmentService)
        {
            _carDal = carDal;
            _segmentService = segmentService;
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car entity)
        {
            IResult result = BusinessRules.Run(CheckIfCarCountOfSegmentCorrect(entity.SegmentId),
                CheckIfCarDescriptionExists(entity.CarDescription),
                CheckIfSegmentLimitExceded());
            if (result != null)
            {
                return result;
            }

            _carDal.Add(entity);
            return new SuccessResult("Araba başarılı bir şekilde eklendi.");
            
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

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car entity)
        {
            IResult result = BusinessRules.Run(CheckIfCarCountOfSegmentCorrect(entity.SegmentId),
                CheckIfCarDescriptionExists(entity.CarDescription),
                CheckIfSegmentLimitExceded());
            if (result != null)
            {
                return result;
            }
            _carDal.Update(entity);
            return new SuccessResult("Araba Başarılı Bir Şekilde Güncellendi");
        }

        private IResult CheckIfCarCountOfSegmentCorrect(int segmentId)
        {
            var result = _carDal.GetAll(p => p.SegmentId == segmentId).Count;
            if (result >= 15)
            {
                return new ErrorResult("Bir Segmentte en fazla 15 araba olabilir");
            }
            return new SuccessResult();
        }

        private IResult CheckIfCarDescriptionExists(string carDescription)
        {
            var result = _carDal.GetAll(p => p.CarDescription == carDescription).Count;
            if (result >= 0)
            {
                return new ErrorResult("Bu açıklamaya ait zaten bir araba var");
            }
            return new SuccessResult();
        }

        private IResult CheckIfSegmentLimitExceded()
        {
            var result = _segmentService.GetAll();
            if (result.Data.Count > 8)
            {
                return new ErrorResult("Segment Limiti Aşıldı");
            }
            return new SuccessResult();
        }
    }
}
