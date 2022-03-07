using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : ICarDal
    {
        public void Add(Car entity)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var addedEntity = context.Entry(entity); //Referans yakalama
                addedEntity.State = EntityState.Added;   //Eklenilecek nesne
                context.SaveChanges();                   //Eklendi
            }
        }

        public void Delete(Car entity)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var deletedEntity = context.Entry(entity);   //Referans yakalama
                deletedEntity.State = EntityState.Deleted;   //Silinecek nesne
                context.SaveChanges();                       //Silindi
            }
        }

        public Car Get(Expression<Func<Car, bool>> filter)
        {
            using (RentACarContext context = new RentACarContext())
            {
                return context.Set<Car>().SingleOrDefault(filter); //Tek data getirme
            }
        }

        public List<Car> GetAll(Expression<Func<Car, bool>> filter = null)
        {
            using (RentACarContext context=new RentACarContext())
            {
                return filter == null 
                    ? context.Set<Car>().ToList()                //filtre null'sa Select * from cars
                    : context.Set<Car>().Where(filter).ToList(); //filtre varsa, filtreleyip ver
            }
        }

        public void Update(Car entity)
        {
            using (RentACarContext context = new RentACarContext())
            {
                var updatedEntity = context.Entry(entity);   //Referans yakalama
                updatedEntity.State = EntityState.Modified;  //Güncellecek nesne
                context.SaveChanges();                       //Güncellendi
            }
        }
    }
}
