using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class ProductInfoRepository : IRepository<ProductInfo>
    {
        private readonly OnlineStoreDbContext db;

        public ProductInfoRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(ProductInfo item)
        {
            db.ProductInfos.Add(item);
        }

        public void Delete(int id)
        {
            ProductInfo productInfo = db.ProductInfos.Find(id);
            if (productInfo != null)
                db.ProductInfos.Remove(productInfo);
        }

        public IEnumerable<ProductInfo> Find(Func<ProductInfo, bool> predicate)
        {
            return db.ProductInfos.Where(predicate);
        }

        public ProductInfo Get(int id)
        {
            return db.ProductInfos.Find(id);
        }

        public IEnumerable<ProductInfo> GetAll()
        {
            return db.ProductInfos;
        }

        public void Update(ProductInfo item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
