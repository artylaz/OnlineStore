using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.DAL.Repositories
{
    public class MonitorDatabaseRepository : IRepository<MonitorDatabase>
    {
        private readonly OnlineStoreDbContext db;

        public MonitorDatabaseRepository(OnlineStoreDbContext context)
        {
            db = context;
        }

        public void Create(MonitorDatabase item)
        {
            db.MonitorDatabases.Add(item);
        }

        public void Delete(int id)
        {
            MonitorDatabase monitorDatabase = db.MonitorDatabases.Find(id);
            if (monitorDatabase != null)
                db.MonitorDatabases.Remove(monitorDatabase);
        }

        public IEnumerable<MonitorDatabase> Find(Func<MonitorDatabase, bool> predicate)
        {
            return db.MonitorDatabases.Where(predicate);
        }

        public MonitorDatabase Get(int id)
        {
            return db.MonitorDatabases.Find(id);
        }

        public IEnumerable<MonitorDatabase> GetAll()
        {
            return db.MonitorDatabases;
        }

        public void Update(MonitorDatabase item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
