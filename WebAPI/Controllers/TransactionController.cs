using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebAPI.Data.DataManager;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController: Controller
    {
        private readonly TransactionManager _repo;
        public TransactionController(TransactionManager repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public IEnumerable<Transaction> Get(int id)
        {
            return _repo.GetAll(id);
        }
        [HttpGet("{id}/{id2}")]
        public IEnumerable<Transaction> Get(int id,int id2)
        {
            return _repo.GetAll(id,id2);
        }
    }
}
