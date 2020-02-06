using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Data.Repository;
using WebAPI.Models;

namespace WebAPI.Data.DataManager
{
    public class LoginManager :IDataRepository<Login, int>
    {
        private readonly NwbaContext _context;

        public LoginManager(NwbaContext context)
        {
            _context = context;
        }
        public int Add(Login user)
        {
            throw new NotImplementedException();
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Login Get(int id)
        {
            return _context.Logins.Where(x=>x.CustomerID == id).FirstOrDefault();
        }

        public IEnumerable<Login> GetAll()
        {
            return _context.Logins.ToList();
        }

        public int Update(int id, Login user)
        {
            var newUser = _context.Logins.Find(user.UserID);
            if (newUser.IsLocked)
                newUser.IsLocked = false;
            else
                newUser.IsLocked = true;
            _context.Update(newUser);
            _context.SaveChanges();

            return id;
        }
    }
}
