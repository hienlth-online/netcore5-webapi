using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBooks.Controllers.v1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.2")]
    [ApiVersion("1.4")]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Test controller version 1.0");
        }

        [HttpGet, MapToApiVersion("1.2")]
        public IActionResult Get12()
        {
            return Ok("Test controller version 1.2");
        }

        [HttpGet, MapToApiVersion("1.4")]
        public IActionResult Get14()
        {
            return Ok("Test controller version 1.4");
        }
    }
}
