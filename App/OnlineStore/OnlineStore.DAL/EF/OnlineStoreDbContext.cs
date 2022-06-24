using Microsoft.EntityFrameworkCore;
using OnlineStore.DAL.Entities;

namespace OnlineStore.DAL.EF
{
    public partial class OnlineStoreDbContext : DbContext
    {
        public OnlineStoreDbContext()
        {
        }

        public OnlineStoreDbContext(DbContextOptions<OnlineStoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<MonitorDatabase> MonitorDatabases { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductInfo> ProductInfos { get; set; }
        public virtual DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RolesPermission> RolesPermissions { get; set; }
        public virtual DbSet<Store> Stores { get; set; }
        public virtual DbSet<StoresProduct> StoresProducts { get; set; }
        public virtual DbSet<StoresUser> StoresUsers { get; set; }
        public virtual DbSet<User> Users { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.House)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Housing)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Region)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Addresses__Store__6477ECF3");
            });

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId })
                    .HasName("PK__Baskets__DCC800205FA6E2E0");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Baskets__Product__4D94879B");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Baskets__UserId__4CA06362");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InverseCategoryNavigation)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Categorie__Categ__1CF15040");
            });

            modelBuilder.Entity<MonitorDatabase>(entity =>
            {
                entity.HasKey(e => e.IdItem);

                entity.ToTable("Monitor_Database");

                entity.Property(e => e.IdItem).HasColumnName("Id_item");

                entity.Property(e => e.DataModificationRecord).HasColumnType("datetime");

                entity.Property(e => e.Hostname).HasMaxLength(50);

                entity.Property(e => e.IdRecord).HasMaxLength(100);

                entity.Property(e => e.NameColumns).HasMaxLength(50);

                entity.Property(e => e.NameOperation).HasMaxLength(100);

                entity.Property(e => e.NameTable).HasMaxLength(50);

                entity.Property(e => e.NewRecord).HasMaxLength(100);

                entity.Property(e => e.NtUsername)
                    .HasMaxLength(100)
                    .HasColumnName("Nt_username");

                entity.Property(e => e.OldRecord).HasMaxLength(100);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Pictures__Produc__6FE99F9F");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK__Products__BrandI__22AA2996");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__21B6055D");
            });

            modelBuilder.Entity<ProductInfo>(entity =>
            {
                entity.ToTable("Product_Info");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInfos)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Product_I__Produ__25869641");
            });

            modelBuilder.Entity<PurchaseHistory>(entity =>
            {
                entity.ToTable("PurchaseHistory");

                entity.Property(e => e.DatePurchase).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseHistories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__PurchaseH__Produ__0A9D95DB");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchaseHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PurchaseH__UserI__09A971A2");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<RolesPermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId })
                    .HasName("PK__Roles_Pe__6400A1A8A26FC3B9");

                entity.ToTable("Roles_Permissions");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolesPermissions)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Roles_Per__Permi__15502E78");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolesPermissions)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Roles_Per__RoleI__145C0A3F");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<StoresProduct>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.ProductId })
                    .HasName("PK__Stores_P__F0C23D6D17E440C8");

                entity.ToTable("Stores_Products");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoresProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stores_Pr__Produ__5EBF139D");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoresProducts)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stores_Pr__Store__5DCAEF64");
            });

            modelBuilder.Entity<StoresUser>(entity =>
            {
                entity.HasKey(e => new { e.StoreId, e.UserId })
                    .HasName("PK__Stores_U__EAFA7DC540EC729A");

                entity.ToTable("Stores_Users");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.StoresUsers)
                    .HasForeignKey(d => d.StoreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stores_Us__Store__0D7A0286");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.StoresUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Stores_Us__UserI__0E6E26BF");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359ED45A5858")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534535E588D")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegistrationDate).HasColumnType("date");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Users__RoleId__1A14E395");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
