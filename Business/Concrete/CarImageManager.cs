﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;
        IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }
        public IResult Add(IFormFile file, CarImage carImage)
        {/*
            var result = BusinessRules.Run(CheckCarImageCount(carImage.CarId));
            if (result != null)
                return result;*/

            carImage.ImagePath = _fileHelper.Upload(file, Paths.ImagesPath);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);

            return new SuccessResult("Araba fotoğrafı eklendi");
        }

        public IResult Delete(CarImage carImage)
        {
            _fileHelper.Delete(Paths.ImagesPath + carImage.ImagePath);
            _carImageDal.Delete(carImage);

            return new SuccessResult("Araba fotoğrafı silindi");        
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            var carImages = _carImageDal.GetAll(ci => ci.CarId == carId);

            if (!carImages.Any())
                return new SuccessDataResult<List<CarImage>>(GetDefaultImage().Data);

            return new SuccessDataResult<List<CarImage>>(carImages);
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.CarImageId == id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = _fileHelper.Update(file, Paths.ImagesPath + carImage.ImagePath, Paths.ImagesPath);
            _carImageDal.Update(carImage);

            return new SuccessResult("Araba fotoğrafı güncellendi");
        }

        private IDataResult<List<CarImage>> GetDefaultImage()
        {
            var image = new List<CarImage>(){
                new CarImage{
                ImagePath = "wwwroot\\DefaultCarImage.png"
                } };

            return new SuccessDataResult<List<CarImage>>();
        }
        /*
        public IResult CheckCarImageCount(int carId)
        {
            var result = _carImageDal.GetAll(c => c.CarId == carId).Count;
            if (result >= 5)
            {
                new ErrorResult();
            }
            new SuccessResult();
            
        }
        */
    }
}
