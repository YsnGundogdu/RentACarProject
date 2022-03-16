using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class SegmentManager : ISegmentService
    {
        ISegmentDal _segmentDal;
        public SegmentManager(ISegmentDal segmentDal)
        {
            _segmentDal = segmentDal;
        }
        public IResult Add(Segment segment)
        {
            _segmentDal.Add(segment);
            return new SuccessResult("Segment Eklendi");
        }

        public IResult Delete(Segment segment)
        {
            _segmentDal.Delete(segment);
            return new SuccessResult("Segment Silindi");
        }

        public IDataResult<List<Segment>> GetAll()
        {
            return new SuccessDataResult<List<Segment>>(_segmentDal.GetAll());
        }

        public IDataResult<Segment> GetById(int segmentId)
        {
            return new SuccessDataResult<Segment>(_segmentDal.Get(s => s.SegmentId == segmentId));
        }

        public IResult Update(Segment segment)
        {
            _segmentDal.Update(segment);
            return new SuccessResult("Segment Güncellendi");
        }
    }
}
