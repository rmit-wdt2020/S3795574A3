using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repository;
using WebAPI.Models;
using X.PagedList;

namespace WebAPI.Data.DataManager
{
    public class TransactionManager : IDataRepository<Transaction, int>
    {
        private readonly NwbaContext _context;
        public TransactionManager(NwbaContext context)
        {
            _context = context;
        }

        public int Add(Transaction item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Transaction Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Transaction> GetAll(int id)
        {
            return _context.Transactions.Where(x => x.AccountNumber == id || x.DestinationAccountNumber == id).OrderByDescending(x=>x.ModifyDate).AsEnumerable() ;
        }

        public IEnumerable<Transaction> GetAll(int id,int id2)
        {

            return _context.Transactions.Where(x => x.AccountNumber == id || x.DestinationAccountNumber == id).Where(x=>x.ModifyDate > DateTime.Now.AddDays(-id2)).OrderByDescending(x => x.ModifyDate).AsEnumerable();
        }

        public IEnumerable<Transaction> GetAll()
        {
            throw new NotImplementedException();
        }

        public int Update(int id, Transaction item)
        {
            throw new NotImplementedException();
        }
    }
}
