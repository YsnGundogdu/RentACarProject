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
    public class SegmentsController : BaseController<Segment, ISegmentService>
    {
        ISegmentService _segmentService;

        public SegmentsController(ISegmentService segmentService):base(segmentService)
        {
            _segmentService = segmentService;
        }
    }
}
