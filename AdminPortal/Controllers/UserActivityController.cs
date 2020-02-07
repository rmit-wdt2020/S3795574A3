using AdminPortal.Attributes;
using AdminPortal.Models;
using AdminPortal.ViewModel;
using AdminWeb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace AdminPortal.Controllers
{
    [AuthorizeAdmin]
    [Route("/SecureUserActivity/{Action}")]
    public class UserActivityController: Controller
    {

        public async Task<IActionResult> Activity(int? id)
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

        public async Task<IActionResult> TransactionHistory(int? id, int? id2, int page = 1)
        {
            HttpResponseMessage response;
            const int pageSize = 6;
            if (!id2.HasValue)
                response = await NwbaAPI.InitializeClient().GetAsync($"api/transaction/{id}");
            else
                response = await NwbaAPI.InitializeClient().GetAsync($"api/transaction/{id}/{id2}");
            ViewBag.AccountNumber = id;
            if (!response.IsSuccessStatusCode)
                throw new Exception();
            var result = response.Content.ReadAsStringAsync().Result;
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);

            return View(transactions.ToPagedList(page, pageSize));
        }
        public async Task<IActionResult> Chart(int? id, int? id2)
        {
            HttpResponseMessage response;
            if (String.IsNullOrEmpty(id2.ToString()))
                response = await NwbaAPI.InitializeClient().GetAsync($"api/transaction/{id}");
            else
                response = await NwbaAPI.InitializeClient().GetAsync($"api/transaction/{id}/{id2}");
            ViewBag.AccountNumber = id;
            if (!response.IsSuccessStatusCode)
                throw new Exception();
            var result = response.Content.ReadAsStringAsync().Result;
            var transactions = JsonConvert.DeserializeObject<List<Transaction>>(result);
            var SortedList = transactions.OrderBy(o => o.ModifyDate).ToList();
            List<string> date = new List<string>();
            foreach(var t in SortedList)
            {
                date.Add(t.ModifyDate.ToString("dd/MM/yyyy"));
            }
            var chartViewModelList = date.GroupBy(x => x)
                    .Select(g => new ChartViewModel{ Date = g.Key, Count = g.Count() })
                    .ToList();

            return View(chartViewModelList);
        }
    }
}
