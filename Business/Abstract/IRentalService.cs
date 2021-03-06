using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IRentalService : IBaseService<Rental>
    {
        IDataResult<List<RentalDetailDto>> GetRentalDetails();
        IResult RentalCarControl(int CarId);
        IResult CheckIfCarIsAvailable(int carId, DateTime rentDate, DateTime returnDate);
    }
}
