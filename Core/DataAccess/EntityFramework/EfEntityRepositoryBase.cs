using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext> : IEntityRepository<TEntity>
        where TEntity: class, IEntity, new()
        where TContext: DbContext, new()

    {
        public void Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); //Referans yakalama
                addedEntity.State = EntityState.Added;   //Eklenilecek nesne
                context.SaveChanges();                   //Eklendi
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);   //Referans yakalama
                deletedEntity.State = EntityState.Deleted;   //Silinecek nesne
                context.SaveChanges();                       //Silindi
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter); //Tek data getirme
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()                //filtre null'sa Select * from cars benzeri
                    : context.Set<TEntity>().Where(filter).ToList(); //filtre varsa, filtreleyip ver
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);   //Referans yakalama
                updatedEntity.State = EntityState.Modified;  //Güncellecek nesne
                context.SaveChanges();                       //Güncellendi
            }
        }
    }
}
