using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPortal.Controllers
{
    [Route("Error/{statusCode}")]
    public class ErrorController : Controller
    {
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    break;
                case 500:
                    ViewBag.ErrorMessage = "Sorry, something wrong with our server.";
                    break;
                default:
                    ViewBag.ErrorMessage = "Unknown error.";
                    break;
            }

            return View("CustomisedError");
        }
    }
}
