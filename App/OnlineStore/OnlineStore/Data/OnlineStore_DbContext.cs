using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnlineStore.Data
{
    public partial class OnlineStore_DbContext : DbContext
    {
        public OnlineStore_DbContext()
        {
        }

        public OnlineStore_DbContext(DbContextOptions<OnlineStore_DbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Basket> Baskets { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Characteristic> Characteristics { get; set; }
        public virtual DbSet<MonitorDatabase> MonitorDatabases { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCharacteristic> ProductCharacteristics { get; set; }
        public virtual DbSet<PurchaseHistory> PurchaseHistories { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Basket>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId })
                    .HasName("PK__Baskets__DCC80020F3056140");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__Baskets__Product__20C1E124");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Baskets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Baskets__UserId__1FCDBCEB");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OnSale)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.InverseCategoryNavigation)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Categorie__Categ__173876EA");
            });

            modelBuilder.Entity<Characteristic>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(30);
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

            modelBuilder.Entity<Picture>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(30);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__Pictures__Produc__1CF15040");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('Нет описания')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OnSale)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Products__Catego__1A14E395");
            });

            modelBuilder.Entity<ProductCharacteristic>(entity =>
            {
                entity.HasKey(e => new { e.ProductId, e.CharacteristicId })
                    .HasName("PK__Product___58026211C132A5BB");

                entity.ToTable("Product_Characteristics");

                entity.HasOne(d => d.Characteristic)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(d => d.CharacteristicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_C__Chara__2A4B4B5E");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductCharacteristics)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Product_C__Produ__29572725");
            });

            modelBuilder.Entity<PurchaseHistory>(entity =>
            {
                entity.ToTable("PurchaseHistory");

                entity.Property(e => e.DatePurchase).HasColumnType("datetime");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PurchaseHistories)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK__PurchaseH__Produ__24927208");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PurchaseHistories)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__PurchaseH__UserI__239E4DCF");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Phone, "UQ__Users__5C7E359E68AAE883")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105349361A158")
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

                entity.Property(e => e.OnSale)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

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
                    .HasConstraintName("FK__Users__RoleId__145C0A3F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
