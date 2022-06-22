using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class StoreRepository : IRepository<Store>
    {
        private readonly OnlineStoreDbContext db;

        public StoreRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Store item)
        {
            db.Stores.Add(item);
        }

        public void Delete(int id)
        {
            Store store = db.Stores.Find(id);
            if (store != null)
                db.Stores.Remove(store);
        }

        public IEnumerable<Store> Find(Func<Store, bool> predicate)
        {
            return db.Stores.Where(predicate);
        }

        public Store Get(int id)
        {
            return db.Stores.Find(id);
        }

        public IEnumerable<Store> GetAll()
        {
            return db.Stores;
        }

        public void Update(Store item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
