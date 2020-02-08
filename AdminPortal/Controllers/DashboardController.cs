using AdminPortal.Attributes;
using AdminPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminWeb.Controllers
{
    [AuthorizeAdmin]
    [Route("/SecureDashboard/{Action}")]
    public class DashboardController:Controller
    {
        //the home page that display all users
        public async Task<IActionResult> Index()
        {
            var response = await NwbaAPI.InitializeClient().GetAsync("api/customer");

            if (!response.IsSuccessStatusCode)
                throw new Exception();              
            var result = response.Content.ReadAsStringAsync().Result;

            var customers = JsonConvert.DeserializeObject<List<Customer>>(result);

            return View(customers);
        }
        //the page that manage each user
        public async Task<IActionResult> Manage(int id)
        {
            var response = await NwbaAPI.InitializeClient().GetAsync($"api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return View(customer);
        }
    }
}
