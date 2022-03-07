using DataAccess.Abstract;
using Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.repository
{
    public class EnitityRepositoryBase<Tentity> //: IEntityRepository<Tentity>
        //where Tentity : class, IEntity, new()
    {
        /*
        public Context db;
        public DbSet<TEntity> _object;
        public EntityRepositoryBase()
        {
            db = new Context();
            _object = db.Set<TEntity>();
        }
        public void Add(Tentity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Tentity entity)
        {

                _object.Remove(entity);
                db.SaveChanges();
            
        }

        public Tentity Get(Expression<Func<Tentity, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Tentity> GetAll(Expression<Func<Tentity, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Tentity entity)
        {
            throw new NotImplementedException();
        }
     */   
    }
}
