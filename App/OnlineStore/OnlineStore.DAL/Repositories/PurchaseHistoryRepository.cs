using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class PurchaseHistoryRepository : IRepository<PurchaseHistory>
    {
        private readonly OnlineStoreDbContext db;

        public PurchaseHistoryRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(PurchaseHistory item)
        {
            db.PurchaseHistories.Add(item);
        }

        public void Delete(int id)
        {
            PurchaseHistory purchaseHistory = db.PurchaseHistories.Find(id);
            if (purchaseHistory != null)
                db.PurchaseHistories.Remove(purchaseHistory);
        }

        public IEnumerable<PurchaseHistory> Find(Func<PurchaseHistory, bool> predicate)
        {
            return db.PurchaseHistories.Where(predicate);
        }

        public PurchaseHistory Get(int id)
        {
            return db.PurchaseHistories.Find(id);
        }

        public IEnumerable<PurchaseHistory> GetAll()
        {
            return db.PurchaseHistories;
        }

        public void Update(PurchaseHistory item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
