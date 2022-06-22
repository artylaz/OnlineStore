using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class RoleRepository : IRepository<Role>
    {
        private readonly OnlineStoreDbContext db;

        public RoleRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(Role item)
        {
            db.Roles.Add(item);
        }

        public void Delete(int id)
        {
            Role role = db.Roles.Find(id);
            if (role != null)
                db.Roles.Remove(role);
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return db.Roles.Where(predicate);
        }

        public Role Get(int id)
        {
            return db.Roles.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return db.Roles;
        }

        public void Update(Role item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
