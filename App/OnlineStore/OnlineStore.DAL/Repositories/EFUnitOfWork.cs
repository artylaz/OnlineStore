using OnlineStore.DAL.EF;
using OnlineStore.DAL.Entities;
using OnlineStore.DAL.Interfaces;
using System;

namespace OnlineStore.DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly OnlineStoreDbContext db;

        public EFUnitOfWork(OnlineStoreDbContext db)
        {
            this.db = db;
        }

        private AddressRepository addressRepository;

        private BasketRepository basketRepository;

        private BrandRepository brandRepository;

        private CategoryRepository categoryRepository;

        private MonitorDatabaseRepository monitorDatabaseRepository;

        private PermissionRepository permissionRepository;

        private PictureRepository pictureRepository;

        private ProductRepository productRepository;

        private ProductInfoRepository productInfoRepository;

        private PurchaseHistoryRepository purchaseHistoryRepository;

        private RoleRepository roleRepository;

        private RolesPermissionRepository rolesPermissionRepository;

        private StoreRepository storeRepository;

        private StoresProductRepository storesProductRepository;

        private UserRepository userRepository;

        public IRepository<Address> Addresses
        {
            get
            {
                if (addressRepository == null)
                    addressRepository = new AddressRepository(db);
                return addressRepository;
            }
        }

        public IRepository<Basket> Baskets
        {
            get
            {
                if (basketRepository == null)
                    basketRepository = new BasketRepository(db);
                return basketRepository;
            }
        }

        public IRepository<Brand> Brands
        {
            get
            {
                if (brandRepository == null)
                    brandRepository = new BrandRepository(db);
                return brandRepository;
            }
        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categoryRepository == null)
                    categoryRepository = new CategoryRepository(db);
                return categoryRepository;
            }
        }

        public IRepository<MonitorDatabase> MonitorDatabases
        {
            get
            {
                if (monitorDatabaseRepository == null)
                    monitorDatabaseRepository = new MonitorDatabaseRepository(db);
                return monitorDatabaseRepository;
            }
        }

        public IRepository<Permission> Permissions
        {
            get
            {
                if (permissionRepository == null)
                    permissionRepository = new PermissionRepository(db);
                return permissionRepository;
            }
        }

        public IRepository<Picture> Pictures
        {
            get
            {
                if (pictureRepository == null)
                    pictureRepository = new PictureRepository(db);
                return pictureRepository;
            }
        }

        public IRepository<Product> Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db);
                return productRepository;
            }
        }

        public IRepository<ProductInfo> ProductInfos
        {
            get
            {
                if (productInfoRepository == null)
                    productInfoRepository = new ProductInfoRepository(db);
                return productInfoRepository;
            }
        }

        public IRepository<PurchaseHistory> PurchaseHistories
        {
            get
            {
                if (purchaseHistoryRepository == null)
                    purchaseHistoryRepository = new PurchaseHistoryRepository(db);
                return purchaseHistoryRepository;
            }
        }

        public IRepository<Role> Roles
        {
            get
            {
                if (roleRepository == null)
                    roleRepository = new RoleRepository(db);
                return roleRepository;
            }
        }

        public IRepository<RolesPermission> RolesPermissions
        {
            get
            {
                if (rolesPermissionRepository == null)
                    rolesPermissionRepository = new RolesPermissionRepository(db);
                return rolesPermissionRepository;
            }
        }

        public IRepository<Store> Stores
        {
            get
            {
                if (storeRepository == null)
                    storeRepository = new StoreRepository(db);
                return storeRepository;
            }
        }

        public IRepository<StoresProduct> StoresProducts
        {
            get
            {
                if (storesProductRepository == null)
                    storesProductRepository = new StoresProductRepository(db);
                return storesProductRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }



        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}
