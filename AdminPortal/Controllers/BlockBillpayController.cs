using AdminPortal.Attributes;
using AdminPortal.Models;
using AdminWeb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Controllers
{
    [AuthorizeAdmin]
    [Route("/SecureBlockBillpay/{Action}")]
    public class BlockBillpayController: Controller
    {
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
