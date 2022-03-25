using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class LocationManager : ILocationService
    {
        ILocationDal _locationDal;
        public LocationManager(ILocationDal locationDal)
        {
            _locationDal = locationDal;
        }
        public IResult Add(Location entity)
        {
            _locationDal.Add(entity);
            return new SuccessResult("Lokasyon Eklendi");
        }

        public IResult Delete(Location entity)
        {
            _locationDal.Add(entity);
            return new SuccessResult("Lokasyon Silindi");
        }

        public IDataResult<List<Location>> GetAll()
        {
            return new SuccessDataResult<List<Location>>(_locationDal.GetAll());
        }

        public IDataResult<Location> GetById(int id)
        {
            return new SuccessDataResult<Location>(_locationDal.Get(l => l.LocationId == id));
        }

        public IResult Update(Location entity)
        {
            _locationDal.Add(entity);
            return new SuccessResult("Lokasyon Güncelledi");
        }
    }
}
