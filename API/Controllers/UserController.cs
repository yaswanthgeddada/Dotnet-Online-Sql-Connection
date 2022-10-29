using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    public class UserController : BaseApiController
    {

        [HttpGet]
        public IActionResult GetUserDetails()
        {
            return Ok("hello");
        }

    }
}