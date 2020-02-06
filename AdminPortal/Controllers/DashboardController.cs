using AdminPortal.Attributes;
using AdminPortal.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminWeb.Controllers
{
    [AuthorizeAdmin]
    public class DashboardController:Controller
    {
        public async Task<IActionResult> Index()
        {
            var response = await NwbaAPI.InitializeClient().GetAsync("api/customer");

            if (!response.IsSuccessStatusCode)
                throw new Exception();              
            // Storing the response details recieved from web api.
            var result = response.Content.ReadAsStringAsync().Result;

            // Deserializing the response recieved from web api and storing into a list.
            var customers = JsonConvert.DeserializeObject<List<Customer>>(result);

            return View(customers);
        }
        public async Task<IActionResult> Manage(int id)
        {
            var response = await NwbaAPI.InitializeClient().GetAsync($"api/customer/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var customer = JsonConvert.DeserializeObject<Customer>(result);

            return View(customer);
        }
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
                    return RedirectToAction("Index");
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
                return RedirectToAction("Index");

            return NotFound();
        }
        public async Task<IActionResult> BlockLogin(int id)
        {
            var response = await NwbaAPI.InitializeClient().GetAsync($"api/login/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = response.Content.ReadAsStringAsync().Result;
            var user = JsonConvert.DeserializeObject<Login>(result);
            ViewBag.CustomerID = id;

            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockLoginAsync(int id, Login user)
        {
            if (id != user.CustomerID)
                return NotFound();

            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = NwbaAPI.InitializeClient().PutAsync("api/login", content).Result;
            //if success, get new user and display in the view
            if (response.IsSuccessStatusCode)
            {
                var newResponse = await NwbaAPI.InitializeClient().GetAsync($"api/login/{id}");
                var result = newResponse.Content.ReadAsStringAsync().Result;
                var newUser = JsonConvert.DeserializeObject<Login>(result);
                return View(newUser);
            }
            return View(user);
        }
        public async Task<IActionResult> GetAccounts(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaAPI.InitializeClient().GetAsync($"api/account/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();
            var result = response.Content.ReadAsStringAsync().Result;
            var accounts = JsonConvert.DeserializeObject<List<Account>>(result);

            return View(accounts);
        }
        public async Task<IActionResult> BillpayList(int? id)
        {
            if (id == null)
                return NotFound();

            var response = await NwbaAPI.InitializeClient().GetAsync($"api/billpay/{id}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();
            var result = response.Content.ReadAsStringAsync().Result;
            var billPays = JsonConvert.DeserializeObject<List<BillPay>>(result);

            return View(billPays);
        }
        public async Task<IActionResult> BlockBillpay(int? id, int? id2)
        {
            var response = await NwbaAPI.InitializeClient().GetAsync($"api/billpay/{id}/{id2}");
            var result = response.Content.ReadAsStringAsync().Result;
            var billpay = JsonConvert.DeserializeObject<BillPay>(result);
            return View(billpay);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BlockBillpay(int? id, int? id2, BillPay billpay)
        {
            var content = new StringContent(JsonConvert.SerializeObject(billpay), Encoding.UTF8, "application/json");
            var response = NwbaAPI.InitializeClient().PutAsync("api/billpay", content).Result;
            //if success, get new billpay and display in the view
            if (response.IsSuccessStatusCode)
            {
                var newResponse = await NwbaAPI.InitializeClient().GetAsync($"api/billpay/{id}/{id2}");
                var result = newResponse.Content.ReadAsStringAsync().Result;
                var newBillpay = JsonConvert.DeserializeObject<BillPay>(result);
                return View(newBillpay);
            }
            return View(billpay);
        }
    }
}
