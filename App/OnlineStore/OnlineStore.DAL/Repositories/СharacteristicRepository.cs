using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class СharacteristicRepository : IRepository<Сharacteristic>
    {
        private readonly OnlineStoreDbContext db;

        public СharacteristicRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Сharacteristic item)
        {
            db.Сharacteristics.Add(item);
        }

        public void Delete(int id)
        {
            Сharacteristic characteristic = db.Сharacteristics.Find(id);
            if (characteristic != null)
                db.Сharacteristics.Remove(characteristic);
        }

        public IEnumerable<Сharacteristic> Find(Func<Сharacteristic, bool> predicate)
        {
            return db.Сharacteristics.Where(predicate);
        }

        public Сharacteristic Get(int id)
        {
            return db.Сharacteristics.Find(id);
        }

        public IEnumerable<Сharacteristic> GetAll()
        {
            return db.Сharacteristics;
        }

        public void Update(Сharacteristic item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
