using AdminPortal.Attributes;
using AdminPortal.Models;
using AdminWeb;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminPortal.Controllers
{
    [AuthorizeAdmin]
    [Route("/SecureBlockUser/{Action}")]
    public class BlockUserController:Controller
    {
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
    }
}
