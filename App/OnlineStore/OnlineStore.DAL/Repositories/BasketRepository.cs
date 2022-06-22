using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class BasketRepository : IRepository<Basket>
    {
        private readonly OnlineStoreDbContext db;

        public BasketRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Basket item)
        {
            db.Baskets.Add(item);
        }

        public void Delete(int id)
        {
            Basket basket = db.Baskets.Find(id);
            if (basket != null)
                db.Baskets.Remove(basket);
        }

        public IEnumerable<Basket> Find(Func<Basket, bool> predicate)
        {
            return db.Baskets.Where(predicate);
        }

        public Basket Get(int id)
        {
            return db.Baskets.Find(id);
        }

        public IEnumerable<Basket> GetAll()
        {
            return db.Baskets;
        }

        public void Update(Basket item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
