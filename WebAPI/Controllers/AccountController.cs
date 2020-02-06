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
    public class AccountController : Controller
    {
        private readonly AccountManager _repo;
        public AccountController(AccountManager repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public IEnumerable<Account> Get(int id)
        {
            return _repo.GetAll(id);
        }
    }
}
