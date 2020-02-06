using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repository;
using WebAPI.Models;

namespace WebAPI.Data.DataManager
{
    public class AccountManager : IDataRepository<Account, int>
    {
        private readonly NwbaContext _context;
        public AccountManager(NwbaContext context)
        {
            _context = context;
        }
        public int Add(Account item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Account> GetAll()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Account> GetAll(int id)
        {
            return _context.Accounts.Where(x => x.CustomerID == id).ToList<Account>();
        }

        public int Update(int id, Account item)
        {
            throw new NotImplementedException();
        }
    }
}
