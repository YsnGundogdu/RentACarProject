using Business.Abstract;
using Castle.Core.Internal;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;
        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }
        public IResult Add(Rental entity)
        {
            var result = RentalCarControl(entity.CarId);
            if (!result.Success)
            {
                return new ErrorResult("RentalNotDelivered");
            }
            else
                _rentalDal.Add(entity);
            return new SuccessResult("ItemAdded");
            /*
            if (!entity.ReturnDate.Equals(null))
            {
                _rentalDal.Add(entity);
                return new SuccessResult();
            }
            return new ErrorResult();
            */
        }

        public IResult Delete(Rental entity)
        {
            _rentalDal.Delete(entity);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.RentalId == id));
        }
        public IResult RentalCarControl(int carId)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate == null).Any();
            if (result)
            {
                return new ErrorResult("RentalNotDelivered");
            }

            return new SuccessResult();
        }
        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails());
        }

        public IResult CheckIfCarIsAvailable(int carId, DateTime rentDate, DateTime returnDate)
        {
            var result = _rentalDal.GetAll(r => r.CarId == carId && r.ReturnDate >= rentDate);
            if (result.IsNullOrEmpty() == false)
            {
                return new ErrorResult("Araba şuan mevcut değil. Şu tarihte mevcut olacak: " +
                                       result[result.Count - 1].ReturnDate.Value.ToString("yyyy-MM-dd"));
            }

            if (rentDate > returnDate)
            {
                return new ErrorResult("Başlangıç günü bitiş gününden önce olmalıdır");
            }

            if ((returnDate - rentDate).Days > 365)
            {
                return new ErrorResult("Kiralamanız 1 yılı aşıyor");
            }

            if (rentDate < DateTime.Now || returnDate < DateTime.Now)
            {
                return new ErrorResult("Arabayı geçmiş için değil gelecek için kiralamalısınız");
            }

            return new SuccessResult("Araba mevcuttur");
        }

        public IResult Update(Rental entity)
        {
            _rentalDal.Update(entity);
            return new SuccessResult();
        }
    }
}
