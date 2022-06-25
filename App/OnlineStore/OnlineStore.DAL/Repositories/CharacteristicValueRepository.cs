using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class CharacteristicValueRepository : IRepository<CharacteristicValue>
    {
        private readonly OnlineStoreDbContext db;

        public CharacteristicValueRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(CharacteristicValue item)
        {
            db.CharacteristicValues.Add(item);
        }

        public void Delete(int id)
        {
            CharacteristicValue characteristicValue = db.CharacteristicValues.Find(id);
            if (characteristicValue != null)
                db.CharacteristicValues.Remove(characteristicValue);
        }

        public IEnumerable<CharacteristicValue> Find(Func<CharacteristicValue, bool> predicate)
        {
            return db.CharacteristicValues.Where(predicate);
        }

        public CharacteristicValue Get(int id)
        {
            return db.CharacteristicValues.Find(id);
        }

        public IEnumerable<CharacteristicValue> GetAll()
        {
            return db.CharacteristicValues;
        }

        public void Update(CharacteristicValue item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
