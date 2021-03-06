using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
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
        private IBrandService _brandService;
        private IColorService _colorService;


        public CarManager(ICarDal carDal, ISegmentService segmentService, IBrandService brandService, IColorService colorService)
        {
            _carDal = carDal;
            _brandService = brandService;
            _colorService = colorService;
            _segmentService = segmentService;
        }

        [SecuredOperation("car.add")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
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
            return new SuccessResult("Araba eklendi.");
            
        }

        public IResult Delete(Car entity)
        {
            _carDal.Delete(entity);
            return new SuccessResult("Araba Başarılı Bir Şekilde Silindi");
        }
        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour == 8)
            {
                return new ErrorDataResult<List<Car>>("Bakımda");
            }
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(),"Arabalar");
        }

        public IDataResult<List<CarDetailDto>> GetCarsBySegmentId(int segmentId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.SegmentId == segmentId));
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == id));
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>> (_carDal.GetCarDetails()); 
        }

        public IDataResult<List<CarDetailDto>> GetCarDetailsByCarId(int carId)
        {
            var result = BusinessRules.Run(IsCarExists(carId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>();
            }
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(x => x.CarId == carId));
        }
        public IDataResult<List<CarDetailDto>> GetCarDetailsByColorAndByBrand(int colorId, int brandId)
        {
            var result = BusinessRules.Run(IsColorExists(colorId), IsBrandExists(brandId));
            if (result != null)
            {
                return new ErrorDataResult<List<CarDetailDto>>();
            }


            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId && c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByBrandId(int brandId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.BrandId == brandId));
        }

        public IDataResult<List<CarDetailDto>> GetCarsByColorId(int colorId)
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(c => c.ColorId == colorId));
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
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
            if (result > 0)
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

        private IResult IsCarExists(int carId)
        {
            var result = _carDal.GetByID(carId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IResult IsColorExists(int colorId)
        {
            var result = _colorService.GetById(colorId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        private IResult IsBrandExists(int brandId)
        {
            var result = _brandService.GetById(brandId);
            if (result != null)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
    }
}
