using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : BaseController<Rental, IRentalService>
    {
        IRentalService _rentalService;

        public RentalsController(IRentalService rentalService):base(rentalService)
        {
            _rentalService = rentalService;
        }
        [HttpGet("getrentaldetails")]
        public IActionResult GetRentalDetails()
        {
            var result = _rentalService.GetRentalDetails();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("checkifcarisavailable")]
        public IActionResult CheckIfCarIsAvailable(int carId, DateTime rentDate, DateTime returnDate)
        {
            var result = _rentalService.CheckIfCarIsAvailable(carId, rentDate, returnDate);
            return Ok(result);
        }
    }
}
