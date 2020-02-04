using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repository;
using WebAPI.Models;

namespace WebAPI.Data.DataManager
{
    public class CustomerManager : IDataRepository<Customer, int>
    {
        private readonly NwbaContext _context;

        public CustomerManager(NwbaContext context)
        {
            _context = context;
        }
        public int Add(Customer item)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        public IEnumerable<Customer> GetAll()
        {
            return _context.Customers.ToList();
        }

        public int Update(int id, Customer customer)
        {
            _context.Update(customer);
            _context.SaveChanges();

            return id;
        }
    }
}
