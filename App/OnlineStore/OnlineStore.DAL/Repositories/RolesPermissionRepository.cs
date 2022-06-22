using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class RolesPermissionRepository : IRepository<RolesPermission>
    {
        private readonly OnlineStoreDbContext db;

        public RolesPermissionRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(RolesPermission item)
        {
            db.RolesPermissions.Add(item);
        }

        public void Delete(int id)
        {
            RolesPermission rolesPermission = db.RolesPermissions.Find(id);
            if (rolesPermission != null)
                db.RolesPermissions.Remove(rolesPermission);
        }

        public IEnumerable<RolesPermission> Find(Func<RolesPermission, bool> predicate)
        {
            return db.RolesPermissions.Where(predicate);
        }

        public RolesPermission Get(int id)
        {
            return db.RolesPermissions.Find(id);
        }

        public IEnumerable<RolesPermission> GetAll()
        {
            return db.RolesPermissions;
        }

        public void Update(RolesPermission item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
