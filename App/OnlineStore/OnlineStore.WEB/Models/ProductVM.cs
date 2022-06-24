using System.Collections.Generic;

namespace OnlineStore.WEB.Models
{
    public partial class ProductVM
    {
        public ProductVM()
        {
            Baskets = new HashSet<BasketVM>();
            Pictures = new HashSet<PictureVM>();
            ProductInfos = new HashSet<ProductInfoVM>();
            PurchaseHistories = new HashSet<PurchaseHistoryVM>();
            StoresProducts = new HashSet<StoresProductVM>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool? Rating { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        public virtual BrandVM Brand { get; set; }
        public virtual CategoryVM Category { get; set; }
        public virtual ICollection<BasketVM> Baskets { get; set; }
        public virtual ICollection<PictureVM> Pictures { get; set; }
        public virtual ICollection<ProductInfoVM> ProductInfos { get; set; }
        public virtual ICollection<PurchaseHistoryVM> PurchaseHistories { get; set; }
        public virtual ICollection<StoresProductVM> StoresProducts { get; set; }
    }
}
