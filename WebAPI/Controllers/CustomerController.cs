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
    public class CustomerController : Controller
    {
        private readonly CustomerManager _repo;

        public CustomerController(CustomerManager repo)
        {
            _repo = repo;
        }
        // GET: api/movies
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _repo.GetAll();
        }
        [HttpPut]
        public void Put([FromBody] Customer customer)
        {
            _repo.Update(customer.CustomerID, customer);
        }
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _repo.Get(id);
        }
    }
}
