using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class AddressRepository : IRepository<Address>
    {
        private readonly OnlineStoreDbContext db;

        public AddressRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Address item)
        {
            db.Addresses.Add(item);
        }

        public void Delete(int id)
        {
            Address address = db.Addresses.Find(id);
            if (address != null)
                db.Addresses.Remove(address);
        }

        public IEnumerable<Address> Find(Func<Address, bool> predicate)
        {
            return db.Addresses.Where(predicate);
        }

        public Address Get(int id)
        {
            return db.Addresses.Find(id);
        }

        public IEnumerable<Address> GetAll()
        {
            return db.Addresses;
        }

        public void Update(Address item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
