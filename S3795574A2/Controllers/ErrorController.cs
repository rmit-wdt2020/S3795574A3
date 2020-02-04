using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace S3795574A2.Controllers
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult PageNotFound()
        {
            ViewBag.Error = "404";
            return View();
        }
        [Route("500")]
        public IActionResult InternalServerError()
        {
            ViewBag.Error = "500";
            return View();
        }
    }
}
