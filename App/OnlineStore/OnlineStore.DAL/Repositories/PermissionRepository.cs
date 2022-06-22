using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class PermissionRepository : IRepository<Permission>
    {
        private readonly OnlineStoreDbContext db;

        public PermissionRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Permission item)
        {
            db.Permissions.Add(item);
        }

        public void Delete(int id)
        {
            Permission permission = db.Permissions.Find(id);
            if (permission != null)
                db.Permissions.Remove(permission);
        }

        public IEnumerable<Permission> Find(Func<Permission, bool> predicate)
        {
            return db.Permissions.Where(predicate);
        }

        public Permission Get(int id)
        {
            return db.Permissions.Find(id);
        }

        public IEnumerable<Permission> GetAll()
        {
            return db.Permissions;
        }

        public void Update(Permission item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
