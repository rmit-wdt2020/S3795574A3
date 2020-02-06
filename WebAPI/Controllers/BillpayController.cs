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
    public class BillpayController:Controller
    {
        private readonly BillpayManager _repo;
        public BillpayController(BillpayManager repo)
        {
            _repo = repo;
        }
        [HttpGet("{id}")]
        public IEnumerable<BillPay> Get(int id)
        {
            return _repo.GetAll(id);
        }
        [HttpPut]
        public void Put([FromBody] BillPay billpay)
        {
            _repo.Update(billpay.BillPayID, billpay);
        }
        [HttpGet("{id}/{id2}")]
        public BillPay Get(int id,int id2)
        {
            return _repo.GetBillpay(id,id2);
        }
    }
}
