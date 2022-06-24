using System.Collections.Generic;

namespace OnlineStore.BLL.DTO
{
    public partial class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool? Rating { get; set; }
        public int? CategoryId { get; set; }
        public int? BrandId { get; set; }

        public virtual BrandDTO Brand { get; set; }
        public virtual CategoryDTO Category { get; set; }
        public virtual ICollection<BasketDTO> Baskets { get; set; }
        public virtual ICollection<PictureDTO> Pictures { get; set; }
        public virtual ICollection<ProductInfoDTO> ProductInfos { get; set; }
        public virtual ICollection<PurchaseHistoryDTO> PurchaseHistories { get; set; }
        public virtual ICollection<StoresProductDTO> StoresProducts { get; set; }
    }
}
