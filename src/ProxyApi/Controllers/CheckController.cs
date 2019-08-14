using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProxyApi.Controllers
{
    [Route("api/check")]
    [ApiController]
    public class CheckController : ControllerBase
    {
        // GET: api/Check
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ok");
        }
    }
}
