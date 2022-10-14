using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class StoresUserRepository : IRepository<StoresUser>
    {
        private readonly OnlineStoreDbContext db;

        public StoresUserRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(StoresUser item)
        {
            db.StoresUsers.Add(item);
        }

        public void Delete(int id)
        {
            StoresUser storesUser = db.StoresUsers.Find(id);
            if (storesUser != null)
                db.StoresUsers.Remove(storesUser);
        }

        public IEnumerable<StoresUser> Find(Func<StoresUser, bool> predicate)
        {
            return db.StoresUsers.Where(predicate);
        }

        public StoresUser Get(int id)
        {
            return db.StoresUsers.Find(id);
        }

        public IEnumerable<StoresUser> GetAll()
        {
            return db.StoresUsers;
        }

        public void Update(StoresUser item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
