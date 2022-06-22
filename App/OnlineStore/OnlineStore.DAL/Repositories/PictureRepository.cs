using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class PictureRepository : IRepository<Picture>
    {
        private readonly OnlineStoreDbContext db;

        public PictureRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Picture item)
        {
            db.Pictures.Add(item);
        }

        public void Delete(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture != null)
                db.Pictures.Remove(picture);
        }

        public IEnumerable<Picture> Find(Func<Picture, bool> predicate)
        {
            return db.Pictures.Where(predicate);
        }

        public Picture Get(int id)
        {
            return db.Pictures.Find(id);
        }

        public IEnumerable<Picture> GetAll()
        {
            return db.Pictures;
        }

        public void Update(Picture item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
