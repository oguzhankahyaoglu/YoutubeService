using System;
using Microsoft.AspNetCore.Mvc;

namespace YoutubeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Test : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<DateTime> Index()
        {
            return DateTime.Now;
        }
    }
}