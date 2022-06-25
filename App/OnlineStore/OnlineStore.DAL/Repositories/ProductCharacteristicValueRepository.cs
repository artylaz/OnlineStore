using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class ProductCharacteristicValueRepository : IRepository<ProductCharacteristicValue>
    {
        private readonly OnlineStoreDbContext db;

        public ProductCharacteristicValueRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(ProductCharacteristicValue item)
        {
            db.ProductCharacteristicValues.Add(item);
        }

        public void Delete(int id)
        {
            ProductCharacteristicValue productCharacteristicValue = db.ProductCharacteristicValues.Find(id);
            if (productCharacteristicValue != null)
                db.ProductCharacteristicValues.Remove(productCharacteristicValue);
        }

        public IEnumerable<ProductCharacteristicValue> Find(Func<ProductCharacteristicValue, bool> predicate)
        {
            return db.ProductCharacteristicValues.Where(predicate);
        }

        public ProductCharacteristicValue Get(int id)
        {
            return db.ProductCharacteristicValues.Find(id);
        }

        public IEnumerable<ProductCharacteristicValue> GetAll()
        {
            return db.ProductCharacteristicValues;
        }

        public void Update(ProductCharacteristicValue item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
