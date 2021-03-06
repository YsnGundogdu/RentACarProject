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
    public class ColorsController : BaseController<Color, IColorService>
    {
        IColorService _colorService;

        public ColorsController(IColorService colorService):base(colorService)
        {
            _colorService = colorService;
        }
    }
}
