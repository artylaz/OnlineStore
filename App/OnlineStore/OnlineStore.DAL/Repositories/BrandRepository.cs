using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class BrandRepository : IRepository<Brand>
    {
        private readonly OnlineStoreDbContext db;

        public BrandRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Brand item)
        {
            db.Brands.Add(item);
        }

        public void Delete(int id)
        {
            Brand brand = db.Brands.Find(id);
            if (brand != null)
                db.Brands.Remove(brand);
        }

        public IEnumerable<Brand> Find(Func<Brand, bool> predicate)
        {
            return db.Brands.Where(predicate);
        }

        public Brand Get(int id)
        {
            return db.Brands.Find(id);
        }

        public IEnumerable<Brand> GetAll()
        {
            return db.Brands;
        }

        public void Update(Brand item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
