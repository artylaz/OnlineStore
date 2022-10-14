using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class CharacteristicRepository : IRepository<Characteristic>
    {
        private readonly OnlineStoreDbContext db;

        public CharacteristicRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Characteristic item)
        {
            db.Characteristics.Add(item);
        }

        public void Delete(int id)
        {
            Characteristic characteristic = db.Characteristics.Find(id);
            if (characteristic != null)
                db.Characteristics.Remove(characteristic);
        }

        public IEnumerable<Characteristic> Find(Func<Characteristic, bool> predicate)
        {
            return db.Characteristics.Where(predicate);
        }

        public Characteristic Get(int id)
        {
            return db.Characteristics.Find(id);
        }

        public IEnumerable<Characteristic> GetAll()
        {
            return db.Characteristics;
        }

        public void Update(Characteristic item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
