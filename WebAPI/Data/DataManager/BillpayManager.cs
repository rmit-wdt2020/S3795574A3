using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repository;
using WebAPI.Models;

namespace WebAPI.Data.DataManager
{
    public class BillpayManager : IDataRepository<BillPay, int>
    {
        private readonly NwbaContext _context;

        public BillpayManager(NwbaContext context)
        {
            _context = context;
        }
        public int Add(BillPay item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }
        public BillPay Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BillPay> GetAll(int id)
        {
            return _context.BillPays.Where(x => x.AccountNumber == id).ToList();
        }

        public IEnumerable<BillPay> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(int id, BillPay item)
        {
            var newBillpay = _context.BillPays.Find(id);
            if (newBillpay.IsLocked)
                newBillpay.IsLocked = false;
            else
                newBillpay.IsLocked = true;
            _context.Update(newBillpay);
            _context.SaveChanges();

            return id;
        }
        public BillPay GetBillpay(int id, int id2)
        {
            return _context.BillPays.Find(id2);
        }
    }
}
