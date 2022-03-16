using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ISegmentService
    {
        IDataResult<List<Segment>> GetAll();
        IDataResult<Segment> GetById(int segmentId);
        IResult Add(Segment segment);
        IResult Update(Segment segment);
        IResult Delete(Segment segment);
    }
}
