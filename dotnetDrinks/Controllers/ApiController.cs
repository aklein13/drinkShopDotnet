using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetDrinks.Controllers
{
    [Produces("application/json")]
    [Route("api")]
    public class ApiController : Controller
    {
        // GET: api
        [HttpGet]
        public JsonResult GetApi()
        {
            return Json(new { message = "Welcome to Drinks API!", routes = new { drinks = "api/drinks", companies = "api/companies" } });
        }
    }
}