using OnlineStore.DAL.Entities;
using System;

namespace OnlineStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Address> Addresses { get; }
        IRepository<Basket> Baskets { get; }
        IRepository<Brand> Brands { get; }
        IRepository<Category> Categories { get; set; }
        IRepository<CharacteristicValue> CharacteristicValues { get; }
        IRepository<MonitorDatabase> MonitorDatabases { get; }
        IRepository<Permission> Permissions { get; }
        IRepository<Picture> Pictures { get; }
        IRepository<Product> Products { get; }
        IRepository<ProductCharacteristicValue> ProductCharacteristicValues { get; }
        IRepository<PurchaseHistory> PurchaseHistories { get; }
        IRepository<Role> Roles { get; }
        IRepository<RolesPermission> RolesPermissions { get; }
        IRepository<Store> Stores { get; }
        IRepository<StoresProduct> StoresProducts { get; }
        IRepository<StoresUser> StoresUsers { get; }
        IRepository<User> Users { get; }
        IRepository<Сharacteristic> Сharacteristics { get; }

        void Save();
    }
}
