using AdminPortal.Attributes;
using AdminPortal.Models;
using AdminWeb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Controllers
{
    [AuthorizeAdmin]
    [Route("/SecureUserDetail/{Action}")]
    public class UserDetailController: Controller
    {
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaAPI.InitializeClient().GetAsync($"api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return View(customer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Customer customer)
        {
            if (id != customer.CustomerID)
                return NotFound();

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonConvert.SerializeObject(customer), Encoding.UTF8, "application/json");
                var response = NwbaAPI.InitializeClient().PutAsync("api/customer", content).Result;

                if (response.IsSuccessStatusCode)
                    return RedirectToAction("Index", "Dashboard");
            }

            return View(customer);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaAPI.InitializeClient().GetAsync($"api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return View(customer);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var response = NwbaAPI.InitializeClient().DeleteAsync($"api/customer/{id}").Result;

            if (response.IsSuccessStatusCode)
                return RedirectToAction("Index", "Dashboard");

            return NotFound();
        }
    }
}
