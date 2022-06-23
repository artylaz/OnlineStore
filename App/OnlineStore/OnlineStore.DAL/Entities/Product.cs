using System.Collections.Generic;

namespace OnlineStore.DAL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Baskets = new HashSet<Basket>();
            Pictures = new HashSet<Picture>();
            ProductInfos = new HashSet<ProductInfo>();
            PurchaseHistories = new HashSet<PurchaseHistory>();
            StoresProducts = new HashSet<StoresProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool? Rating { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        public virtual Brand Brand { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<ProductInfo> ProductInfos { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
        public virtual ICollection<StoresProduct> StoresProducts { get; set; }
    }
}
