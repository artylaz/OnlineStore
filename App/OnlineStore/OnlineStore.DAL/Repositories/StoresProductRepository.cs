using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class StoresProductRepository : IRepository<StoresProduct>
    {
        private readonly OnlineStoreDbContext db;

        public StoresProductRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(StoresProduct item)
        {
            db.StoresProducts.Add(item);
        }

        public void Delete(int id)
        {
            StoresProduct storesProduct = db.StoresProducts.Find(id);
            if (storesProduct != null)
                db.StoresProducts.Remove(storesProduct);
        }

        public IEnumerable<StoresProduct> Find(Func<StoresProduct, bool> predicate)
        {
            return db.StoresProducts.Where(predicate);
        }

        public StoresProduct Get(int id)
        {
            return db.StoresProducts.Find(id);
        }

        public IEnumerable<StoresProduct> GetAll()
        {
            return db.StoresProducts;
        }

        public void Update(StoresProduct item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
