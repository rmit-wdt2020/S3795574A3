using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.DataManager;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly LoginManager _repo;
        public LoginController(LoginManager repo)
        {
            _repo = repo;
        }
        [HttpGet]
        public IEnumerable<Login> Get()
        {
            return _repo.GetAll();
        }
        [HttpGet("{id}")]
        public Login Get(int id)
        {
            return _repo.Get(id);
        }
        [HttpPut]
        public void Put([FromBody] Login login)
        {
            _repo.Update(login.CustomerID, login);
        }
    }
}
