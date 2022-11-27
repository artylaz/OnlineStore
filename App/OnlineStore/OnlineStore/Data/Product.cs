using System;
using System.Collections.Generic;

#nullable disable

namespace OnlineStore.Data
{
    public partial class Product
    {
        public Product()
        {
            Baskets = new HashSet<Basket>();
            Pictures = new HashSet<Picture>();
            ProductCharacteristics = new HashSet<ProductCharacteristic>();
            PurchaseHistories = new HashSet<PurchaseHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? Rating { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Basket> Baskets { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<ProductCharacteristic> ProductCharacteristics { get; set; }
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; }
    }
}
