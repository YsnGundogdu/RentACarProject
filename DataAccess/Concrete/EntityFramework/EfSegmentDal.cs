using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSegmentDal : ISegmentDal
    {
        public void Add(Segment entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Segment entity)
        {
            throw new NotImplementedException();
        }

        public Segment Get(Expression<Func<Segment, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Segment> GetAll(Expression<Func<Segment, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public void Update(Segment entity)
        {
            throw new NotImplementedException();
        }
    }
}
